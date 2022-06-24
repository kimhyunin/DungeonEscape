using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMove : MonoBehaviour
{
    public float climbSpeed = 3;
    public LayerMask ladderMask;
    public float vertical;
    public bool climbing;
    public float checkRadius = 0.3f;
    private Rigidbody2D rigid;
    private Animator animator;
    private Collider2D collider2;
    KeyBoardManager keyBoardManager;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2 = GetComponent<Collider2D>();
        keyBoardManager = GetComponent<KeyBoardManager>();

    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical") + keyBoardManager.up_value + keyBoardManager.down_value;
    }

    void FixedUpdate()
    {
        ClimbLadder();
    }
    bool OnLadder(){ 
        return collider2.IsTouchingLayers(ladderMask); // 사다리에 접근시 True
    }
    void ClimbLadder(){
        bool up = Physics2D.OverlapCircle(transform.position, checkRadius, ladderMask);
        bool down = Physics2D.OverlapCircle(transform.position + new Vector3(0, -1), checkRadius, ladderMask);
        if(vertical != 0 && OnLadder()){
            climbing = true;
            rigid.isKinematic = true; // 사다리에 올랐을 경우 충돌 0

            float xPos = (int)transform.position.x; // 사다리 중간
            xPos = xPos + (0.5f * Mathf.Sign(xPos));

            transform.position = new Vector2(xPos, transform.position.y);
        }
        if(climbing){
            if (!up && vertical >= 0) // 상단 도착
            {
                FinishClimb();
                return;
            }

            if (!down && vertical <= 0) // 하단 도착
            {
                FinishClimb();
                return;
            }
            float y = vertical * climbSpeed;
            rigid.velocity = new Vector2(0, y);            //atualiza velocidade do rigdbody de acordo com velocidade em y armazenada
        }
        animator.SetBool("IsClimb", climbing);
    }
     void FinishClimb()
    {
        climbing = false;
        rigid.isKinematic = false;
        animator.SetBool("IsClimb", false);
    }
     

}
