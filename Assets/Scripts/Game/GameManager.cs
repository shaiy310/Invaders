using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int MAX_HEALTH = 50;

    public static int currentLevel = 0;
    
    public static int lives = 3;

    public static float health = MAX_HEALTH;

    public static int score = 0;

    public static bool showControllerCanvas = true;

    public static string mode = "Game";  // "Game" or "GameSimulator"

    public static string GameScene { get { return GameManager.mode; } }

    public static string GameOverScene { 
        get { 
            return GameManager.mode == "GameSimulator" ? "GameOverSimulator" : "GameOver";
        }
    }

    public static void ResetGameSettings()
    {
        currentLevel = 0;
        score = 0;
        lives = 3;
        health = MAX_HEALTH;

    }

}


/* TODO:
 * Add menu scene
 * Add more sounds
 * Handle health regeneration
 * 
*/