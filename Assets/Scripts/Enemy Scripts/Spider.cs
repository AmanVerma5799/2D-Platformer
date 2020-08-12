using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D spiderBody;

    private Vector3 moveDirection = Vector3.down;

    void Awake()
    {
        spiderBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine("ChangeMovement");
    }

    void Update()
    {
        MoveSpider();
    }

    void MoveSpider()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        if(moveDirection == Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        else
        {
            moveDirection = Vector3.down;
        }

        StartCoroutine("ChangeMovement");
    }

    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet")
        {
            animator.Play("Spider Dead");
            spiderBody.bodyType = RigidbodyType2D.Dynamic;

            StartCoroutine(SpiderDead());
            StopCoroutine("ChangeMovement");
        }

        if(target.tag == "Player")
        {
            target.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
