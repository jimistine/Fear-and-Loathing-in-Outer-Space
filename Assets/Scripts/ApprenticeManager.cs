using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.Linq;


public class ApprenticeManager : MonoBehaviour
{
    public Apprentice apprentice;
    public static ApprenticeManager AM;
    public string missionStatUpdateToCheck;

    [TextArea(2,10)]
    public string testString;

    void Awake(){
        AM = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        MatchCollection matchList = Regex.Matches(testString, @"\w+");
        var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
        for(int i = 0; i < list.Count(); i++){
            // maybe I get all the words and then separate them to two lists from there
            if(i % 2 == 0){
                Debug.Log("Regex returned: " + list[i]);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateStatsMission(bool missionSuccess, Mission mission){
        if(mission.succeed == null || mission.fail == null){
            Debug.Log("No updates to apprentice from mission");
            return;
        }
        if(missionSuccess){
            missionStatUpdateToCheck = mission.succeed;
        }
        else{
            missionStatUpdateToCheck = mission.fail;
        }

        MatchCollection matchList = Regex.Matches(missionStatUpdateToCheck, @"^.*?(?= - )", RegexOptions.Multiline);
        List<string> nameOfStatList = matchList.Cast<Match>().Select(match => match.Value).ToList();
        MatchCollection matchListStats = Regex.Matches(missionStatUpdateToCheck, @"[^ - ]*$", RegexOptions.Multiline);
        List<int> valueOfStatList = matchList.Cast<Match>().Select(match => Int32.Parse(match.Value)).ToList();

        //string nameOfStat = Regex.Match(missionStatUpdateToCheck, @"^.*?(?= - )").Value;
        //float statChangeValue = Int32.Parse(Regex.Match(missionStatUpdateToCheck, @"[^ - ]*$").Value);
        Debug.Log("Count for stat iteration: " + nameOfStatList.Count());
        for(int i = 0; i < nameOfStatList.Count(); i++){
            ApplyStatChange(nameOfStatList[i], valueOfStatList[i]);
        }
        GameManager.GM.ApprenticeCard.UpdateApprenticeCard();
    }

    public void ApplyStatChange(string nameOfStat, float statChangeValue){
        if(nameOfStat == "Loyalty"){
            apprentice.loyalty += statChangeValue;
        }
        else if(nameOfStat == "Power"){
            apprentice.power += statChangeValue;
        }
        else if(nameOfStat == "Skill"){
            apprentice.skill += statChangeValue;
        }
        else if(nameOfStat == "Confidence"){
            apprentice.confidence += statChangeValue;
        }
        else if(apprentice.attribtues.Contains(nameOfStat)){
            int attributeToRemoveIndex = apprentice.attribtues.IndexOf(nameOfStat);
            apprentice.attribtues.RemoveAt(attributeToRemoveIndex);
        }
        else{
            apprentice.attribtues.Add(nameOfStat);
        }
        Debug.Log("Added " + statChangeValue + " to " + nameOfStat);
    }
}
