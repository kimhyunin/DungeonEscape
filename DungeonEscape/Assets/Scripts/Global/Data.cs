using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    private Data() {}
    private static Data instance = null;
    public static Data GetInstance(){
        if(instance == null)
        {
            instance = new Data();
        }
        return instance;
    }
    
    public bool isStart {get; set;}
}
