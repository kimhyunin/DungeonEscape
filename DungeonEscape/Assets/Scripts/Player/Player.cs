using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    private bool knockBack = false;
    public AudioClip audioJump;
    public AudioClip audioCoin;
    public GameManager gameManager;

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
    AudioSource audioSource;



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        keyBoardManager = GetComponent<KeyBoardManager>();
        capsuleColider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!DataManager.GetInstance().playerisDie){
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
        if(DataManager.GetInstance().isStart && !DataManager.GetInstance().isPause
            && !DataManager.GetInstance().playerisDie){
            if(!knockBack){
                Move();
            }
        }
    }
    private void SoundPlay(string action){
        switch(action){
            case "JUMP":
            audioSource.clip = audioJump;
            break;
            case "COIN":
            audioSource.clip = audioCoin;
            break;
        }
        audioSource.Play();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!DataManager.GetInstance().playerisDie){
            if(collision.gameObject.tag == "Enemy"){
                OnDamaged(collision.transform.position, "Enemy");
            } else if(collision.gameObject.tag == "Spike"){
                OnDamaged(collision.transform.position, "Spike");
            }
        } else {
            OnDie();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템먹기
        if(collision.gameObject.tag == "Item"){
            DataManager.GetInstance().stagePoint += 1;
            collision.gameObject.SetActive(false);
            SoundPlay("COIN");
        }
        if(collision.gameObject.tag == "Finish"){
            gameManager.NextStage();
        }
    }

    void PlayerJump(){
        // 점프
        if ((Input.GetButtonDown("Jump") || keyBoardManager.b_value == 1) && !animator.GetBool("IsJump"))
        {
            rigid.velocity = Vector2.up * jumpPower;
            rigid.gravityScale = 2f; // 중력 초기화
            animator.SetBool("IsJump", true); // 점프 애니메이션
            animator.SetBool("IsFall", true);
            SoundPlay("JUMP"); // 점프 사운드
        }
        if (rigid.velocity.y < -1.0f)
        {
            // 바닥면 체크
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); 
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.7f )
                {
                    animator.SetBool("IsJump", false); //IDLE 상태
                    animator.SetBool("IsFall", false);
                }
            }
        }
    }

    void PlayerRunAnimation(){
        // 애니메이션
        animator.SetBool("IsRun", Mathf.Abs(rigid.velocity.x) < 0.3f ? false : true);
    }

    void PlayerFlipX(){
        // 플레이어 스프라이트 반전
        float hor = Input.GetAxisRaw("Horizontal") + keyBoardManager.right_value + keyBoardManager.left_value;
        if (hor > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // 오른쪽 이동
        }
        else if (hor < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // 왼쪽
        }
    }

    void Move(){
        // 플레이어 이동
        float h = Input.GetAxisRaw("Horizontal")+ keyBoardManager.right_value + keyBoardManager.left_value;
        rigid.velocity = new Vector2(maxSpeed * h, rigid.velocity.y);
    }

    void OnDamaged(Vector2 targetPos, string target)
    {
        knockBack = true; // 넉백 당함
        gameObject.layer = 11; // PlayerDamaged
        // Health Down
        DataManager.GetInstance().HealthDown();
        if(!DataManager.GetInstance().playerisDie){
            spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 데미지 효과
        }
        // Reaction Force
        if(target == "Enemy"){ // 몬스터를 만났을 경우
        // 플레이어 위치 - 몬스터 위치 > 0
            int Dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(Dirc, 1) * 3 , ForceMode2D.Impulse);
        } else if(target == "Spike"){
            // 바라보는 방향 반대
            int Dirc = transform.rotation.y == 0 ? -1 : 1;
            rigid.AddForce(new Vector2(Dirc, 1) * 3 , ForceMode2D.Impulse);
        }

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
    public void OnDie(){
        animator.SetTrigger("IsDie");
        gameObject.layer = 12; // PlayerDie
        Invoke("DeActive",0.5f);
    }
    void DeActive(){
        gameObject.SetActive(false);
    }
    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}
