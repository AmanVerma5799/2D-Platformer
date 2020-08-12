using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    public Transform bottomCollider;
    public LayerMask playerLayer;

    private Animator animator;
    private AudioSource audioManager;
    private GameObject player;

    private Vector3 moveDirection = Vector3.up;
    private Vector3 originPosition;
    private Vector3 animPosition;
    private bool startAnimation;
    private bool canAnimate = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        originPosition = transform.position;
        animPosition = transform.position;
        animPosition.y += 0.15f;
    }

    void Update()
    {
        CheckForCollision();
        AnimateUpDown();
    }

    void CheckForCollision()
    {
        if(canAnimate)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottomCollider.position, Vector3.down, 0.1f, playerLayer);
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    animator.Play("BonusBlock Idle");
                    audioManager.Play();
                    startAnimation = true;
                    canAnimate = false;
                    player.GetComponent<ScoreScript>().BonusScore();
                }
            }
        }
    }

    void AnimateUpDown()
    {
        if(startAnimation)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);

            if(transform.position.y >= animPosition.y)
            {
                moveDirection = Vector3.down;
            }
            else if(transform.position.y <= originPosition.y)
            {
                startAnimation = false;
            }
        }
    }
}
