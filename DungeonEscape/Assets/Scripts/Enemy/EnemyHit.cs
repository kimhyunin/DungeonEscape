using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow"){            
            animator.SetBool("IsHit", true);
            Invoke("EnemyDestory",1f);
            Debug.Log("True");
        } 
    }

    void EnemyDestory(){
        Destroy(gameObject);
    }


    
    
    
    
    
}
