using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public ApprenticeManager AM;
    //public Apprentice currentApprentice;
    public ApprenticeGenerator AG;
    public MissionGenerator MG;

    void Awake(){
        GM = this;
        //AM = ApprenticeManager.AM;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApprenticeChosen(Apprentice newApprentice){
        //currentApprentice = newApprentice;
        AM.apprentice = newApprentice;
        //MG.GenerateMission();
        
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
    [] Get shown a variety of missions to send them on
    [] Send them on a mission
    [] Get results
        - What happened?
        - Was it a success or failure?
        - Update Apprentice stats/attributes
    [] Get more missions
    */
}
