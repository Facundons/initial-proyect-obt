using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static event EventHandler OnDeath;
    private string levelblock = "Block";
    private string enemy = "Obstacle";
    private bool isGrounded;
    private float jumpforce = 1300.0f;
    private Rigidbody2D rigidBody;
    private Animator animator;   

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isGrounded", true);
        animator.SetBool("gameStarted", false);
    }

    void Update()
    {
        GameState currentGameState = GameController.Instance.GetGameState();
        if (currentGameState == GameState.InGame) 
        {
            ControlMainChar();
        }
    }

    private void OnEnable()
    {
        animator.SetBool("isGrounded", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision.collider.name);
    }

    private void CheckCollision(string name)
    {
        if (name.Contains(levelblock))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
        if (name.Contains(enemy))
        {
            animator.Play("Death_Principal_Char");
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ControlMainChar()
    {
        if(animator.GetBool("gameStarted") == false)
        {
            animator.SetBool("gameStarted", true);
        }
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpforce, ForceMode2D.Force);
        animator.Play("Jump_Principal_Char");
        isGrounded = false;
        animator.SetBool("isGrounded", false);
    }
}
