using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] GameObject GroundEnemy;
    private int EnemyCount = 0;

    void Start()
    {
        UiController.onStartGame += Generator;
    }

    private void Generator(object sender, System.EventArgs e)
    {
        if (GameController.Instance.GetGameState() == GameState.InGame)
        {
            StartCoroutine(GenerateEnemies());
        }
    }

    IEnumerator GenerateEnemies()
    {
        while (GameController.Instance.GetGameState() == GameState.InGame)
        {
            float generationSpeed = 8/GameController.GameSpeed;
            Instantiate(GroundEnemy, new Vector2(10f, -3.25f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.5f, generationSpeed));
        }
    }
}
