using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text coinScoreText;
    private AudioSource audioManager;
    private int score;

    void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    void Start()
    {
        coinScoreText = GameObject.Find("CoinText").GetComponent<Text>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Coin")
        {
            target.gameObject.SetActive(false);
            score++;

            coinScoreText.text = "" + score;
            audioManager.Play();
        }
    }

    public void BonusScore()
    {
        score += 5;
        coinScoreText.text = "" + score;
    }
}
