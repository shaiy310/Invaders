using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPrehabs;
    int maxEnemyIndex;

    int GeneratedEnemies;
    float MinX, MinY, MaxX, MaxY;
    int maxEnemiesInLevel, maxSimultaniousEnemiesInLevel;
    // Start is called before the first frame update
    void Start()
    {
        GeneratedEnemies = 0;
        // max enemy count per level
        maxEnemiesInLevel = 10 + 3 * GameManager.currentLevel;
        // Simultanious enemies per level
        maxSimultaniousEnemiesInLevel = 5;
        maxEnemyIndex = System.Math.Min(enemyPrehabs.Length, 1 + GameManager.currentLevel / 2);

        if (GameManager.mode == "GameSimulator") {
            (MinX, MaxX) = (-4.5f, 4.5f);
            (MinY, MaxY) = (10f, 12f);
        } else {
            (MinX, MaxX) = (-17f, 17f);
            (MinY, MaxY) = (10f, 12f);
            maxSimultaniousEnemiesInLevel += GameManager.currentLevel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (GeneratedEnemies < maxEnemiesInLevel) {
            if (enemies.Length < maxSimultaniousEnemiesInLevel) {
                int enemyIndex = Random.Range(0, maxEnemyIndex);
                Instantiate(enemyPrehabs[enemyIndex], new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)), Quaternion.identity);
                ++GeneratedEnemies;
            }
        } else {
            if (enemies.Length == 0) {
                // All current level enemies were killed -> move on to next level.
                ++GameManager.currentLevel;
                SceneManager.LoadScene(GameManager.GameScene);
            }
        }

    }
}
