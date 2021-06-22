using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Called when the game begins
    [SerializeField]private GameState gameState = GameState.Menu;
    private static GameController sharedInstance;

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
        ChangeGameState(gameState);
    }

    // Called when mainChar dies
    public void GameOver()
    {
        gameState = GameState.GameOver;
        ChangeGameState(gameState);
    }

    // Called to go to the main menu
    public void BackToMainMenu()
    {
        gameState = GameState.Menu;
        ChangeGameState(gameState);
    }

    void ChangeGameState(GameState newGameState)
    {
        gameState = newGameState;
        switch (newGameState)
        {
            case GameState.Menu:
                //show menu
                break;

            case GameState.InGame:
                //run game
                break;

            case GameState.GameOver:
                //show game over anim and back to menu
                break;

            default:
                //default block, go to main menu
                break;
        
            }
    }
}
