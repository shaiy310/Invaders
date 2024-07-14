using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEngine : MonoBehaviour
{
    public string shooter;  // can be "Player" or "Enemy"
    public string type;     // can be "Normal", "Poison", "Freeze"
    public float damage;
    public float speed;
    
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (shooter == "Enemy") {
            speed *= -1;
        }

        rb.velocity = transform.TransformDirection(0, speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Shot")) {
            Destroy(gameObject);
        }
    }
}
