using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Image newEnemy;
    public GameObject controllerCanvas;
    public Sprite[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.currentLevel % 2 == 0 && GameManager.currentLevel / 2 < enemies.Length) {
            controllerCanvas.SetActive(false);
            Time.timeScale = 0;
            newEnemy.sprite = enemies[GameManager.currentLevel / 2];
        } else {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        controllerCanvas.SetActive(GameManager.showControllerCanvas);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
