using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    private int nextMove;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Think();
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if(DataManager.GetInstance().isStart){
            switch(this.name){
                case "Slime":
                SlimeMove();
                break;

                case "Bat":
                BatMove();
                break;

                case "PatrolGuy":
                SlimeMove();
                break;
            }
        }
    }

    void SlimeMove(){
        Move();

    }
    void BatMove(){
        Move();
    }
    void PatrolGuyMove(){
        Move();
    }
    void Move(){
        rigid.velocity = new Vector2(nextMove,rigid.velocity.y);
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(rayHit.collider == null)
        {
            Turn();
        }
    }

     void Turn()
    {
        nextMove = nextMove * -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 5);
    }
    void Think()
    {
        // Set Next Active
        nextMove = Random.Range(-1, 2);
        // Flip Sprite
        if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
}
