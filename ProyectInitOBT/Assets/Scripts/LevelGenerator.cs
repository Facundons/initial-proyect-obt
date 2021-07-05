using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject firstLevelBlock;
    [SerializeField] GameObject secondLevelBlock;
    [SerializeField] GameObject thirdLevelBlock;
    private int flagForBlockPosition = 0;
    private GameObject lastBlock;
    private Transform positionOfNextBlock;

    public void Awake()
    {
        BlockController.OnCharacterCollision += OnBlockCollision;
        UiController.onRestartGame += OnRestartGame;
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

    private void OnRestartGame(object sender, System.EventArgs e)
    {
        firstLevelBlock.transform.SetPositionAndRotation(new Vector2(0f, -4.25f), Quaternion.identity);
        secondLevelBlock.transform.SetPositionAndRotation(new Vector2(18.31f, -4.25f), Quaternion.identity);
        thirdLevelBlock.transform.SetPositionAndRotation(new Vector2(36.64f, -4.25f), Quaternion.identity);
        flagForBlockPosition = 0;
    }
}
