using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform transform;
    public float speed = 5.0f;
    public float lifeTime = 5.0f;
    public Vector2 range;
    Player player;
    private Vector2 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        currentPos = transform.position;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.y == 0){ // 오른쪽 발사
            transform.Translate(transform.right * speed * Time.deltaTime);
            if(transform.position.x > currentPos.x + range.x){
                DestroyArrow();
            }
        }
        else { // 왼쪽 발사
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
            if(transform.position.x < currentPos.x - range.x){
                DestroyArrow();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            DestroyArrow();
        }
    }

    void DestroyArrow(){
        Destroy(gameObject);
    }
}
