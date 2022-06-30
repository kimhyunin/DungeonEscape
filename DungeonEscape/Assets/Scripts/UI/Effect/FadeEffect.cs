using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeEffect : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DataManager.GetInstance().isStart == true)
        {
            Color color = image.color;

            if (color.a > 0)
            {
                color.a -= Time.deltaTime; // FadeOut
            }
            image.color = color;
        }
    }
}
