using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = $"Your score is {GameManager.score}";
    }

    public void GotoMainMenu()
    {
        GameManager.ResetGameSettings();
        SceneManager.LoadScene(GameManager.GameScene);
    }
}
