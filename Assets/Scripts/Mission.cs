using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission{
    public string missionName;
    public string location;
    public string description;
    public List<string> passReqs;
    public float minAge;
    public float maxAge;
    public int missionSequenceIndex;
    public List<Mission> followingMission;
}
