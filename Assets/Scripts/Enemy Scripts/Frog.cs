using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Animator animator;
    private GameObject player;

    private bool animationStarted;
    private bool animationFinished;

    private int jumpTimes;
    private bool jumpLeft = true;

    public LayerMask playerLayer;

    void Awake()
    {
        animator = GetComponent<Animator>();        
    }

    void Start()
    {
        StartCoroutine("FrogJump");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {
            player.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    void LateUpdate()
    {
        if(animationStarted && animationFinished)
        {
            animationStarted = false;

            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        animationStarted = true;
        animationFinished = false;

        jumpTimes++;

        if(jumpLeft)
        {
            animator.Play("Frog Left Jump");
        }
        else
        {
            animator.Play("Frog Right Jump");
        }

        StartCoroutine("FrogJump");
    }

    void AnimationFinished()
    {
        animationFinished = true;
        
        if(jumpLeft)
        {
            animator.Play("Frog Left Idle");
        }
        else
        {
            animator.Play("Frog Right Idle");
        }

        if(jumpTimes == 3)
        {
            jumpTimes = 0;

            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft;
        }
    }
}
