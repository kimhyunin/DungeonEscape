using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    private DataManager() {}
    private static DataManager instance = null;
    public static DataManager GetInstance(){
        if(instance == null)
        {
            instance = new DataManager();
        }
        return instance;
    }

    public int totalPoint; // 전체점수
    public int stagePoint; // 스테이지 점수
    public int health; // 체력
    public Image[] UIhealth; // 체력 이미지 배열
    public Sprite whiteHealth; // 체력 흰색 이미지
    public TextMeshProUGUI UIPoint; // 포인트 Text
    public bool playerisDie {get; set;} // 플레이어 사망 여부
    public bool isPause {get; set;} // 일시정지 여부
    public bool isStart {get; set;} // 게임시작 여부

    public void Awake(){
        instance = this;
    }

    void Update()
    {
        // 포인트 업데이트
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void HealthDown(){
        health--;
        UIhealth[health].sprite = whiteHealth; // UISprite 바꾸기
        if(health == 0){
            playerisDie = true;
        }
    }
}
