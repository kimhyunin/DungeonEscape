using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeInEffect : MonoBehaviour
{
   private Image image;
   public Text endingCredit;
   private string text = "CLEAR!!\r\n\r\nDEVELOPER BY KIM HYUN IN\r\n\r\nTHANK YOU";
    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start(){
        ShowText();
    }

    // Update is called once per frame
    void Update()
    {
        if(DataManager.GetInstance().isStart == true)
        {
            Color color = image.color;

            if (color.a >= 0)
            {
                color.a += Time.deltaTime; // FadeIn 효과
            }
            image.color = color;
        }
    }
    void ShowText(){
        StartCoroutine(typing());
    }
    IEnumerator typing(){
        yield return new WaitForSeconds(2f);
        for(int i = 0; i<=text.Length; i++){
            endingCredit.text = text.Substring(0,i); // 한글자 씩 노출
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(0);

    }
}
