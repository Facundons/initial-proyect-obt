using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    void Update()
    {
        if (GameController.GetInstance().GetGameState() == GameState.InGame)
        {
            transform.position += Vector3.right * Time.fixedDeltaTime;
        }
    }

}
