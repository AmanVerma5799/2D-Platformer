using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEgg : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Player")
        {
            target.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
        gameObject.SetActive(false);
    }
}
