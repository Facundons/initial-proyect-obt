using System;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public static event EventHandler OnCharacterCollision;

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckCharacterCollision(collision);
    }

    private void CheckCharacterCollision(Collider2D collider)
    {
        if (collider.name.Contains("Character"))
        {
            OnCharacterCollision?.Invoke(this, EventArgs.Empty);
        }       
    }

    private void FixedUpdate()
    {
        if (GameController.Instance.GetGameState() == GameState.InGame)
        {
            transform.position += Vector3.left * Time.fixedDeltaTime * GameController.GameSpeed;
        }
    }
}
