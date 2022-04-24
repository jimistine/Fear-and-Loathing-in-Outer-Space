using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;

public class ApprenticeCard : MonoBehaviour
{
    public Apprentice thisApprentice;
    public AudioManager AudMan;
    public Animator cardAnimator;
    public List<Animator> dotAnims;


    void Start()
    {
        AudMan = AudioManager.AudMan;
        thisApprentice = GameManager.GM.AG.GenerateApprentice();
        UpdateApprenticeCard();
    }
    public void AnimateStatDots(string passReqs){
        //dotAnimClip.
        Debug.Log("Scaling Dots");
        MatchCollection matchListRegex = Regex.Matches(passReqs, @"\w+");
        var matchList = matchListRegex.Cast<Match>().Select(match => match.Value).ToList();

        List<string> nameOfStatList = new List<string>();
        List<string> valueOfStatList = new List<string>();

        for(int i = 0; i < matchList.Count(); i++){
            if(i % 2 == 0){
                nameOfStatList.Add(matchList[i]);
            }
            else{
                valueOfStatList.Add(matchList[i]);
            }
        }
        for(int i = 0; i < nameOfStatList.Count(); i++){
            if(nameOfStatList[i] == "Loyalty"){
                dotAnims[0].SetFloat("DotScale", float.Parse(valueOfStatList[i])/10);
            }
            if(nameOfStatList[i] == "Power"){
                dotAnims[1].SetFloat("DotScale", float.Parse(valueOfStatList[i])/10);
            }
            if(nameOfStatList[i] == "Skill"){
                dotAnims[2].SetFloat("DotScale", float.Parse(valueOfStatList[i])/10);
            }
            if(nameOfStatList[i] == "Confidence"){
                dotAnims[3].SetFloat("DotScale", float.Parse(valueOfStatList[i])/10);
            }
        }
    }

    public void AnimateStatDotsDefault(){
        foreach(Animator dotAnimator in dotAnims){
            dotAnimator.SetFloat("DotScale", -1);
        }
    }

    /*
    public IEnumerator SetZoom(Vector3 startP, Vector3 endP, Quaternion startR, Quaternion endR){    
        float timeElapsed = 0;
        while(timeElapsed < moveTime){
            float t = timeElapsed/moveTime;
            // we gon ease on out
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            heldBook.transform.position = Vector3.Lerp(startP, endP, t);
            heldBook.transform.rotation = Quaternion.Lerp(startR, endR, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // snap to end points once time is up
        heldBook.transform.position = endP;
        heldBook.transform.rotation = endR;
    }
    */



    public void UpdateApprenticeCard(){
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
            headerGroup.transform.Find("Attributes").gameObject.GetComponent<TextMeshProUGUI>().text = "Personality unknown";
        }
        else{
            headerGroup.transform.Find("Attributes").gameObject.GetComponent<TextMeshProUGUI>().text = string.Join(", ", thisApprentice.attribtues);
        }  
        statGroup.transform.Find("Skill").gameObject.GetComponent<TextMeshProUGUI>().text = "<b>" + thisApprentice.skill.ToString() + "</b><size=20>" + "/100";
        statGroup.transform.Find("Confidence").gameObject.GetComponent<TextMeshProUGUI>().text = "<b>" + thisApprentice.confidence.ToString() + "</b><size=20>" + "/100";
    }

    public void RecruitApprentice(){
        GameManager.GM.ApprenticeCard = this;
        GameManager.GM.ApprenticeChosen(thisApprentice);
        cardAnimator.SetBool("Chosen", true);
        gameObject.tag = "chosen";
        AudMan.PlayMiscSFX(0);
        foreach(GameObject card in GameObject.FindGameObjectsWithTag("candidate")){
            card.GetComponent<ApprenticeCard>().cardAnimator.SetBool("Remove", true);
        }
    }
    public void ApprenticeButtonAudio(int indexToPass){
        AudMan.PlayButtonSFX(indexToPass);
    }
}
