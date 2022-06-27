using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraSpeed = 3.0f;
    public GameObject player;
    public Vector2 followOffset;
    private Vector2 threshold;
    private Rigidbody2D rigid;
    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    void Start(){
        threshold = calculateThreshold();
        rigid = player.GetComponent<Rigidbody2D>();
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        Vector2 follow = player.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference)>=threshold.x){
            newPosition.x = follow.x;
        }
        if(Mathf.Abs(yDifference) >= threshold.y){
            newPosition.y = follow.y;
        }
        float moveSpeed = rigid.velocity.magnitude > cameraSpeed ? rigid.velocity.magnitude : cameraSpeed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);


    }
    private Vector3 calculateThreshold(){
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    void LateUpdate()
    {
        float lx = size.x * 0.5f -width;
        float clampx =Mathf.Clamp(transform.position.x, -lx + center.x, lx +center.x);

        float ly = size.x * 0.5f -width;
        float clampy =Mathf.Clamp(transform.position.y, -ly + center.y, ly +center.y);

        transform.position = new Vector3(clampx, clampy, -10f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center,size);

    }
}
