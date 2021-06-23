using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Called when the game begins
    [SerializeField]private GameState gameState = GameState.Menu;
    private static GameController sharedInstance;
    public UiController UiController;

    public GameController()
    {
        PlayerController.OnDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath(object sender, System.EventArgs e)
    {
        UiController.ShowGameOverMenu();
    }

    private void Awake()
    {
        sharedInstance = this;
    }

    public static GameController GetInstance()
    {
        return sharedInstance;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void StartGame()
    {
        gameState = GameState.InGame;
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
