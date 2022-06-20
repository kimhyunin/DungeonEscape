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
    KeyBoardManager keyBoardManager;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        keyBoardManager = GetComponent<KeyBoardManager>();
    }

    void Update()
    {
        PlayerJump();
        PlayerRunAnimation();
        PlayerFlipX();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            OnDamaged(collision.transform.position);
            // animator.SetBool("IsDie", true);

        }
    }

    void PlayerJump(){
        // 점프
        Debug.Log(keyBoardManager.b_value);
        if((Input.GetButtonDown("Jump") || keyBoardManager.b_value == 1) && !animator.GetBool("IsJump")){
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
        // 플레이어 스프라이트 반전
        float hor = Input.GetAxisRaw("Horizontal") + keyBoardManager.right_value + keyBoardManager.left_value;
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

    void Move(){
        float h = Input.GetAxisRaw("Horizontal")+ keyBoardManager.right_value + keyBoardManager.left_value;
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
    
    

    void OnDamaged(Vector2 targetPos)
    {
        // Health Down
        //gameManager.HealthDown();

        gameObject.layer = 11; // Layer 11
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //TODO 넉백 수정 하기
        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 4 , ForceMode2D.Impulse);

        //Animation
        Invoke("offDamaged", 3); 

    }

    void offDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }
}
