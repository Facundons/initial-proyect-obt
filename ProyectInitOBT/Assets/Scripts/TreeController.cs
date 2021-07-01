using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private int speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Background"))
        {
            StartCoroutine(TreePositioningCoroutine());
        }
    }

    IEnumerator TreePositioningCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        transform.SetPositionAndRotation(new Vector2(15.52f, -1f), Quaternion.identity);
    }

    void Update()
    {
        if (GameController.Instance.GetGameState() == GameState.InGame)
        {
            transform.position += Vector3.left * Time.fixedDeltaTime * GameController.GameSpeed / speed;
        }
    }
}
