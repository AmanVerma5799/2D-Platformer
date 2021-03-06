﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float resetSpeed = 0.5f;
    public float cameraSpeed = 0.3f;

    public Bounds cameraBounds;

    private Transform target;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;

    private bool isFollowing;

    void Awake()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        cameraBounds.size = collider.size;
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        isFollowing = true;
    }


    void FixedUpdate()
    {
        if(isFollowing)
        {
            Vector3 aheadTargetPosition = target.position + Vector3.forward * offsetZ;
            if(aheadTargetPosition.x >= transform.position.x)
            {
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPosition, ref currentVelocity, cameraSpeed);
                transform.position = new Vector3(newCameraPosition.x, transform.position.y, newCameraPosition.z);
                lastTargetPosition = target.position; 
            }
        }
    }
}
