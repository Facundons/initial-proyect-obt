using UnityEngine;

public class ParalaxController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Vector2 respawnPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Background"))
        {
            transform.SetPositionAndRotation(new Vector2(17.95f, 1f), Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (GameController.Instance.GetGameState() == GameState.InGame)
        {
            transform.position += Vector3.left * Time.fixedDeltaTime * GameController.GameSpeed/speed;
        }
    }
}
