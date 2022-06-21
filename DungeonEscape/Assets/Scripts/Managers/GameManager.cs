using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject continueUI;
    public Text countText;
    public GameObject gameoverUI;
    private int startCount = 9;
    void Start()
    {

    }

    void Update()
    {
        if(DataManager.GetInstance().playerisDie){
            PlayerDie();
        }
    }
    public void OnClickStart(){
        Time.timeScale = !DataManager.GetInstance().isPause ? 0: 1;
        pauseUI.SetActive(!DataManager.GetInstance().isPause);
        DataManager.GetInstance().isPause = !DataManager.GetInstance().isPause;
    }
    public void PlayerDie(){
        continueUI.SetActive(true);
        // TODO countdown
        StartCoroutine("TimerCroutine");
    }
    private IEnumerator TimerCroutine(){
        while(true)
        {
            if(startCount == 0){
                break;
            }
            startCount = startCount - 1;
            yield return new WaitForSeconds(1f);
            countText.text = (startCount).ToString();
            yield return new WaitForSeconds(1f);
        }
    }
}
