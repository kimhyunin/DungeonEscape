using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMove : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator animator;
    Player player;
    KeyBoardManager keyBoardManager;

    public bool isLadder;
    public bool isClimbing;
    private float playerMaxSpeed = 2f;

    public GameObject ladder;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        keyBoardManager = GetComponent<KeyBoardManager>();
    }

    void FixedUpdate()
    {
        if (isLadder&&!player.isJumping)
        {
            float ver = Input.GetAxis("Vertical"); // 초기화
            Debug.Log(ver);
            if (keyBoardManager.up_value == 1)
            {
                ver += keyBoardManager.up_value;
            }
            if (keyBoardManager.down_value == -1)
            {
                ver += keyBoardManager.down_value;
            }

            if (Mathf.Abs(ver) > 0)
            {
                rigid.gravityScale = 0f;
                rigid.velocity = new Vector2(rigid.velocity.x, ver * playerMaxSpeed);
                rigid.transform.position = new Vector2(Mathf.Floor(rigid.transform.position.x) + 0.5f, rigid.transform.position.y);
                animator.SetBool("IsClimb", true);
                isClimbing = true;
                rigid.velocity = new Vector2(0, ver * playerMaxSpeed);
            }
        }
        if (!isLadder)
        {
            animator.SetBool("IsClimb", false);
            rigid.gravityScale = 2f;
            isClimbing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            Debug.Log("On!!");

            //isLadder = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            Debug.Log("Exit!!");
            isLadder = false;
            isClimbing = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            Debug.Log("still touching");
            isLadder = true;

        }

    }
}
