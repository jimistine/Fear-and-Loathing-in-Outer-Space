using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission{
    public string missionName;
    public string location;
    [TextArea(3, 10)]
    public string description;
    public List<string> passReqs;
    public float minAge;
    public float maxAge;
    [TextArea(3, 10)]
    public string successText;
    [TextArea(3, 10)]
    public string failureText;
    [Header("Stat Updates")]
    public List<string> succeed;
    public List<string> fail;
    public List<Mission> followingMission;
}
