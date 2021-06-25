using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public static event EventHandler OnCharacterCollision;

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    CheckCharacterCollision(collider);
    //}

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


}
