using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class ApprenticeManager : MonoBehaviour
{
    public Apprentice apprentice;
    public static ApprenticeManager AM;
    List<StatCheckChange> missionStatUpdateToCheck;

    void Awake(){
        AM = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
        foreach(StatCheckChange missionResult in missionStatUpdateToCheck){
            if(missionResult.nameOfStat == "Loyalty"){
                apprentice.loyalty += missionResult.value;
            }
            else if(missionResult.nameOfStat == "Power"){
                apprentice.power += missionResult.value;
            }
            else if(missionResult.nameOfStat == "Skill"){
                apprentice.skill += missionResult.value;
            }
            else if(missionResult.nameOfStat == "Confidence"){
                apprentice.confidence += missionResult.value;
            }
            else if(apprentice.attribtues.Contains(missionResult.nameOfStat)){
                int attributeToRemoveIndex = apprentice.attribtues.IndexOf(missionResult.nameOfStat);
                apprentice.attribtues.RemoveAt(attributeToRemoveIndex);
            }
            else{
                apprentice.attribtues.Add(missionResult.nameOfStat);
            }
        }
        GameManager.GM.ApprenticeCard.UpdateApprenticeCard();
    }
}
