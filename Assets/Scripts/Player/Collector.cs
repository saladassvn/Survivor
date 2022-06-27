using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collector : MonoBehaviour
{
    public LevelSystem level;
    public ExpBar expBar;
    public TextMeshProUGUI levelText;
    public GameObject levelUpCanvas;


    void Start()
    {
        level = new LevelSystem(1, OnLevelUp);
        expBar.SetDefaultExp(0);
        expBar.slider.maxValue = level.GetXPforLevel(2);
        levelText.SetText("Level: 1");
        levelUpCanvas.SetActive(false);
    }

    public void OnLevelUp()
    {
        int oldEXP = level.exp;
        int newexp = level.GetXPforLevel(level.currentLevel);
        level.exp = 0;
        level.exp = (oldEXP - newexp);
        expBar.slider.maxValue = level.GetXPforLevel(level.currentLevel+1);
        levelText.SetText("Level: " + level.currentLevel);

        //For canvas 
        Time.timeScale = 0f;
        levelUpCanvas.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Gem gem = collision.GetComponent<Gem>();
        if(gem != null)
        {
            level.AddExp(gem.expAmmount);
            expBar.SetExp(level.exp);
            gem.Collect();
        }
    }
}
