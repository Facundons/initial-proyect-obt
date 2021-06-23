using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{

    [SerializeField] GameObject botonStart;
    [SerializeField] GameObject floor;

    private void Awake()
    {

    }
    private void Start()
    {
        
    }

    public void StartGameFromMainMenu()
    {
        GameController.GetInstance().StartGame();
        botonStart.SetActive(false);
    }

    public void StartGameFromGameOverMenu()
    {
        GameController.GetInstance().StartGame();
    }

    public void GoBackToMainMenu()
    {
        GameController.GetInstance().StartGame();
    }

    public void ShowGameOverMenu()
    {
        //TODO: setActive buttons and gameover menu
    }

}
