using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private Animator animator;
    private int health = 10;

    private bool canDamage;

    void Awake()
    {
        animator = GetComponent<Animator>();
        canDamage = true;
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(canDamage)
        {
            if(target.tag == "Bullet")
            {
                health--;
                canDamage = false;

                if(health == 0)
                {
                    animator.Play("Boss Dead");
                    GetComponent<Boss>().DeactivateBoss();
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }
}
