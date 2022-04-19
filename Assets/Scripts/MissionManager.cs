using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.Linq;

public class MissionManager : MonoBehaviour
{

    public GameObject missionCard;
    public GameObject missionHolder;
    public int maxMissionsToShow;
    int missionsToShow;
    public TextAsset allMissionsJson;
    public List<Mission> onScreenMissions;
    public List<string> successfulMissions;
    public List<string> failedMissions;
    public List<Mission> missions;
    private AllMissions missionsJSON;

    ApprenticeManager AM;

    void Start()
    {
        AM = ApprenticeManager.AM;

        missionsJSON = JsonUtility.FromJson<AllMissions>(allMissionsJson.text);
        missions = missionsJSON.allMissions.ToList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

// GM calls this whenever we need to show a new set of missions
// Determine the number of missions to show, instantiate the cards
    public void ShowMissions(){
        onScreenMissions.Clear();
        if(missionHolder.transform.childCount != 0){
            //Debug.Log("Mission holder childeren count: " + missionHolder.transform.childCount);
            foreach(Transform missionCard in missionHolder.transform){
                Destroy(missionCard.gameObject);
                //missionCard.GetComponent<MissionCard>().missionCardAnimator.SetBool("MissionDone", true);
            }
        }
        if(maxMissionsToShow > CheckMissions()){
            missionsToShow = CheckMissions();
        }
        else{
            missionsToShow = maxMissionsToShow;
        }
        for(int i = 0; i < missionsToShow; i++){
            GameObject newMissionCard = Instantiate(missionCard);
            newMissionCard.transform.SetParent(missionHolder.transform);
        }
    }
    
    public int CheckMissions(){
        int missionsAvailable = 0;
        foreach(Mission mission in missions){
            if(AM.apprentice.age >= mission.minAge && AM.apprentice.age <= mission.maxAge){
                if(mission.previousMission == "None"){
                    missionsAvailable ++;
                    Debug.Log(mission.missionName + " available");
                }
                else{  // Check requirements for mission chains
                    if(mission.previousMissionResultNeeded == "Success" && successfulMissions.Contains(mission.previousMission)
                    || mission.previousMissionResultNeeded == "Failure" && failedMissions.Contains(mission.previousMission)){
                        missionsAvailable ++;
                        Debug.Log(mission.missionName + " available");
                    }
                    else{
                        continue;
                    }
                }
            }
            else{
                continue;
            }
        }
        return missionsAvailable;
    }

// MissionCard calls this to figure out what to show
// Should mirror CheckMissions
    public Mission GenerateMission(){

        // shuffle that bad boy
        for (int i = 0; i < missions.Count; i++) {
            Mission temp = missions[i];
            int randomIndex = UnityEngine.Random.Range(i, missions.Count);
            missions[i] = missions[randomIndex];
            missions[randomIndex] = temp;
        }

        // Go through all the missions, and grab the first one that we're old enough for
        foreach(Mission mission in missions){
            // Check if we're already showing a copy of this mission
            if(onScreenMissions.Contains(mission)){
                continue;
            } // Check to make sure we're old enough to ride (but not too old!)
            else if(AM.apprentice.age >= mission.minAge && AM.apprentice.age <= mission.maxAge){
                // Is this part of a chain of missions?
                if(mission.previousMission == "None"){
                    onScreenMissions.Add(mission);
                    return mission;
                }
                else{ // Check requirements for mission chains
                    if(mission.previousMissionResultNeeded == "Success" && successfulMissions.Contains(mission.previousMission)
                    || mission.previousMissionResultNeeded == "Failure" && failedMissions.Contains(mission.previousMission)){
                        onScreenMissions.Add(mission);
                        return mission;
                    }
                    else{
                        continue;
                    }
                }
            }
            else{ // if we're outside the age range, go to the next
                continue;
            }
        }
        // If there weren't any, we're out of new missions
        Debug.Log("No missions available ("+CheckMissions()+")");
        GameManager.GM.noMissionsAvailable.Invoke();
        return null;
    }

    float chanceToPass;

    public bool ResolveMission(Mission mission){
        Debug.Log("Resolving: " + mission.missionName);
        bool succeedMission;
        int missionIndex = missions.IndexOf(mission);

        MatchCollection matchListRegex = Regex.Matches(mission.passReqs, @"\w+");
        var matchList = matchListRegex.Cast<Match>().Select(match => match.Value).ToList();

        List<string> nameOfStatList = new List<string>();
        List<string> valueOfStatList = new List<string>();

        for(int i = 0; i < matchList.Count(); i++){
                if(i % 2 == 0){
                    nameOfStatList.Add(matchList[i]);
                }
                else{
                    valueOfStatList.Add(matchList[i]);
                }
            }

        for(int i = 0; i < nameOfStatList.Count(); i++){
            Debug.Log("Pass reqs: " + nameOfStatList[i] + " " + valueOfStatList[i]);
            string quailifier = nameOfStatList[i];
            string reqStatStr = valueOfStatList[i];

            // They need this attribute to pass
            if(quailifier == "NeedAttribute"){
                if(AM.apprentice.attribtues.Contains(reqStatStr)){
                    // this check passes
                    continue;
                }
                else{
                    // this check fails, mission is failed
                    Debug.Log("Mission failed.");
                    succeedMission = false;
                    failedMissions.Add(mission.missionName);
                    missions.RemoveAt(missionIndex);
                    // update player stats
                    ApprenticeManager.AM.UpdateStatsMission(succeedMission, mission);
                    return succeedMission;
                }
            }
            // If they have this attribute, they DON'T pass
            else if(quailifier == "BadAttribute"){
                if(AM.apprentice.attribtues.Contains(reqStatStr)){
                    // this check fails, mission is failed
                    Debug.Log("Mission failed.");
                    succeedMission = false;
                    failedMissions.Add(mission.missionName);
                    missions.RemoveAt(missionIndex);
                    // update player stats
                    ApprenticeManager.AM.UpdateStatsMission(succeedMission, mission);
                    return succeedMission;
                }
                else{
                    // this check passes
                    continue;
                }
            }
            else{ // Done with attributes, the rest are numbers
                float reqStat = float.Parse(reqStatStr);
                if(quailifier == "Loyalty"){
                    chanceToPass = AM.apprentice.loyalty/reqStat;
                }
                if(quailifier == "Power"){
                    chanceToPass = AM.apprentice.power/reqStat;
                }
                if(quailifier == "Skill"){
                    chanceToPass = AM.apprentice.skill/reqStat;
                }
                if(quailifier == "Confidence"){
                    chanceToPass = AM.apprentice.confidence/reqStat;
                }

                if(chanceToPass*100 >= UnityEngine.Random.Range(0,100)){
            // this check passes
                    continue;
                }
                else{
            // this check fails
                    Debug.Log("Mission failed.");
                    succeedMission = false;
                    failedMissions.Add(mission.missionName);
                    missions.RemoveAt(missionIndex);
                    // update player stats
                    ApprenticeManager.AM.UpdateStatsMission(succeedMission, mission);
                    return succeedMission;
                }
            }
        }
            
        // If it gets through everything without failing, its a success!
        succeedMission = true;
        successfulMissions.Add(mission.missionName);
        missions.RemoveAt(missionIndex);
        // update player stats
        ApprenticeManager.AM.UpdateStatsMission(succeedMission, mission);

        Debug.Log("Mission success.");
        return succeedMission;
    }
}
/*



    TO DO

    [x] Fill three mission cards
        [x] Create mission class
        [x] Create mission card prefab
        [x] Pick three missions to show (no random generation yet)
            [x] Each mission is only available to certain age ranges
            [x] Some missions require having completed others to be shown
    [x] Player can click one
    [x] The other's go away
    [x] There's a beat, and then it tells you if it was a success or failure
    [x] Update stats (maybe tell the player?)
    [x] Show new missions

    [] Missions are failed if you *have* a specific attribute or have a stat that is *too* high

    - Always available
        - Sparring
        - Meditation
        - Education

    Mission Ideas
        - Pilgrimige
        - Collect ingredients for potion
        - Harvest something nasty
        - Summoning ritual
        - Learn what potions do to you the hard way
        - Pretend to die
        - Torture training  
        - Mission chains
            - If they succeed in taming the wild animal -> later, kill it
            - If meditation succeeds -> get new snippets of their vision. You get the sense they're not telling you everything


    
    Loyalty | Power | Skill | Confidence | Age | Attributes

    All Ages
        Place in a room with a wild animal.
        - Skill >= 5
            - Success
                - They bond with it and will cherish it as a dear friend
                - +3 confidence
            - Failure
                - To maims them and they resent wild creatures forever more
                - -2 confidence, -3 loyalty
        Meditate
        - Confidence > 20
        - Skill > 10
            - Success
                - They see new edges of reality and open their eyes with a sense of cool understanding. They tell you they 
                  saw a tower on the violet, desolate surface of a moon. A planet hung behind it.
                - Skill +4, power +2
            - Failure
                - Their mind swirls with confused thoughts of pain and desire. Much time passes, and they open their eyes
                  with muscles tense and covered in a clammy sweat.
                - Confidence -2

    0-5 cycles old
        Leave them in a random part of the castle, see if they can make it back to their room before time runs out
        - Loyalty > 80
        - Skill > 15
            - Success

        Run The Gauntlet for Childeren
        Challenge them to a game of Treats and Talismans
        Bend light
    6-12
        Play catch with no hands
            - you bend light to do stuff
        Run The Gauntlet for Growing Disciples 
        Fly them out into the wilderness and leave them with what the had on them
        Steal a trinket from this market place
    13-18
        Bring in a renown fighter for them to contend with
        Fly them to the moon and leave them with a broken shuttle
        Run the Gauntlet
        Debate this smart guy on some topic
        Hunt down a scary creature -> adult version of the one they can play with as a child
        Kill this guy
        Pretend there's an assault on the Castle by some droids and see what they do
    18+
        Assassinate a senator
        Kidnap someone
        Put down an enclave of other mages
        Help the middle faction deal with a growing band of pirates
        Attempt a coup on a small planet run by bandits/weakish power
        Negotiate a trade deal with a crime lord

*/


