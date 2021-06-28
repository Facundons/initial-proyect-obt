using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameState gameState = GameState.Menu;
    [SerializeField] private UiController UiController;
    public static float GameSpeed = 1.0f;
    private float acceleration = 0.1f;
    private float maxSpeed = 4.0f;
    private int flag = 0;
    [SerializeField] private PlayerController mainChar;

    private static GameController instance;
    public static GameController Instance 
    {
        get
        {
            if (instance == null)
            {
                var gameController = new GameObject();
                instance = gameController.AddComponent<GameController>();              
            }
            return instance;
        }
    }

    private GameController()
    {

    }

    private void Awake()
    {
        PlayerController.OnDeath += OnPlayerDeath;
        UiController = FindObjectOfType<UiController>();
        mainChar = FindObjectOfType<PlayerController>();
    }

    IEnumerator UpdateSpeed()
    {
        while (GameSpeed < maxSpeed)
        {
            GameSpeed += acceleration;
            Debug.Log("game speed is: " + GameSpeed);
            yield return new WaitForSeconds(1);
        }
        yield break;
    }

    private void OnPlayerDeath(object sender, System.EventArgs e)
    {
        UiController.ShowGameOverMenu();
        mainChar.gameObject.SetActive(false);
        GameOver();
        GameSpeed = 0;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void StartGame()
    {
        gameState = GameState.InGame;
        StartCoroutine(UpdateSpeed());
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
    }

    public void MainMenu()
    {
        gameState = GameState.Menu;
    }

}
