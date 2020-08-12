using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 5f;

    private Rigidbody2D rigidBody;
    private Animator animator;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if(input > 0)
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            ChangeDirection(1);
        }
        else if(input < 0)
        {
            rigidBody.velocity = new Vector2(speed * -1, rigidBody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }

        animator.SetInteger("Speed", Mathf.Abs((int)rigidBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        if(isGrounded)
        {
            if(jumped)
            {
                jumped = false;
                animator.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if(isGrounded)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower);
                animator.SetBool("Jump", true);
            }
        }
    }
}
