using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MissionCard : MonoBehaviour
{
    public Mission thisMission;

    void Awake(){
        // grab the mission
        thisMission = GameManager.GM.MM.GenerateMission();
        Transform headerGroup = gameObject.transform.Find("HeaderGroup");
        headerGroup.Find("Mission Name").GetComponent<TextMeshProUGUI>().text = thisMission.missionName;
        // Location
        headerGroup.Find("Location").GetComponent<TextMeshProUGUI>().text = thisMission.location;
        // Desc
        headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.description;
        // Chance of success
        //headerGroup.Find("Chance of success").GetComponent<TextMeshProUGUI>().text = thisMission.successRate;

    }
    public void SendToResolve(){
        // Figure out if you succeeded and update the card accordingly
        bool missionSuccess = GameManager.GM.MM.ResolveMission(thisMission);
        if(missionSuccess){
            Transform headerGroup = gameObject.transform.Find("HeaderGroup");
            headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.successText;
        }
        else{
            Transform headerGroup = gameObject.transform.Find("HeaderGroup");
            headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.failureText;
        }
        // Update apprentice stats -> this on mission manager?
        // Turn on button to procede to the next set of missions
    }

    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
