using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ApprenticeCard : MonoBehaviour
{
    public Apprentice thisApprentice;
    public Animator cardAnimator;
    void Start()
    {
        thisApprentice = GameManager.GM.AG.GenerateApprentice();
        GameObject headerGroup = gameObject.transform.Find("HeaderGroup").gameObject;
        GameObject statGroup = gameObject.transform.Find("StatGroup").gameObject;
        headerGroup.transform.Find("Candidate Name").gameObject.GetComponent<TextMeshProUGUI>().text = thisApprentice.firstName + " " + thisApprentice.lastName;
        if(thisApprentice.age == 1){
            headerGroup.transform.Find("Age").gameObject.GetComponent<TextMeshProUGUI>().text = thisApprentice.age.ToString() + " cycle old";
        }
        else{
            headerGroup.transform.Find("Age").gameObject.GetComponent<TextMeshProUGUI>().text = thisApprentice.age.ToString() + " cycles old";
        }
        statGroup.transform.Find("Loyalty").gameObject.GetComponent<TextMeshProUGUI>().text = "<b>" + thisApprentice.loyalty.ToString() + "</b><size=20>" + "/100";
        if(thisApprentice.age < 5){
            statGroup.transform.Find("Power").gameObject.GetComponent<TextMeshProUGUI>().text = "<b>NA";
        }
        else{
            statGroup.transform.Find("Power").gameObject.GetComponent<TextMeshProUGUI>().text = "<b>" + thisApprentice.power.ToString() + "</b><size=20>" + "/100";
        }
        if(thisApprentice.age < 10){
            headerGroup.transform.Find("Attributes").gameObject.GetComponent<TextMeshProUGUI>().text = "Unknown";
        }
        else{
            headerGroup.transform.Find("Attributes").gameObject.GetComponent<TextMeshProUGUI>().text = string.Join(", ", thisApprentice.attribtues);
        }
        
        
        statGroup.transform.Find("Skill").gameObject.GetComponent<TextMeshProUGUI>().text = "<b>" + thisApprentice.skill.ToString() + "</b><size=20>" + "/100";
        statGroup.transform.Find("Confidence").gameObject.GetComponent<TextMeshProUGUI>().text = "<b>" + thisApprentice.confidence.ToString() + "</b><size=20>" + "/100";
    }

    public void RecruitApprentice(){
        GameManager.GM.ApprenticeChosen(thisApprentice);
        cardAnimator.SetBool("Chosen", true);
        gameObject.tag = "chosen";
        foreach(GameObject card in GameObject.FindGameObjectsWithTag("candidate")){
            card.GetComponent<ApprenticeCard>().cardAnimator.SetBool("Remove", true);
        }
    }
}
