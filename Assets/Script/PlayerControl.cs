﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float forcePower;
    public float sizeToBurst;
    CircleCollider2D playerCollider;
    public Color[] playerColor;
    public Color playerOriginal;
    public Color playerShadow;

    bool shadowMode;
    public int collectiblesCollected;

    Color currentColor;
    float ballSpeed = 0.3f;

    void Start()
    {
        playerCollider = GetComponent<CircleCollider2D>();
        currentColor = playerOriginal;
        //GetComponent<Rigidbody2D>().AddForce(transform.up * forcePower);
    }

	// Update is called once per frame
	void Update ()
    {
        if(GameManager.Instance.gamePlaying)
        {
            //transform.position =new Vector3(transform.position.x, Mathf.PingPong(-2.0f,Time.time*2f ), transform.position.z);
            PlayerMovementMobile();
            //KeyboardControl();
            //if (GetComponent<Rigidbody2D>().velocity.y == 0)
            //{
            //    Debug.Log("force given" + GetComponent<Rigidbody2D>().velocity.y);
            //    GetComponent<Rigidbody2D>().AddForce(transform.up * forcePower);
            //}

//            Debug.Log("force:" + GetComponent<Rigidbody2D>().velocity.y);

            if (Input.GetMouseButtonDown(0))
            {
                ShadowMode();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ShadowModeOff();
            }
                

            if(transform.position.y<-5.5f)
            {
                GameManager.Instance.GameOver(false,true);
            }
            
        }
        
	}

    //public  void JumpClick()
    //{
    //    if(GetComponent<Rigidbody2D>().velocity.y ==0)
    //    {
    //        Debug.Log("force given" + GetComponent<Rigidbody2D>().velocity.y);
    //        GetComponent<Rigidbody2D>().AddForce(transform.up * forcePower);
    //    }
        
    //}

    public void PlayerMovementMobile()
    {
        //Vector3 currentVelocity = GetComponent<Rigidbody2D>().velocity;
        //GetComponent<Rigidbody2D>().velocity = new Vector3(5 * movementDirection, currentVelocity.y, 0);

        //MOTION sensor code
        float delta = Input.acceleration.x * ballSpeed;
        transform.position += new Vector3(delta, 0, 0);
    }

    public void ShadowModeBtnClick()
    {
        if (!shadowMode)
            ShadowMode();
    }

    

    void ShadowMode()
    {
        Debug.Log("Shadow");
        shadowMode = true;
        playerCollider.enabled = false;
        GetComponent<SpriteRenderer>().color = playerShadow;

        if(GetComponent<Rigidbody2D>().velocity.y>0)
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        //Debug.Log("force:" + GetComponent<Rigidbody2D>().velocity.y);
        //Debug.Log("shadowMode:" + sMode);
    }
    int i;

    public void ShadowModeOff()
    {
        if(i!=1)
        {
            Debug.Log("Shadow off");
            shadowMode = false;
            playerCollider.enabled = true;
            GetComponent<SpriteRenderer>().color = currentColor;
        }
        
        //Debug.Log("shadowMode: off");
    }

    
    public void IncreaseSize()
    {
        
        //RE-ENABLE IT PLEASE
        //transform.localScale = new Vector3(transform.localScale.x + 1f, transform.localScale.y + 1f, 0);
        //GetComponent<Rigidbody2D>().mass++;
        GetComponent<SpriteRenderer>().color = playerColor[i];
        currentColor = GetComponent<SpriteRenderer>().color;
        i++;

        //GameOver
        if (i==1)
        {
            Debug.Log("GameOver, inside IncreaseSize");
            ShadowMode();
            //GameManager.Instance.gamePlaying = false;
            //Time.timeScale=0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //GameManager.Instance.GameOver(false);
        }
    }
    
    
    public void Stop()
    {
        GetComponent<CircleCollider2D> ().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void Reset()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        i = 0;
        currentColor = playerOriginal;
        GetComponent<SpriteRenderer>().color = currentColor;
        
        transform.localScale = new Vector3(1, 1, 0);
        transform.position = new Vector3(-2f,0,0);
        //collectiblesCollected = 0;
    }
}
