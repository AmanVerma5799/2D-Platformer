using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject stone;
    public Transform attackPosition;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine("StartAttack");
    }

    void BackToIdle()
    {
        animator.Play("Boss Idle");
    }

    void Attack()
    {
        GameObject obj = Instantiate(stone, attackPosition.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300, -700), 0f));
    }

    public void DeactivateBoss()
    {
        StopCoroutine("StartAttack");
        enabled = false;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        animator.Play("Boss Attack");
        StartCoroutine("StartAttack");
    }
}
