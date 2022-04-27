using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class MissionCard : MonoBehaviour
{
    public Mission thisMission;
    public GameObject okButton;
    public Animator missionCardAnimator;
    public Transform headerGroup;
    public AudioManager AM;
    public ApprenticeCard appCard;
    public bool missionSuccess;

    void Awake(){
        AM = AudioManager.AudMan;
        appCard = GameManager.GM.ApprenticeCard;
        // grab the mission
        if(GameManager.GM.MM.CheckMissions() == 0 ){
            Destroy(this.gameObject);
        }
        else{
            thisMission = GameManager.GM.MM.GenerateMission();
            missionCardAnimator.SetBool("Awake", true);
            if(thisMission != null){
                headerGroup = gameObject.transform.Find("HeaderGroup");
                headerGroup.Find("Mission Name").GetComponent<TextMeshProUGUI>().text = thisMission.missionName;
                // Location
                headerGroup.Find("Location").GetComponent<TextMeshProUGUI>().text = thisMission.location;
                // Desc
                thisMission.description = thisMission.description.Replace("Darth", ApprenticeManager.AM.apprentice.firstName);
                headerGroup.Find("Description").GetComponent<TextMeshProUGUI>().text = thisMission.description;
                // Change the other description texts while we're here
                thisMission.successText = thisMission.successText.Replace("Darth", ApprenticeManager.AM.apprentice.firstName);
                thisMission.failureText = thisMission.failureText.Replace("Darth", ApprenticeManager.AM.apprentice.firstName);
            }
        }

    }
    public void ScaleStatDots(){
        string passReqsHover;
        passReqsHover = thisMission.passReqs;
        Debug.Log("Pass reqs are: " + passReqsHover);
        appCard.AnimateStatDots(passReqsHover);
    }
    public void ResetStatDots(){
        appCard.AnimateStatDotsDefault();
    }
    public void SendToResolve(){
        // Figure out if you succeeded and update the card accordingly
        missionSuccess = GameManager.GM.MM.ResolveMission(thisMission);
        Debug.Log("Mission: " + thisMission.missionName + " passed -> "  + missionSuccess);

        missionCardAnimator.SetBool("MissionSelected", true);

        // we update the desc text on the animator behavior
        if(missionSuccess){
            thisMission.successText += "\n" + "<color=green>" + thisMission.succeed + "</color>";
            thisMission.successText = thisMission.successText.Replace("AddAttribute", "+Attribute: ");
            thisMission.successText = thisMission.successText.Replace("SubAttribute", "-Attribute: ");
            missionCardAnimator.SetBool("MissionSucceed", true);
        }
        else{
            thisMission.failureText += "\n" + "<color=red>" + thisMission.fail + "</color>";
            thisMission.failureText = thisMission.failureText.Replace("AddAttribute", "+Attribute: ");
            thisMission.failureText = thisMission.failureText.Replace("SubAttribute", "-Attribute: ");
            missionCardAnimator.SetBool("MissionSucceed", false);
        }
        // Turn on button to proceede to the next set of missions
        GameManager.GM.onMissionResolved.Invoke();
        gameObject.tag = "chosenMission";
        foreach(GameObject card in GameObject.FindGameObjectsWithTag("missionCard")){
            card.GetComponent<MissionCard>().missionCardAnimator.SetBool("Visible", false);
            card.SetActive(false);
        }
    }
    public void playMissionCardSFX(int indexToPass){
        AM.PlayButtonSFX(indexToPass);
    }
}
