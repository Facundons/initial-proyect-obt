using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    private List<EnemyController> groundEnemyList;
    [SerializeField] private GameObject groundEnemyPrefab;

    private bool pause;
    void Awake()
    {
        groundEnemyList = new List<EnemyController>();
        UiController.onStartGame += StartGenerator;
        UiController.onRestartGame += ResumeGenerator;
        PlayerController.OnDeath += StopGenerator;
    }

    private void StartGenerator(object sender, System.EventArgs e)
    {
        pause = false;
        StartCoroutine(GenerateEnemiesCoroutine());       
    }

    private void ResumeGenerator(object sender, System.EventArgs e)
    {
        pause = false;
    }

    private void StopGenerator(object sender, System.EventArgs e)
    {
        StartCoroutine(DestroyEnemies());
        pause = true;
    }

    IEnumerator GenerateEnemiesCoroutine()
    {
        while (GameController.Instance.GetGameState() == GameState.InGame)
        {
            float generationSpeed = 8/GameController.GameSpeed;
            Vector2 instantiatedEnemyPosition = new Vector2(10f, -3.25f);
            GameObject instantiatedEnemy = Instantiate(groundEnemyPrefab, instantiatedEnemyPosition, Quaternion.identity);
            groundEnemyList.Add(instantiatedEnemy.GetComponent<EnemyController>());
            yield return new WaitForSeconds(Random.Range(0.5f, generationSpeed));
            yield return new WaitUntil(() => { return pause == false;});
        }
    }

    IEnumerator DestroyEnemies()
    {
        yield return new WaitForSeconds(3);
        foreach (var groundEnemy in groundEnemyList)
        {
            Destroy(groundEnemy.gameObject);
        }
        groundEnemyList.Clear();
    }
}