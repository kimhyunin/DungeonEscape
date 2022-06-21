using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow"){
            animator.SetBool("IsHit", true);
            OnDamaged();
        }
    }
    public void OnDamaged()
    {
        DataManager.GetInstance().stagePoint += 10;
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Colider Disable
        boxCollider2D.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("EnemyDestory",3f);
    }

    void EnemyDestory(){
        Destroy(gameObject);
    }
}
