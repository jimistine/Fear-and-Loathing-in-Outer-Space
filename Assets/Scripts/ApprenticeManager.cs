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
        // MatchCollection matchList = Regex.Matches(testString, @"\w+");
        // var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
        // for(int i = 0; i < list.Count(); i++){
        //     // maybe I get all the words and then separate them to two lists from there
        //     if(i % 2 == 0){
        //         Debug.Log("Regex returned on mod 2: " + list[i]);
        //     }
        //     else{
        //         Debug.Log("Regex returned on else: " + list[i]);
        //     }
        // }
        
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
        //Debug.Log("Mission updates to check: " + missionStatUpdateToCheck);

        MatchCollection matchListRegex = Regex.Matches(missionStatUpdateToCheck, @"\w+");
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
            Debug.Log("Applying: " + nameOfStatList[i] + " " + valueOfStatList[i]);
            ApplyStatChange(nameOfStatList[i], valueOfStatList[i], missionSuccess);
        }
        GameManager.GM.ApprenticeCard.UpdateApprenticeCard();
    }

    public void ApplyStatChange(string nameOfStat, string statChangeValue, bool missionSuccess){
        if(nameOfStat  == "AddAttribute"){
            if(!apprentice.attribtues.Contains(statChangeValue)){
                apprentice.attribtues.Add(statChangeValue);
            }
        }
        else if(nameOfStat == "SubAttribute"){
            if(apprentice.attribtues.Contains(statChangeValue)){
                int indexOfAttribute = apprentice.attribtues.FindIndex(x => x.Contains(statChangeValue));
                apprentice.attribtues.RemoveAt(indexOfAttribute);
            }
        }
        else{
            float statChangeValueNum = float.Parse(statChangeValue);
            if(!missionSuccess){
                statChangeValueNum *= -1;
            }
            if(nameOfStat == "Loyalty"){
                apprentice.loyalty += statChangeValueNum;
            }
            else if(nameOfStat == "Power"){
                apprentice.power += statChangeValueNum;
            }
            else if(nameOfStat == "Skill"){
                apprentice.skill += statChangeValueNum;
            }
            else if(nameOfStat == "Confidence"){
                apprentice.confidence += statChangeValueNum;
            }
        }
        Debug.Log("Added " + statChangeValue + " to " + nameOfStat);
    }
}
