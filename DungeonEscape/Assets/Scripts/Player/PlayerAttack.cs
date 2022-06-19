using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject arrow;
    public Transform pos;
    Animator animator;
    

    // Start is called before the first frame update
    void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    async void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            Debug.Log("Input Key Z!!");
            animator.SetBool("IsAttack", true);
            Instantiate(arrow,pos.position,transform.rotation);

        } else{
            animator.SetBool("IsAttack",false);
        }

    }
}
