using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed = 5.0f;
    public float lifeTime = 5.0f;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {   
        if(transform.rotation.y == 0){
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);  

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
