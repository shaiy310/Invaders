using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyEngine : MonoBehaviour
{
    public float health;
    public int points;
    public float speed;
    public string movementType;
    public GameObject explosion;
    public GameObject dropItem;
    /// <summary>
    /// Drop rate of of item after enemy is killed. A number between 0 and 100.
    /// </summary>
    public float dropRate;
    public GameObject blaster;
    public float shootingRate;
    public Vector3 blasterSpawnPosition;

    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(Random.Range(-1f, 1f) * speed, -speed);

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true) {
            yield return new WaitForSeconds(shootingRate);

            Instantiate(blaster, transform.TransformPoint(blasterSpawnPosition), transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border")) {
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Player")) {
            Kill();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Shot")) {
            return;
        }

        // Ignore other enemies' shots.
        LaserEngine laser = collision.gameObject.GetComponent<LaserEngine>();
        if (laser.shooter != "Player") {
            return;
        }

        health -= laser.damage;
        if (health <= 0) {
            Kill();
        }
    }

    void Kill()
    {
        UpdateScore(points);
        Instantiate(explosion, transform.position, Quaternion.identity);
        if (dropItem) {
            if (Random.Range(0, 100) < dropRate) {
            Instantiate(dropItem, transform.position, Quaternion.identity);

            }
        }
        Destroy(gameObject);
    }

    void UpdateScore(int delta)
    {
        GameManager.score += delta;
    }
}
