using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject levelFinished;

    public static UIController instance;

    private ScoreManager _scoreManager;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    public void TryAgain()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

    }

    public void MainMenuButton()
    {
        
        SceneManager.LoadScene("Main Menu");
    }
}
