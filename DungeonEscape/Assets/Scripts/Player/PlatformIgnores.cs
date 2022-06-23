using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnores : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D platformCollider;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, false);
        }
    }

}

