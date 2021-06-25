using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> listOfLevelBlocks;
    [SerializeField] GameObject initLevelBlock;
    private Collider2D collider;
    private int flagForFirstBlockPosition = 0;
    GameObject lastBlock;
    private Transform positionOfNextBlock;
    private int randomExecutionNumber;

    public void Awake()
    {
        BlockController.OnCharacterCollision += OnBlockCollision;
    }

    private void OnBlockCollision(object sender, System.EventArgs e)
    {
        int randomNum;
        if (flagForFirstBlockPosition == 0)
        {
            randomNum = GetRandomNumber();
            flagForFirstBlockPosition++;
            randomExecutionNumber = randomNum;
            lastBlock = initLevelBlock;
            positionOfNextBlock = lastBlock.transform.GetChild(4).transform;
            listOfLevelBlocks[randomNum].transform.SetPositionAndRotation(positionOfNextBlock.position, positionOfNextBlock.rotation);
            listOfLevelBlocks[randomNum].SetActive(true);
            lastBlock = listOfLevelBlocks[randomNum];
        }
        else
        {
            do
            {
                randomNum = GetRandomNumber();
            } while (randomNum == randomExecutionNumber);
            randomExecutionNumber = randomNum;
            positionOfNextBlock = lastBlock.transform.GetChild(4).transform;
            lastBlock = listOfLevelBlocks[randomNum];
            listOfLevelBlocks[randomNum].transform.SetPositionAndRotation(positionOfNextBlock.position, positionOfNextBlock.rotation);
            listOfLevelBlocks[randomNum].SetActive(true);
        }
    }

    private int GetRandomNumber()
    {
       return Random.Range(0, listOfLevelBlocks.Count);
    }

}
