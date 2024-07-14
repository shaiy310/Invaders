using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIStats : MonoBehaviour
{
    public TextMeshProUGUI score;
    public Slider healthBar;
    public TextMeshProUGUI lifes;
    
    // Update is called once per frame
    void Update()
    {
        score.text = GameManager.score.ToString();
        healthBar.value = GameManager.health;
        lifes.text = GameManager.lives.ToString();
    }
}
