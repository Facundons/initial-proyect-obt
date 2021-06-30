using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject score;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject highScore;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject botonStart;
    [SerializeField] private GameObject gameOverScreen;
    private float scoreCounter;
    private float highScoreNumber = 0;
    public static event EventHandler onStartGame;
    public static event EventHandler onRestartGame;

    public void StartGameFromMainMenu()
    {
        GameController.Instance.StartGame();
        botonStart.SetActive(false);
        score.SetActive(true);
        highScore.SetActive(true);
        StartCoroutine(StartScoreCountingCoroutine());
        onStartGame?.Invoke(this, EventArgs.Empty);
    }

    public void StartGameFromGameOverMenu()
    {
        scoreCounter = 0;
        GameController.Instance.StartGame();
        highScore.SetActive(true);
        score.SetActive(true);
        particles.SetActive(false);
        retryButton.SetActive(false);
        gameOverScreen.SetActive(false);
        StartCoroutine(StartScoreCountingCoroutine());
        onRestartGame?.Invoke(this, EventArgs.Empty);
        
    }

    public void ShowGameOverMenu()
    {
        StartCoroutine(GameOverScreenCoroutine());
    }

    public IEnumerator GameOverScreenCoroutine()
    {
        yield return new WaitForSeconds(3);
        if (scoreCounter > highScoreNumber)
        {
            highScore.transform.SetPositionAndRotation(new Vector2(2.5f, 4.7f), Quaternion.identity);
            highScoreNumber = scoreCounter;
            highScoreText.text = scoreText.text;
            particles.SetActive(true);
            score.SetActive(false);
        }
        else
        {
            score.transform.SetPositionAndRotation(new Vector2(-2f, 4.7f), Quaternion.identity);
            highScore.SetActive(false);
        }
        retryButton.SetActive(true);
        gameOverScreen.SetActive(true);
    }

    IEnumerator StartScoreCountingCoroutine()
    {
        highScore.transform.SetPositionAndRotation(new Vector2(9f, 4.7f), Quaternion.identity);
        score.transform.SetPositionAndRotation(new Vector2(-9.5f, 4.7f), Quaternion.identity);
        while (GameController.Instance.GetGameState() == GameState.InGame)
        {         
            yield return new WaitForSeconds(0.5f);
            scoreCounter += 1;
            scoreText.text = scoreCounter.ToString();
        }
    }

}
