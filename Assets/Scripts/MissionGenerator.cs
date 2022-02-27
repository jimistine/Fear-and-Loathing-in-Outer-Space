using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Mission GenerateMissions(){
        Mission newMission = new Mission();
        newMission.missionName = GenerateMissionName();


        return newMission;
    }

    public string GenerateMissionName(){
        string missionName = "";

        return missionName;
    }



/*
    Mission Ideas

    - Put down an enclave of other mages
    - Help the middle faction deal with a growing band of pirates
    - Attempt a coup on a small planet run by bandits/weakish power
    - Negotiate a trade deal

    - Should there be a generic set of options that are gaurenteed?
        - ie combat training, meditation,general education


*/

}
