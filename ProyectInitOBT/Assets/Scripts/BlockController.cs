using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public static event EventHandler OnTrigger;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnTrigger?.Invoke(this, EventArgs.Empty);
    }

    private void CheckCollider2d(string name)
    {
        //if (name == initLevelBlock.transform.GetChild(5).name)
        //{
        //    GameObject.Instantiate(listOfLevelBlocks[Random.Range(0, 2)]);
        //}
    }
}
