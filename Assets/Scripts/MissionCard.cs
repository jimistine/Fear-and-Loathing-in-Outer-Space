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

        // populate the card
    }

    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
