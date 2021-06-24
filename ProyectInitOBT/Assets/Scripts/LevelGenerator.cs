using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> listOfLevelBlocks;
    [SerializeField] GameObject initLevelBlock;
    private Collider2D collider;

    private void CheckCollider2d(string name)
    {
        if (name == initLevelBlock.transform.GetChild(5).name)
        {
            GameObject.Instantiate(listOfLevelBlocks[Random.Range(0, 2)]);
        }
    }

}
