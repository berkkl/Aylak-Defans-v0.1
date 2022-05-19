using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public AudioSource gameOverSound;
    private Waypoint wp;
    //private Spawner spw;
    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    private UIController _uiController;
    public int sec_flag = 0;
    public CameraShake cameraShake;
    public float money;
    public int lives;

    public TMP_Text moneyText;
    public TMP_Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PrintMoney();
    }

    void PrintMoney()
    {
        moneyText.text = money.ToString();
        livesText.text = lives.ToString();
    }
    public void LoseLife()
    {
        
        lives--;
        if (lives <= 0)
        {
            //TODO: Game over screen!
            lives = 0;
            Time.timeScale = 0f;
            GameOver();
        }
    }

    
    void GameOver()
    {
        gameOverSound.Play();
        UIController.instance.levelFinished.SetActive(true);
        wp = FindObjectOfType<Waypoint>();
    }
}
