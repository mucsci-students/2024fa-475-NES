using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || 
            collision.gameObject.tag == "SmallEnemy" || 
            collision.gameObject.tag == "MediumEnemy" ||
            collision.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }
}
