using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollisionWithDestroyer(collision);
    }

    private void CheckCollisionWithDestroyer(Collider2D collision)
    {
        if (collision.name.Contains("EnemiesManager"))
        {
                gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameController.Instance.GetGameState() == GameState.InGame)
        {
            transform.position += Vector3.left * Time.fixedDeltaTime * GameController.GameSpeed;
        }
    }
}
