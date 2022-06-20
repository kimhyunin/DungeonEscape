using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject arrow;
    public Transform pos;
    Animator animator;
    KeyBoardManager keyBoardManager;
    private bool isAttack = false;

    // Start is called before the first frame update
    void Awake(){
        animator = GetComponent<Animator>();
        keyBoardManager = GetComponent<KeyBoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack(){
        // 연속공격 방지
        if(Input.GetKeyDown(KeyCode.Z) || ((keyBoardManager.a_Down) && isAttack )){
            animator.SetBool("IsAttack", true);
            Instantiate(arrow,pos.position,transform.rotation);
            isAttack = false;
        } else{
            animator.SetBool("IsAttack",false);
        }

        if(keyBoardManager.a_Up){
            isAttack = true;
        }
    }
}
