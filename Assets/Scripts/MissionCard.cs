using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MissionCard : MonoBehaviour
{
    public Mission thisMission;
    public GameObject okButton;
    public Animator missionCardAnimator;
    Transform headerGroup;

    void Awake(){
        // grab the mission
        if(GameManager.GM.MM.CheckMissions() == 0 ){
            Destroy(this.gameObject);
        }
        else{
            thisMission = GameManager.GM.MM.GenerateMission();
            if(thisMission != null){
                headerGroup = gameObject.transform.Find("HeaderGroup");
                headerGroup.Find("Mission Name").GetComponent<TextMeshProUGUI>().text = thisMission.missionName;
                // Location
                headerGroup.Find("Location").GetComponent<TextMeshProUGUI>().text = thisMission.location;
                // Desc
                thisMission.description = thisMission.description.Replace("your apprentice", ApprenticeManager.AM.apprentice.firstName);
                thisMission.description = thisMission.description.Replace("Darth", ApprenticeManager.AM.apprentice.firstName);
                headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.description;
                // Chance of success
                //headerGroup.Find("Chance of success").GetComponent<TextMeshProUGUI>().text = thisMission.successRate;
            }
        }

    }
    public void SendToResolve(){
        
        // Figure out if you succeeded and update the card accordingly
        bool missionSuccess = GameManager.GM.MM.ResolveMission(thisMission);

        missionCardAnimator.SetBool("MissionSelected", true);

        if(missionSuccess){
            thisMission.successText += "\n" + thisMission.succeed[0].nameOfStat + " +" + thisMission.succeed[0].value;
            headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.successText;
            missionCardAnimator.SetBool("MissionSucceed", true);
        }
        else{
            headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.failureText;
            missionCardAnimator.SetBool("MissionSucceed", false);
        }
        // Turn on button to proceede to the next set of missions
        GameManager.GM.onMissionResolved.Invoke();
        gameObject.tag = "chosenMission";
        foreach(GameObject card in GameObject.FindGameObjectsWithTag("missionCard")){
            card.GetComponent<MissionCard>().missionCardAnimator.SetBool("Visible", false);
        }
        //GameManager.GM.proceedButton.SetActive(true);
        //okButton.SetActive(true);
    }

    // public void Proceed(){
    //     GameManager.GM.MM.ShowMissions();
    // }
    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
