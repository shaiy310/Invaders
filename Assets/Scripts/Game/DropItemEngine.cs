using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemEngine : MonoBehaviour
{
    public string type;     // can be "Points", "Antidote", "Fire"
    public float speed;
    
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(0, -speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Border")) {
            Destroy(gameObject);
        }
    }
}
