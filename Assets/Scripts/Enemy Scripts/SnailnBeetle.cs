using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnailnBeetle : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Rigidbody2D myBody;
    private Animator animator;
    private bool moveLeft;

    public LayerMask playerLayer;

    private bool canMove;
    private bool isStunned;

    public Transform topCollider, bottomCollider, leftCollider, rightCollider;
    private Vector3 leftColliderPosition, rightColliderPosition;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        leftColliderPosition = leftCollider.position;
        rightColliderPosition = rightCollider.position;
    }

    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    void Update()
    {
        if(canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(moveSpeed * -1, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }

        CheckColission();
    }

    void CheckColission()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollider.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollider.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCollider.position, 0.2f, playerLayer);

        if(topHit != null)
        {
            if(topHit.gameObject.tag == "Player")
            {
                if(!isStunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    animator.Play("Stunned");
                    isStunned = true;

                    if(tag == "Beetle")
                    {
                        animator.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if(leftHit)
        {
            if(leftHit.collider.gameObject.tag == "Player")
            {
                if(!isStunned)
                {
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if(tag != "Beetle")
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if(rightHit)
        {
            if(rightHit.collider.gameObject.tag == "Player")
            {
                if(!isStunned)
                {
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if(tag != "Beetle")
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if(!Physics2D.Raycast(bottomCollider.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;
        if(moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            leftCollider.position = leftColliderPosition;
            rightCollider.position = rightColliderPosition;
        }
        else
        {
            tempScale.x = Mathf.Abs(tempScale.x) * -1;
            leftCollider.position = rightColliderPosition;
            rightCollider.position = leftColliderPosition;
        }
        transform.localScale = tempScale;
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "Bullet")
        {
            if(tag == "Beetle")
            {
                animator.Play("Stunned");
                canMove = false;
                myBody.velocity = new Vector2(0, 0);

                StartCoroutine(Dead(0.4f));
            }
            else if(tag == "Snail")
            {
                if (!isStunned)
                {
                    animator.Play("Stunned");
                    isStunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
