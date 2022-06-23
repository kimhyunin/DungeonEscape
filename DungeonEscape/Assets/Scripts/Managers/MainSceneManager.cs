using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public Text startMessage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkText());
    }

    // Update is called once per frame
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
