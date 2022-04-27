using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ApprenticeGenerator : MonoBehaviour
{
    public List<string> nameSyllables;
    public List<string> possibleAttributes;
    int newAge;
    float newSkill;
    float newPower;
    string output;

    void Start(){
        // foreach(string attribute in possibleAttributes){
        //     output = output + "\n" + attribute;
        //     Debug.Log(output);
        // }
    }

    public Apprentice GenerateApprentice(){
        Apprentice newApprentice = new Apprentice();
        newAge = Random.Range(0, 18);
        newApprentice.attribtues = new List<string>();

        newApprentice.firstName = GenerateApName("first");
        newApprentice.lastName = GenerateApName("last");
        newApprentice.age = newAge;
        newApprentice.loyalty = CalculateStartingLoyalty();
        newApprentice.power = CalculateStartingPower();
        newApprentice.skill = CalculateStartingSkill();
        newApprentice.confidence = CalculateStartingConfidence();
        AssignStartingAttributes(newApprentice);

        return newApprentice;
    }

    public string GenerateApName(string nameType){
        string newName = "";
        int nameRange = nameSyllables.Count;
        if(nameType == "first"){
            int syllableCount = Random.Range(1, 4);
            for(int i = 0; i < syllableCount; i++){
                newName += nameSyllables[Random.Range(0,nameRange)];
            }
        }
        else if(nameType == "last"){
            int syllableCount = Random.Range(2, 5);
            for(int i = 0; i < syllableCount; i++){
                newName += nameSyllables[Random.Range(0,nameRange)];
            }
        }
        string nameFormatted = char.ToUpper(newName[0]).ToString() + newName.Substring(1);
        return nameFormatted;
    }
    public float CalculateStartingLoyalty(){
        float startingLoyalty = 100f - (newAge * 3);
        return startingLoyalty;
    }
    public float CalculateStartingPower(){
        float startingPower = Random.Range(10,25);
        newPower = startingPower;
        return startingPower;
    }
    public float CalculateStartingSkill(){
        float startingSkill = Mathf.Round(newAge * Random.Range(0.75f, 1.25f));
        newSkill = startingSkill;
        return startingSkill;
    }
    public float CalculateStartingConfidence(){
        float startingConfidence = Mathf.Round((newPower + newSkill) / Random.Range(2, 2.25f));
        return startingConfidence;
    }
    public void AssignStartingAttributes(Apprentice newApprentice){
        int startingAttributeCount = Random.Range(1,3);
        for (int i = 0; i < startingAttributeCount; i++){
            string attributeToAdd = possibleAttributes[Random.Range(0, possibleAttributes.Count)];

            if(newApprentice.attribtues != null){
                if(newApprentice.attribtues.Contains(attributeToAdd)){
                    while(newApprentice.attribtues.Contains(attributeToAdd)){
                        attributeToAdd = possibleAttributes[Random.Range(0, possibleAttributes.Count)];
                    }
                }
                newApprentice.attribtues.Add(attributeToAdd);
            }
            else{
                newApprentice.attribtues.Add(attributeToAdd);
            }
        }
    }

    /*
    [x] Procudurally generate apprentice candidates
        [x] Find way to make totally procedural names or pick first and last names from big bucketts
        [x] Age between 0 and 18, the older, the more you know about them, but the less loyal they start off being
            - 0-5   -> Name, age, some attributes if based on heritage/location
            - 6-12  -> Name, age, all attributes, power
            - 13-18 -> Name, age, all attributes, power, confidence, skill
        [x] How to actually assign stats?
            Each out of 100
                - Power      -> between 20-40
                - Skill      -> age * random num between 1.25 and 2
                - Confidence -> (power + skill)/ random range 2-2.5
            Loyalty is a direct function of age to start off
                - 100 - (age*5) = starting loyalty
        [x] Populate their info into card prefabs
    */
}
