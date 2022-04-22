using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public ApprenticeManager AM;
    public ApprenticeGenerator AG;
    public AudioManager AudMan;
    public ApprenticeCard ApprenticeCard;
    public int timePassed;
    public MissionManager MM;
    public GameObject proceedButton;
    public TextMeshProUGUI statusText;
    public List<string> birthdayWishes;
    [TextArea(3, 10)]
    public List<string> gameOverLoyalty;
    [TextArea(3, 10)]
    public List<string> gameOverSkill;
    [TextArea(3, 10)]
    public List<string> gameOverPower;
    [TextArea(3, 10)]
    public List<string> gameOverConfidence;
    public GameObject restartButton;
    int count = 0;
    // Events don't do anything, but functions can listen to them so they run when that event is invoked
    public UnityEvent onMissionResolved;
    public UnityEvent noMissionsAvailable;
    [Space(5)]
    [Header("UI")]
    public GameObject startScreen;
    public GameObject blackCover;



    void Awake(){
        GM = this;
        onMissionResolved.AddListener(ToggleProceedButton);
        onMissionResolved.AddListener(GameOverCheck);
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
            AudMan.PlayButtonSFX(2);
        }
    }
    public void ApprenticeChosen(Apprentice newApprentice){
        AM.apprentice = newApprentice;
        statusText.text = "WHAT WILL YOU ASK OF THEM?";
        Proceed();  
    }
    public void PassTime(){
        timePassed ++;
        if(timePassed % 2 == 0){
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
        ApprenticeCard.UpdateApprenticeCard();
        AudMan.PlayMiscSFX(0);
        //birthdayWishes.Count(); i++)
        statusText.text = birthdayWishes[count];
        count++;
        if(count == birthdayWishes.Count){
            count = 0;
        }
    }
    public void GameOverCheck(){
        if(AM.apprentice.loyalty <= 0){
            string gameOverText = gameOverLoyalty[Random.Range(0, gameOverLoyalty.Count)];
            gameOverText = gameOverText.Replace("Darth", AM.apprentice.firstName);
            statusText.text = gameOverText;

            GameOver();
        }
        else if(AM.apprentice.power <= 0){
            string gameOverText = gameOverPower[Random.Range(0, gameOverPower.Count)];
            gameOverText = gameOverText.Replace("Darth", AM.apprentice.firstName);
            statusText.text = gameOverText;

            GameOver();
        }
        else if(AM.apprentice.skill <= 0){
            string gameOverText = gameOverSkill[Random.Range(0, gameOverSkill.Count)];
            gameOverText = gameOverText.Replace("Darth", AM.apprentice.firstName);
            statusText.text = gameOverText;

            GameOver();
        }
        else if(AM.apprentice.confidence <= 0){
            string gameOverText = gameOverConfidence[Random.Range(0, gameOverConfidence.Count)];
            gameOverText = gameOverText.Replace("Darth", AM.apprentice.firstName);
            statusText.text = gameOverText;

            GameOver();
        }

    }
    public void StartGame(){
        startScreen.SetActive(false);
        blackCover.GetComponent<CoverController>().FadeOut();
        AudMan.PlayButtonSFX(1);
        AudMan.PlayMiscSFX(1);
    }
    public void GameOver(){
        restartButton.SetActive(true);
        AudMan.PlayMiscSFX(1);
        MM.missionHolder.SetActive(false);
    }
    public void RestartGame(){
        //SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        // Reset missions
        ResetMissions();
        // Reset apprentice
        // Show new apprentice options
        // Maintain years passed
        // Maintain number of apprentices
        StartGame();
    }
    public void ResetMissions(){
        MM.InitMissions();
    }
    public void LoopGame(){

    }

    /*


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
    [x] populate missions
    [x] Missions are read in from CSV
    [x] Missions in random order
    [] Win state
        [] older than 18 and ran out of missions to do
        [] restart at apprentice select
        [] keep track of what apprentice this is
        [] keep track of years passed
    [] Audio
        [x] Ambient
        [x] Hover
        [x] Click
        [x] New cards
        [] Resolving mission
            [] Success
            [] Failure
        [] Moving apprentice over
        [x] Birthday
        [x] Lose game
    [] More missions
        - How many to support each age range? -> 12
        current tally
        0-5    : 4
        6-11   : 6
        12-17  : 8
        18+    : 6
        other  : 3s
        Total  : 27
    [] More clarity in stats being checked / Mission chance of success is shown
    [] Missions show when more than one stat is updated
    [] More room to read missions
    */
}
