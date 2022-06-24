using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public bool isJumping = false;
    private bool knockBack = false;
    public bool isGround;
    [SerializeField]
    Transform pos;

    [SerializeField]
    float checkRadius;

    [SerializeField]
    LayerMask islayer;

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    KeyBoardManager keyBoardManager;
    CapsuleCollider2D capsuleColider;
   


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        keyBoardManager = GetComponent<KeyBoardManager>();
        capsuleColider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if(DataManager.GetInstance().playerisDie){
            OnDie();
        } else {
            if(DataManager.GetInstance().isStart && !DataManager.GetInstance().isPause){
                if(!knockBack){
                    PlayerJump();
                    PlayerRunAnimation();
                    PlayerFlipX();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(DataManager.GetInstance().isStart && !DataManager.GetInstance().isPause){
            if(!knockBack){
                Move();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            OnDamaged(collision.transform.position);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템먹기
        if(collision.gameObject.tag == "Item"){
            DataManager.GetInstance().stagePoint += 1;
            collision.gameObject.SetActive(false);
        }
    }

    void PlayerJump(){
        // 점프
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius,islayer);
        if((Input.GetButtonDown("Jump") || keyBoardManager.b_value == 1) && !animator.GetBool("IsJump")){
            Debug.Log("JJJJJ");
            rigid.velocity = Vector2.up * jumpPower;
            //rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("IsJump", true);
            animator.SetBool("IsFall", true);
            isJumping = true;

        }
        if(isGround){
            animator.SetBool("IsJump",false);
            animator.SetBool("IsFall",false);
            isJumping = false;
        }
        // if(isGround){
        //     RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
        //     Debug.DrawRay(rigid.position, Vector3.down * 1, Color.red);
        //     if(rayhit.collider !=null){
        //         if(rayhit.distance < 0.7f){
        //             animator.SetBool("IsJump",false);
        //             animator.SetBool("IsFall",false);
        //             isJumping = false;
        //         }
        //     }
        // }
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
        // 플레이어 이동
        float h = Input.GetAxisRaw("Horizontal")+ keyBoardManager.right_value + keyBoardManager.left_value;
        rigid.velocity = new Vector2(maxSpeed * h, rigid.velocity.y);
    }

    void OnDamaged(Vector2 targetPos)
    {
        knockBack = true; // 넉백 당함
        isJumping = true; // 점프로 인식
        gameObject.layer = 11; // PlayerDamaged
        // Health Down
        DataManager.GetInstance().HealthDown();
        if(!DataManager.GetInstance().playerisDie){
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        }
        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 3 , ForceMode2D.Impulse);

        // 넉백 딜레이
        Invoke("SetKnockBack",0.5f);
        //Animation
        Invoke("OffDamaged", 2);

    }
    private void SetKnockBack(){
        knockBack = false;
    }

    private void OffDamaged()
    {
        gameObject.layer = 10; // Player
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }
    void OnDie(){
        animator.SetTrigger("IsDie");
        gameObject.layer = 12; // PlayerDie
    }
}
