using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Memory;

    public GameManager GM;
    public ApprenticeManager AM;
    public List<string> livingApprentices;
    public List<string> deadApprentices;
    public int yearsPassed;

    void Awake(){
        Memory = this;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
    }
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.GM;
        AM = ApprenticeManager.AM;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoopGame(){
        SceneManager.UnloadSceneAsync("MainScene");
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
    }
    public void UpdateApprenticeList(){
        GM = GameManager.GM;
        AM = ApprenticeManager.AM;
        GameObject listGroup = GM.ApprenticeCard.transform.Find("ListGroup").gameObject;
        if(livingApprentices.Count != 0){
            string newLivingList = "<color=red>Living</color>";
            foreach(string apprentice in livingApprentices){
                newLivingList += "\n" + apprentice;
            }
            listGroup.transform.Find("LivingApprentices").gameObject.GetComponent<TextMeshProUGUI>().text = newLivingList;
        }
        if(deadApprentices.Count != 0){
            string newDeadList = "<color=red>Failed</color>";
            foreach(string apprentice in deadApprentices){
                newDeadList += "\n" + apprentice;
            }
            listGroup.transform.Find("FailedApprentices").gameObject.GetComponent<TextMeshProUGUI>().text = newDeadList;
        }
    }
}
/*
    What do we need to remember?
      - Number of apprentices
        - Living vs. Dead
      - Years passed


*/

       
