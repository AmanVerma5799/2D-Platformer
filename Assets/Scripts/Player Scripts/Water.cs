using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{
    private BoxCollider2D waterTrigger;

    void Awake()
    {
        waterTrigger = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Scene");
    }
}
