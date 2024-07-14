using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerEngine : MonoBehaviour
{
    public Slider healthBar;
    public ParticleSystem particles;
    public float speed = 1000;
    public GameObject[] blasters;

    Rigidbody2D rb;
    GameObject activeBlaster;
    Vector2 direction;
    List<Coroutine> coroutines;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        activeBlaster = blasters[0];
        coroutines = new List<Coroutine>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyboardKeys();
        Vector2 localDirection = transform.TransformDirection(direction);
        rb.velocity = speed * localDirection;
        if (rb.velocity ==  Vector2.zero) {
            particles.Stop();
        } else {
            particles.Play();
        }
    }

    void HandleKeyboardKeys()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();

        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            MoveVertical("Up");
        } else if (Input.GetKeyUp(KeyCode.UpArrow) && (direction.y > 0)) {
            MoveVertical("Stop");

        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            MoveVertical("Down");
        } else if (Input.GetKeyUp(KeyCode.DownArrow) && (direction.y < 0)) {
            MoveVertical("Stop");

        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveHorizontal("Right");
        } else if (Input.GetKeyUp(KeyCode.RightArrow) && (direction.x > 0)) {
            MoveHorizontal("Stop");

        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveHorizontal("Left");
        } else if (Input.GetKeyUp(KeyCode.LeftArrow) && (direction.x < 0)) {
            MoveHorizontal("Stop");

        } else if (Input.GetKeyDown(KeyCode.W)) {
            Rotate("Right");
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            Rotate("Left");
        }
    }

    public void MoveVertical(string dir)
    {
        direction.y = dir switch {
            "Up" => 1,
            "Down" => -1,
            _ => (float)0,
        };
    }

    public void MoveHorizontal(string dir)
    {
        direction.x = dir switch {
            "Right" => 1,
            "Left" => -1,
            _ => (float)0,
        };
    }

    public void Rotate(string dir)
    {
        float angleZ = dir switch {
            "Right" => -30,
            "Left" => 30,
            _ => 0,
        };

        transform.Rotate(0, 0, angleZ);
    }

    public void Shoot()
    {
        Instantiate(activeBlaster, transform.TransformPoint(new Vector3(0, 1.35f)), transform.rotation, transform);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            UpdateHealth(-50);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shot")) {
            LaserEngine laser = collision.gameObject.GetComponent<LaserEngine>();
            switch (laser.type) {
                case "Poison":
                    coroutines.Add(StartCoroutine(Poison(laser.damage, 0.5f, 20)));
                    break;
                case "Freeze":
                    StartCoroutine(Freeze(laser.damage, 5));
                    break;
                default:
                    UpdateHealth(-laser.damage);
                    break;

            }
        } else if (collision.CompareTag("DropItem")) {
            DropItemEngine dropItem = collision.gameObject.GetComponent<DropItemEngine>();
            switch (dropItem.type) {
                case "Antidote":
                    Antidote();
                    break;
                case "Health":
                    UpdateHealth(20);
                    break;
                case "Points":
                    GameManager.score += 50;
                    break;
                default:
                    print($"drop item is '{dropItem.type}'");
                    break;
            }
        }
    }

    IEnumerator Freeze(float damage, float duration)
    {
        speed /= 2;
        UpdateHealth(-damage);
        yield return new WaitForSeconds(duration);
        speed *= 2;
    }

    IEnumerator Poison(float damage, float interval, int iterations)
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 0.5f, 1);
        for (int i = 0; i < iterations; i++) {
            UpdateHealth(-damage);
            yield return new WaitForSeconds(interval);
        }
        renderer.color = Color.white;
    }

    void Antidote()
    {
        coroutines.ForEach(StopCoroutine);
        coroutines.Clear();
        UpdateHealth(10);
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = Color.white;
    }

    void UpdateHealth(float delta)
    {
        GameManager.health += delta;
        if (GameManager.health <= 0) {
            --GameManager.lives;
            transform.position = new Vector3(0, -8);
            transform.rotation = Quaternion.identity;
            GameManager.health = GameManager.MAX_HEALTH;
            if (GameManager.lives == 0) {
                SceneManager.LoadScene(GameManager.GameOverScene);
            }
        }
        
    }
}
