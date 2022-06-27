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
    public Text clearText;
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
                audioSource.clip = audioGameOver;
                audioSource.Play();
                GameOver();
                gameover = true;
            }
        }
    }
    private void GameOver(){
        gameoverUI.SetActive(true);
    }
    public void ClearStage(){
        clearUI.SetActive(true);
    }
    public void NextStage(){
        if(stageIndex < stages.Length - 1){
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            playerReposition();
        } else {
            ClearStage();
        }
    }
    void playerReposition()
    {
        player.transform.position = new Vector3(0.5f, 2.0f, 0);
        player.VelocityZero();
        //cameraFollow.SetInitPosition();
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
