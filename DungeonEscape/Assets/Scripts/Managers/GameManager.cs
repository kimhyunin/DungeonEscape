using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject continueUI;
    public Text countText;
    public GameObject gameoverUI;
    public Text startMessage;
    private float startCount = 9f;

    void Start(){
        StartCoroutine(BlinkText());
    }
    void Update()
    {
        if(DataManager.GetInstance().playerisDie){
            PlayerDie();
        }
    }
    public void PlayerDie(){
        if(startCount > -1){
            continueUI.SetActive(true);
            startCount -= Time.deltaTime;
            countText.text = Mathf.Ceil(startCount).ToString();
        } else {
            continueUI.SetActive(false);
            gameoverUI.SetActive(true);
        }
    }
    public IEnumerator BlinkText()
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
