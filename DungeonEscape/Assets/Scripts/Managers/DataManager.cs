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

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public Image[] UIhealth;
    public Sprite whiteHealth;
    public Sprite RedHealth;
    public Player player;
    public GameObject[] stages;
    public TextMeshProUGUI UIPoint;
    public bool playerisDie {get; set;}
    public bool isPause {get; set;}
    public bool isStart {get; set;}

    public void Awake(){
        instance = this;
    }

    void Start()
    {
        playerisDie = false;
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
