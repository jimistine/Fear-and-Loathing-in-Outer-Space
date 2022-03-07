using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public ApprenticeManager AM;
    public ApprenticeGenerator AG;
    public ApprenticeCard ApprenticeCard;
    public int timePassed;
    public MissionManager MM;
    public GameObject proceedButton;
    public TextMeshProUGUI statusText;
    // Events don't do anything, but functions can listen to them so they run when that event is invoked
    public UnityEvent onMissionResolved;
    public UnityEvent noMissionsAvailable;

    void Awake(){
        GM = this;
        onMissionResolved.AddListener(ToggleProceedButton);
        noMissionsAvailable.AddListener(PassTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Proceed(){
        PassTime();
        ApprenticeCard.UpdateApprenticeCard();
        if(MM.CheckMissions() == 0){
            Debug.Log("No missions to show");
            statusText.text = "The galaxy sleeps easily for there is naught to do but pass the time away. Proceed.";
        }
        else{
            Debug.Log("Showing missions");
            proceedButton.SetActive(false);
            MM.ShowMissions();
        }
    }
    public void ApprenticeChosen(Apprentice newApprentice){
        AM.apprentice = newApprentice;
        statusText.text = "WHAT WILL YOU ASK OF THEM?";
        Proceed();  
    }
    public void PassTime(){
        timePassed ++;
        if(timePassed % 3 == 0){
            AM.apprentice.age += 1;
            Birthday();
        }
    }
    public void ToggleProceedButton(){
        if(proceedButton.activeSelf){
            proceedButton.SetActive(false);
        }
        else{
            proceedButton.SetActive(true);
        }
    }

    public void Birthday(){
        statusText.text = "Happy birthday, my apprentice.";
    }

    /*

    Left off: Mission manager needs to check to see if there are any viable missions before trying to show any
        Also, baby age 0 did older folks mission...


    TO DO
    [x] Create Apprentice class
        Info
            Name
            Age
            Loyalty
            Power
            Confidence
            Skill (?)
            Attributs<string> -> can use this to throw in adjectives that get read in to affect certain rolls

    [x] Procudurally generate apprentice candidates
        [x] Find way to make totally procedural names or pick first and last names from big bucketts
        [x] The older, the more you know about them and the more skilled they tend to be, but they start off less loyal
            - 0-5   -> Name, age, some attributes if based on heritage/location
            - 6-12  -> Name, age, all attributes, power
            - 13-18 -> Name, age, all attributes, power, confidence, skill
        [x] How to actually assign stats?
            Each out of 100
            36 starting points assigned randomly to
                - Power
                - Skill
                - Confidence
            Loyalty is a direct function of age to start off
                - 100 - (age*5) = starting loyalty
        [x] Populate their info into card prefabs
    
    [x] Select an apprentice
    [x] Get shown a variety of missions to send them on
    [x] Send them on a mission
    [x] Get results
        - What happened?
        - Was it a success or failure?
        - Update Apprentice stats/attributes
    [x] Get more missions
    [ ] populate missions
    */
}
