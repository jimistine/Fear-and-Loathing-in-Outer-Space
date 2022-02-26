using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    TO DO
    [] Create Apprentice class
        Info
            Name
            Age
            Loyalty
            Power
            Confidence
            Skill (?)
            Attributs<string> -> can use this to throw in adjectives that get read in to affect certain rolls

    [] Procudurally generate apprentice candidates
        [] Find way to make totally procedural names or pick first and last names from big bucketts
        [] Age between 0 and 18, the older, the more you know about them, but the less loyal they start off being
            - 0-5   -> Name, age, some attributes if based on heritage/location
            - 6-12  -> Name, age, all attributes, power
            - 13-18 -> Name, age, all attributes, power, confidence, skill
        [] How to actually assign stats?
            Each out of 100
            36 starting points assigned randomly to
                - Power
                - Skill
                - Confidence
            Loyalty is a direct function of age to start off
                - 100 - (age*5) = starting loyalty
        [] Populate their info into card prefabs
    */
}
