using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> uiSFX;
    public List<AudioClip> miscSFX;

    public AudioSource UIAudio;
    public AudioSource SFXAudio;

    public static AudioManager AudMan;

    // Start is called before the first frame update
    void Awake()
    {
        AudMan = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayButtonSFX(int indexOfButton){
        UIAudio.PlayOneShot(uiSFX[indexOfButton]);
    }
    public void PlayMiscSFX(int indexOfClip){
        SFXAudio.PlayOneShot(miscSFX[indexOfClip]);
    }
}
