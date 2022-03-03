using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MissionCard : MonoBehaviour
{
    public Mission thisMission;
    public GameObject okButton;
    Transform headerGroup;

    void Awake(){
        // grab the mission
        thisMission = GameManager.GM.MM.GenerateMission();
        headerGroup = gameObject.transform.Find("HeaderGroup");
        headerGroup.Find("Mission Name").GetComponent<TextMeshProUGUI>().text = thisMission.missionName;
        // Location
        headerGroup.Find("Location").GetComponent<TextMeshProUGUI>().text = thisMission.location;
        // Desc
        headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.description;
        // Chance of success
        //headerGroup.Find("Chance of success").GetComponent<TextMeshProUGUI>().text = thisMission.successRate;

    }
    public void SendToResolve(){
        // Pass the time so we age up the apprentice before showing results
        GameManager.GM.PassTime();
        // Figure out if you succeeded and update the card accordingly
        bool missionSuccess = GameManager.GM.MM.ResolveMission(thisMission);
        if(missionSuccess){
            thisMission.successText += "\n" + thisMission.succeed[0].nameOfStat + " +" + thisMission.succeed[0].value;
            headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.successText;
        }
        else{
            headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.failureText;
        }
        // Turn on button to proceede to the next set of missions
        okButton.SetActive(true);
    }

    public void Proceed(){
        GameManager.GM.MM.ShowMissions();
    }
    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
