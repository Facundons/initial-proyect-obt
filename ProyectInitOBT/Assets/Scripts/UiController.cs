using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject botonStart;
    [SerializeField] private GameObject gameOverScreen;
    public static event EventHandler onStartGame;

    public void StartGameFromMainMenu()
    {
        GameController.Instance.StartGame();
        onStartGame?.Invoke(this, EventArgs.Empty);
        botonStart.SetActive(false);
    }

    public void StartGameFromGameOverMenu()
    {
        GameController.Instance.StartGame();
    }

    public void ShowGameOverMenu()
    {
        botonStart.SetActive(true);
        StartCoroutine(GameOverScreenCoroutine());
    }

    public IEnumerator GameOverScreenCoroutine()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(gameOverScreen != null);
        gameOverScreen.SetActive(true);
    }

}
