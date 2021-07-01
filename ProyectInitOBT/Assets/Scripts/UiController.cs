using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
//using Newtonsoft.Json;

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
    private string txtPath = @"C:\Users\pc\Documents\Repos\initial-proyect-obt\ProyectInitOBT\Assets\HighScores\HighScores.txt";
    private string apiPath = $"http://worldtimeapi.org/api/timezone/America/Argentina/Tucuman";
    private static HttpClient apiClient;
    private ApiModel apiModel = new ApiModel();
    private void Awake()
    {
        highScoreText.text = File.ReadAllText(txtPath);
        highScoreNumber = int.Parse(highScoreText.text);
        InitializeClient();
    }

    private void InitializeClient()
    {
        if (apiClient == null)
        {
            apiClient = new HttpClient();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

    private async Task RequestToApi()
    {
        using (HttpResponseMessage response = await apiClient.GetAsync(apiPath))
        {
            if (response.IsSuccessStatusCode)
            {
                HttpResponseMessage customersRm = await apiClient.GetAsync(apiPath);
                apiModel.Datetime = await customersRm.Content.ReadAsStringAsync();
                //List<ApiModel> custs = JsonConvert.DeserializeObject<ApiModel>(apiModel.Datetime);
            }
        }
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
            highScore.transform.SetPositionAndRotation(new Vector2(2.5f, 4.7f), Quaternion.identity);
            highScoreNumber = scoreCounter;
            File.WriteAllText(txtPath, highScoreNumber.ToString());
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
