using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private float jumpforce = 1000.0f;
    public GameObject mainChar;
    private Rigidbody2D rb;
    [SerializeField] LayerMask floorLayer;
    private Animator mainCharAnim;

    public PlayerController()
    {
    }

    void Awake()
    {
        rb = mainChar.GetComponent<Rigidbody2D>();
        mainCharAnim = mainChar.GetComponent <Animator>();
    }

    void Start()
    {
        mainCharAnim.SetBool("isGrounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        mainCharAnim.SetBool("isGrounded", isOnTheFloor());
        if (Input.GetMouseButtonDown(0) && this.isOnTheFloor()) {
            jump();
        }
    }

    private void jump()
    {
        rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Force);
    }

    private bool isOnTheFloor()
    {
        return Physics2D.Raycast(mainChar.transform.position, Vector2.down, 0.88f, floorLayer);
    }
}
