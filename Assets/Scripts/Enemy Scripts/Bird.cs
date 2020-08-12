using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D birdBody;
    private Animator animator;

    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;

    public GameObject birdEgg;
    public LayerMask playerLayer;
    public float speed = 2f;

    private bool attacked;
    private bool canMove;

    private void Awake()
    {
        birdBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        originPosition = transform.position;
        originPosition.x += 6f;

        movePosition = transform.position;
        movePosition.x += -6f;

        canMove = true;
    }

    void Update()
    {
        MoveBird();
        DropEgg();
    }

    void MoveBird()
    {
        if(canMove)
        {
            transform.Translate(moveDirection * speed * Time.smoothDeltaTime);
            if(transform.position.x >= originPosition.x)
            {
                moveDirection = Vector3.left;
                ChangeDirection(0.5f);
            }
            else if(transform.position.x <= movePosition.x)
            {
                moveDirection = Vector3.right;
                ChangeDirection(-0.5f);
            }
        }
    }

    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void DropEgg()
    {
        if(!attacked)
        {
            if(Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                attacked = true;
                animator.Play("Bird Fly");

            }
        }
    }

    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet")
        {
            animator.Play("Bird Dead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            birdBody.bodyType = RigidbodyType2D.Dynamic;

            canMove = false;

            StartCoroutine(BirdDead());
        }
    }
}