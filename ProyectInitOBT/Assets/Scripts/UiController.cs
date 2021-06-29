using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject botonStart;
    [SerializeField] private GameObject gameOverScreen;
    public static event EventHandler onStartGame;
    [SerializeField] private GameObject retryButton;
    public static event EventHandler onRestartGame;

    public void StartGameFromMainMenu()
    {
        GameController.Instance.StartGame();
        botonStart.SetActive(false);
        onStartGame?.Invoke(this, EventArgs.Empty);
    }

    public void StartGameFromGameOverMenu()
    {
        GameController.Instance.StartGame();
        retryButton.SetActive(false);
        gameOverScreen.SetActive(false);
        onRestartGame?.Invoke(this, EventArgs.Empty);
    }

    public void ShowGameOverMenu()
    {
        StartCoroutine(GameOverScreenCoroutine());
    }

    public IEnumerator GameOverScreenCoroutine()
    {
        yield return new WaitForSeconds(3);
        retryButton.SetActive(true);
        gameOverScreen.SetActive(true);
    }

}
