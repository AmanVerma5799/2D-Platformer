using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private Text lifeCountText;
    private int lifeCount;

    private bool canDamage;

    void Awake()
    {
        lifeCountText = GameObject.Find("LivesText").GetComponent<Text>();
        lifeCount = 3;

        lifeCountText.text = "" + lifeCount;
        canDamage = true;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void DealDamage()
    {
        if(canDamage)
        {
            lifeCount--;
            if (lifeCount >= 0)
            {
                lifeCountText.text = "" + lifeCount;
            }
            if(lifeCount == 0)
            {
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }
            canDamage = false;

            StartCoroutine(WaitToDamage());
        }
    }

    IEnumerator WaitToDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Game Scene");
    }
}
