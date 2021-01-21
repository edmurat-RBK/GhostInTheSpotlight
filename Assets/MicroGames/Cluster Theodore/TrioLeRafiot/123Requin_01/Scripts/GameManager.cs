using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// by Antoine
/// This script is the manager of the game. It's use to set parameters of the game. 
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState {Running, Win, Lose}
    public GameState gameState = GameState.Running;

    #region Variables
    public float timeToPlay;

    public GameObject UI;

    #endregion

    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion        
    }

    private void Start()
    {
        UI.SetActive(false);
    }

    private void Update()
    {
        timeToPlay -= Time.deltaTime;

        if (timeToPlay <= 0)
        {
            gameState = GameState.Lose;
        }

        if (gameState == GameState.Lose)
        {
            GameLose();
        }
        else if(gameState == GameState.Win)
        {
            GameWin();
        }
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        UI.SetActive(true);
        UI.GetComponentInChildren<Text>().text = "Win !";
        UI.GetComponentInChildren<Text>().color = Color.green;
    }

    public void GameLose()
    {
        Time.timeScale = 0;
        UI.SetActive(true);
        UI.GetComponentInChildren<Text>().text = "Lose !";
        UI.GetComponentInChildren<Text>().color = Color.red;
    }
}
