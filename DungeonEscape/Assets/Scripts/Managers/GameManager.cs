using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] stages;
    public int stageIndex;
    public GameObject gameoverUI;
    public Text startMessage;
    public GameObject clearUI;
    public AudioClip audioGameOver;
    AudioSource audioSource;
    public Player player;
    private bool gameover = false;

    void Start(){
        StartCoroutine(BlinkText());
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
    void Update()
    {
        if(DataManager.GetInstance().playerisDie){
            if(!gameover){
                // GameOver시 사운드 변경
                audioSource.clip = audioGameOver;
                audioSource.Play();
                GameOver();
                gameover = true;
            }
        }
    }
    private void GameOver(){
        // GameOver UI 노출
        gameoverUI.SetActive(true);
    }
    public void ClearStage(){
        // 클리어 UI 노출
        clearUI.SetActive(true);
    }
    public void NextStage(){
        // 다음 스테이지가 남아있을경우 이동
        if(stageIndex < stages.Length - 1){
            stages[stageIndex].SetActive(false); // 현재 스테이지
            stageIndex++;
            stages[stageIndex].SetActive(true); // 다음 스테이지
            playerReposition();
        } else {
            // 다음 스테이지가 없으면 GameClear
            ClearStage();
        }
    }
    void playerReposition()
    {
        // 플레이어 시작 위치 이동
        player.transform.position = new Vector3(0.5f, 0.0f, -10f);
        player.VelocityZero();
    }
    private IEnumerator BlinkText()
    {
        while (true)
        {
            startMessage.text = "";
            yield return new WaitForSeconds(.5f);
            startMessage.text = "PRESS START!";
            yield return new WaitForSeconds(.5f);
        }
    }
}
