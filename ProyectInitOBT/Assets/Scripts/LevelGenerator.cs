using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject firstLevelBlock;
    [SerializeField] GameObject secondLevelBlock;
    [SerializeField] GameObject thirdLevelBlock;
    private Collider2D collider;
    private int flagForBlockPosition = 0;
    GameObject lastBlock;
    private Transform positionOfNextBlock;

    public void Awake()
    {
        BlockController.OnCharacterCollision += OnBlockCollision;
    }

    private void OnBlockCollision(object sender, System.EventArgs e)
    {
        if (flagForBlockPosition == 0)
        {
            lastBlock = secondLevelBlock;
            positionOfNextBlock = lastBlock.transform.GetChild(4).transform;
            thirdLevelBlock.transform.SetPositionAndRotation(positionOfNextBlock.position, positionOfNextBlock.rotation);
            flagForBlockPosition = 1;
        }
        else if (flagForBlockPosition == 1)
        {
            lastBlock = thirdLevelBlock;
            positionOfNextBlock = lastBlock.transform.GetChild(4).transform;
            firstLevelBlock.transform.SetPositionAndRotation(positionOfNextBlock.position, positionOfNextBlock.rotation);
            flagForBlockPosition = 2;
        }
        else
        {
            lastBlock = firstLevelBlock;
            positionOfNextBlock = lastBlock.transform.GetChild(4).transform;
            secondLevelBlock.transform.SetPositionAndRotation(positionOfNextBlock.position, positionOfNextBlock.rotation);
            flagForBlockPosition = 0;
        }
    }

}
