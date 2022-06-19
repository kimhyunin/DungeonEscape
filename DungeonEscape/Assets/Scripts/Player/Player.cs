using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerJump();
        PlayerRunAnimation();
        PlayerFlipX();
    }

    void PlayerJump(){
        // 점프
        if(Input.GetButtonDown("Jump") && !animator.GetBool("IsJump")){
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("IsJump", true);
            animator.SetBool("IsFall", true);
        }
    }

    void PlayerRunAnimation(){
        // 애니메이션
        if(Mathf.Abs(rigid.velocity.x) < 0.3f)
            animator.SetBool("IsRun", false);
        else 
            animator.SetBool("IsRun",true);
    }
    
    void PlayerFlipX(){
        float hor = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(Mathf.Abs(hor)*Time.deltaTime,0,0));
        if (hor > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (hor < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(maxSpeed * h, rigid.velocity.y);
        
        if(rigid.velocity.y < -1.0f){
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            Debug.DrawRay(rigid.position, Vector3.down * 1, Color.red);
            if(rayhit.collider !=null){
                if(rayhit.distance < 0.7f){
                    animator.SetBool("IsJump",false);
                    animator.SetBool("IsFall",false);
                }
            }
        }
    }
}
