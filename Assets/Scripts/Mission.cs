using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission{
    public string missionName;
    public string location;
    public float minAge;
    public float maxAge;
    [TextArea(3, 10)]
    public string description;
    [TextArea(2, 10)]
    public string passReqs;
    [TextArea(2, 10)]
    public string succeed;
    [TextArea(2, 10)]
    public string fail;
    [TextArea(3, 10)]
    public string successText;
    [TextArea(3, 10)]
    public string failureText;
    public string previousMission;
    public string followingMission;
    //public List<Mission> followingMission;
}
