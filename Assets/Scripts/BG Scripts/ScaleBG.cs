using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBG : MonoBehaviour
{

    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1, 1, 1);

        float width = renderer.sprite.bounds.size.x;
        float height = renderer.sprite.bounds.size.y;

        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight / Screen.height * Screen.width;

        Vector3 tempScale = transform.localScale;
        tempScale.x = worldWidth / width + 0.1f;
        tempScale.y = worldHeight / height + 0.1f;

        transform.localScale = tempScale;
    }
}
