using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGenerator : MonoBehaviour
{

    public List<Mission> missions;
    ApprenticeManager AM;

    void Start()
    {
        AM = ApprenticeManager.AM;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Mission GenerateMission(){
        Mission newMission = new Mission();
        int totalMissions = missions.Count;
        newMission = missions[Random.Range(0, totalMissions)];

        if(AM.apprentice.age < newMission.minAge || AM.apprentice.age > newMission.maxAge){
            while(AM.apprentice.age < newMission.minAge || AM.apprentice.age > newMission.maxAge){
                newMission = missions[Random.Range(0, totalMissions)];
            }
        }
        
        if(AM.apprentice.age <= 5){

        }

        newMission.missionName = GenerateMissionName();

        return newMission;
    }

    public string GenerateMissionName(){
        string missionName = "";

        return missionName;
    }



/*
    TO DO

    [] Fill three mission cards
        [x] Create mission class
        [x] Create mission card prefab
        [] Pick three missions to show (no random generation yet)
            [] Each mission is only available to certain age ranges
            [] Some missions require having completed others to be shown
    [] Player can click one
    [] The other's go away
    [] There's a beat, and then it tells you if it was a success or failure
    [] Update stats (maybe tell the player?)
    [] Show three new missions

    How do we calculate success?

    Skill 20 check
        - 20 -> 100%
        - 10 -> 50% 

    - Always available
        - Sparring
        - Meditation
        - Education

    Mission Ideas
        - Pilgrimige
        - Collect ingredients for potion
        - Summoning ritual
        - Learn what potions do to you the hard way
        - Pretend to die
        - Torture training  
        - Mission chains
            - If they succeed in taming the wild animal -> later, kill it
            - If meditation succeeds -> get new snippets of their vision. You get the sense they're not telling you everything

    Example Mission

    Taming the Crew
    Orbit of Yardaso Prime
    The Ruler of Yardaso Prime has contacted you. He is in need of discrete justice and understands 
    you have a way with such things. The crew of a defensive battle station orbiting the planet has 
    gone rogue, and are threatening to decimate the capital city and its environs if they are not paid
    handsomely. Send your apprentice, and they will take care of the matter.
    
    Loyalty | Power | Skill | Confidence | Age

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

}
