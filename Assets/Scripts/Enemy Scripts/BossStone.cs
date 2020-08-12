﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStone : MonoBehaviour
{
    void Start()
    {
        Invoke("Deactivate", 4f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            target.gameObject.GetComponent<PlayerDamage>().DealDamage();
            gameObject.SetActive(false);
        }
    }
}
