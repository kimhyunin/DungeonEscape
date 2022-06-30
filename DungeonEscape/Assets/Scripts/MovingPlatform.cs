using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPos; // 시작 위치
    public Transform endPos; // 끝 지점
    public Transform desPos;
    public float speed;

    void Start()
    {
        transform.position = startPos.position;
        desPos = endPos;

    }
    void FixedUpdate()
    {
        Move();
    }
    void Move(){
        // 발판 이동
        transform.position=Vector2.MoveTowards(transform.position,desPos.position,Time.deltaTime * speed);
        // 현재 발판의 위치와 목표지점의 위치 사이값 <= 0.05
        Debug.Log(Vector2.Distance(transform.position,desPos.position));
        if(Vector2.Distance(transform.position,desPos.position) <= 0.05f){
            if(desPos == endPos){
                // 목표 지점 바꿔주기
                desPos = startPos;
            } else {
                desPos = endPos;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 발판과 플레이어가 만났을 경우 자식 관계
        if(collision.transform.CompareTag("Player")){
            collision.transform.SetParent(transform);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        // 발판과 플레이어가 떨어졌을 경우 자식 관계 해제
        if(collision.transform.CompareTag("Player")){
            collision.transform.SetParent(null);
        }
    }
}
