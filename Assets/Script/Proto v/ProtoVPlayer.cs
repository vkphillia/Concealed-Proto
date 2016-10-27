﻿using UnityEngine;
using System.Collections;

public class ProtoVPlayer : MonoBehaviour
{
    public Color playerOriginal;
    public Color playerShadow;
    public float outerX;
    public float outerY;

    CircleCollider2D playerCollider;
    bool shadowMode;
    Color currentColor;

    void Start()
    {
        playerCollider = GetComponent<CircleCollider2D>();
        currentColor = playerOriginal;
        GetComponent<Rigidbody2D>().AddForce(transform.right * 150);
    }

    void Update()
    {
        if (ProtoVManager.Instance.gamePlaying)
        {
            if (transform.position.x > outerX || transform.position.x < -outerX || transform.position.y > outerY || transform.position.y < -outerY)
            {
                ProtoVManager.Instance.GameOver();
                return;
            }

            PlayerMovementMobile();

            if (Input.GetButton("Fire1"))
            {
                ShadowMode();
            }
            else
                ShadowModeOff();
        }
    }
    
    void PlayerMovementMobile()
    {
        float delta = Input.acceleration.x * 0.1f;
        transform.position += new Vector3(delta, 0, 0);
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void ShadowMode()
    {
        shadowMode = true;
        playerCollider.enabled = false;
        GetComponent<SpriteRenderer>().color = playerShadow;
        //GetComponent<Rigidbody2D>().velocity;
    }

    public void ShadowModeOff()
    {
            shadowMode = false;
            playerCollider.enabled = true;
            GetComponent<SpriteRenderer>().color = currentColor;
    }
}
