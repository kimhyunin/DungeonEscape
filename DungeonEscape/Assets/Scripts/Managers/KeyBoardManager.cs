using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KeyBoardManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject mainUI;
    public GameObject quitMessageUI;
    public GameObject black;
    private bool menuOn = false;
    private bool pauseOn = false;

    // Mobile Key Var
    [HideInInspector]
    public int up_value { get; set; }
    public int down_value { get; set; }
    public int left_value { get; set; }
    public int right_value { get; set; }
    public int a_value { get; set; }
    public int b_value { get; set; }

    public bool up_Down { get; set; }
    public bool down_Down { get; set; }
    public bool left_Down { get; set; }
    public bool right_Down { get; set; }
    public bool up_Up { get; set; }
    public bool down_Up { get; set; }
    public bool left_Up { get; set; }
    public bool right_Up { get; set; }
    public bool a_Up { get; set; }
    public bool a_Down { get; set; }
    public bool b_Up { get; set; }
    public bool b_Down { get; set; }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_value = 1;
                up_Up = false;
                up_Down = true;
                break;
            case "D":
                down_value = -1;
                down_Up = false;
                down_Down = true;
                break;
            case "L":
                left_value = -1;
                left_Up = false;
                left_Down = true;
                break;
            case "R":
                right_value = 1;
                right_Up = false;
                right_Down = true;
                break;
            case "A":
                a_value = 1;
                a_Up = false;
                a_Down = true;
                break;
            case "B":
                b_value = 1;
                b_Up = false;
                b_Down = true;
                break;

        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_value = 0;
                up_Up = true;
                up_Down = false;
                break;
            case "D":
                down_value = 0;
                down_Up = true;
                down_Down = false;
                break;
            case "L":
                left_value = 0;
                left_Up = true;
                left_Down = false;
                break;
            case "R":
                right_value = 0;
                right_Up = true;
                right_Down = false;
                break;
            case "A":
                a_value = 0;
                a_Up = true;
                a_Down = false;
                break;

            case "B":
                b_value = 0;
                b_Up = true;
                b_Down = false;
                break;

        }
    }

    public void onClickMenu()
    {
        if(DataManager.GetInstance().isStart){
            if(pauseOn){
                pauseUI.SetActive(false);
            }
            menuOn = true;
            pause(true);
            quitMessageUI.SetActive(true);
        }
    }

    public void onClickQuitYes()
    {
        pause(false);
        SceneManager.LoadScene(0);
        pauseOn = false;
        menuOn = false;
        // DataManager.GetInstance().isStart = false;
    }

    public void onClickQuitNo()
    {
        pause(false);
        quitMessageUI.SetActive(false);
        pauseOn = false;
        menuOn = false;
    }

    public void OnClickStart(){
        if(DataManager.GetInstance().isStart && !DataManager.GetInstance().playerisDie){ // 게임 시작이 되지 않았을 경우 일시정지
            if(!menuOn){
                Time.timeScale = !DataManager.GetInstance().isPause ? 0: 1;
                pauseUI.SetActive(!DataManager.GetInstance().isPause);
                DataManager.GetInstance().isPause = !DataManager.GetInstance().isPause;
                pauseOn = !pauseOn;
            }
        } else if(!DataManager.GetInstance().isStart && !DataManager.GetInstance().playerisDie) { // 게임스타트
            black.SetActive(true);
            mainUI.SetActive(false);
            DataManager.GetInstance().isStart = true;
        } else if(DataManager.GetInstance().isStart && DataManager.GetInstance().playerisDie){
            SceneManager.LoadScene(0);
        }
    }

    private void pause(bool value){
        Time.timeScale = value ? 0 : 1;
        DataManager.GetInstance().isPause = value? true : false;
    }

}
