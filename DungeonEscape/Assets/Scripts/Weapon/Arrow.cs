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
        Attack();
    }
    void Attack(){
        // 발사거리가 일정 거리보다 멀어진 경우 화살 삭제
        if(transform.rotation.y == 0){ // 오른쪽 발사
            transform.Translate(transform.right * speed * Time.deltaTime);
            // 현재 포지션 > 시작 위치.x + 사정거리.x
            if(transform.position.x > currentPos.x + range.x){
                DestroyArrow();
            }
        }
        else { // 왼쪽 발사
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
            // 현재 포지션 < 시작 위치.x - 사정거리.x
            if(transform.position.x < currentPos.x - range.x){
                DestroyArrow();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 몬스터에게 닿았을 경우
        if(collision.gameObject.tag == "Enemy"){
            DestroyArrow();
        }
    }
    void DestroyArrow(){
        // 화살 삭제
        Destroy(gameObject);
    }
}
