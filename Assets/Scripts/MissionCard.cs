using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCard : MonoBehaviour
{
    public Mission thisMission;
    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            thisMission = GameManager.GM.MG.GenerateMission();
        }
    }
}
