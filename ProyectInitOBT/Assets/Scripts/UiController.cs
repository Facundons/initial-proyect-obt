using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class UiController : MonoBehaviour
{
    public static event EventHandler onStartGame;
    public static event EventHandler onRestartGame;
    [SerializeField] private GameObject score;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject highScore;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject botonStart;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private DataManager dataManager;
    private float scoreCounter;
    private float highScoreNumber = 0;
    private string txtPath; 

    private void Awake()
    {
        txtPath = Application.dataPath + "\\HighScores\\HighScores.txt";
        GetHighScoreFromTxt();
        highScoreNumber = int.Parse(highScoreText.text);
        dataManager.onDateTimeRecieved += OnDateTimeRecieved;
    }

    private void GetHighScoreFromTxt()
    {
        int index = 0;
        highScoreText.text = File.ReadLines(txtPath).Last();
        do
        {
            index++;
        } while (!highScoreText.text.Substring(11, index).Contains("-"));
        highScoreText.text = highScoreText.text.Substring(11, index-1);
    }

    private void OnDateTimeRecieved(object sender, DateTimeApiModel dateTimeModel)
    {
        WriteHighScoreToTxt(dateTimeModel);
    }

    private void WriteHighScoreToTxt(DateTimeApiModel dateTimeModel)
    {
        highScoreNumber = scoreCounter;
        string date = dateTimeModel.datetime.Substring(0, 10);
        string time = dateTimeModel.datetime.Substring(11, 8);
        string score = highScoreNumber.ToString();
        string format = string.Format("High Score: {0} - Date: {1} - Time: {2}", score, date, time);
        File.AppendAllText(txtPath, format + Environment.NewLine);
        highScoreText.text = scoreText.text;
    }

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
            dataManager.GetDateTimeFromApi();
            highScore.transform.SetPositionAndRotation(new Vector2(2.5f, 4.7f), Quaternion.identity);         
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
