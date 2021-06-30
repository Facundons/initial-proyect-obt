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
                instance.name = "GameController";
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
        UiController.onRestartGame += OnRestartGame;
        UiController = FindObjectOfType<UiController>();
        mainChar = FindObjectOfType<PlayerController>();
    }

    IEnumerator UpdateSpeed()
    {
        while (GameSpeed < maxSpeed)
        {
            GameSpeed += acceleration;
            yield return new WaitForSeconds(5);
        }
        yield break;
    }

    private void OnPlayerDeath(object sender, System.EventArgs e)
    {
        GameOver();
        UiController.ShowGameOverMenu();
        StartCoroutine(WaitForDeathAnimationCourutine());
        GameSpeed = 0;
    }

    private void OnRestartGame(object sender, System.EventArgs e)
    {
        GameSpeed = 1.0f;
        mainChar.gameObject.SetActive(true);
        Vector2 startingPositionMainChar = new Vector2(-5.86f, -1.95f);
        mainChar.gameObject.transform.SetPositionAndRotation(startingPositionMainChar, Quaternion.identity);
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

    IEnumerator WaitForDeathAnimationCourutine()
    {
        yield return new WaitForSeconds(3);
        mainChar.gameObject.SetActive(false);
    }
}
