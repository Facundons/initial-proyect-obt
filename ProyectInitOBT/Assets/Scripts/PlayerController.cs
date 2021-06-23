using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float jumpforce = 1000.0f;
    private Rigidbody2D rigidBody;
    private Collider2D collider;
    private Animator animator;
    [SerializeField] GameObject floor;
    private bool isGrounded;
    [SerializeField] GameObject obstacle;
    public static event EventHandler OnDeath; 

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        animator.SetBool("isGrounded", true);
        animator.SetBool("gameStarted", false);
    }

    void Update()
    {
        GameState currentGameState = GameController.GetInstance().GetGameState();
        if (currentGameState == GameState.InGame) 
        {
            ControlMainChar();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision.collider.name);
    }

    private void CheckCollision(string name)
    {
        if (name == floor.name)
        {
            isGrounded = true;
        }
        else if (name == obstacle.name)
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            isGrounded = false;
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
    }
}
