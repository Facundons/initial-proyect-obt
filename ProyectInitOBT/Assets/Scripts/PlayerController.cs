using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private float jumpforce = 1000.0f;
    public Rigidbody2D rb;
    public Animation jumpAnim;

    void Start()
    {
        jumpAnim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            jump();
        }
    }

    private void jump()
    {
        rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Force);
       // jumpAnim. ("isJumping", true);
    }

}
