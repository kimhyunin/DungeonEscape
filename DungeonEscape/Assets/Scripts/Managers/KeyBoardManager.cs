using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KeyBoardManager : MonoBehaviour
{
    public GameObject pauseUI; // 일시정지UI
    public GameObject mainUI; //메인 UI
    public GameObject quitMessageUI; // 종료문구 UI
    public GameObject black; // 시작효과 UI
    private bool menuOn = false; // 메뉴 UI 켜짐 유무
    private bool pauseOn = false; // 일시정지 UI 켜짐 유무

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
            case "U": // UP KEY
                up_value = 1;
                up_Up = false;
                up_Down = true;
                break;
            case "D": // DOWN KEY
                down_value = -1;
                down_Up = false;
                down_Down = true;
                break;
            case "L": // LEFT KEY
                left_value = -1;
                left_Up = false;
                left_Down = true;
                break;
            case "R": // RIGHT KEY
                right_value = 1;
                right_Up = false;
                right_Down = true;
                break;
            case "A": // A KEY
                a_value = 1;
                a_Up = false;
                a_Down = true;
                break;
            case "B": // B KEY
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
            case "U": // UP KEY
                up_value = 0;
                up_Up = true;
                up_Down = false;
                break;
            case "D": // DOWN KEY
                down_value = 0;
                down_Up = true;
                down_Down = false;
                break;
            case "L": // LEFT KEY
                left_value = 0;
                left_Up = true;
                left_Down = false;
                break;
            case "R": // RIGHT KEY
                right_value = 0;
                right_Up = true;
                right_Down = false;
                break;
            case "A": // A KEY
                a_value = 0;
                a_Up = true;
                a_Down = false;
                break;
            case "B": // B KEY
                b_value = 0;
                b_Up = true;
                b_Down = false;
                break;
        }
    }

    public void onClickMenu()
    {
        if(DataManager.GetInstance().isStart){
            if(pauseOn){ // pause 화면이 노출 되었을 경우
                pauseUI.SetActive(false);
            }
            menuOn = true;
            pause(true); // 일시정지
            quitMessageUI.SetActive(true);
        }
    }

    public void onClickQuitYes()
    {
        pause(false); // 개임재개
        SceneManager.LoadScene(0); // 게임종료
    }

    public void onClickQuitNo()
    {
        pause(false); // 게임재개
        quitMessageUI.SetActive(false);
        pauseOn = false;
        menuOn = false;
    }
    public void OnClickStart(){
        // 게임이 시작 되었을 경우 일시정지
        if(DataManager.GetInstance().isStart && !DataManager.GetInstance().playerisDie){
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
        } else if(DataManager.GetInstance().isStart && DataManager.GetInstance().playerisDie){ // GAMEOVER
            SceneManager.LoadScene(0);
        }
    }
    private void pause(bool value){
        Time.timeScale = value ? 0 : 1;
        DataManager.GetInstance().isPause = value? true : false;
    }
}
