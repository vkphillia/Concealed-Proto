using UnityEngine;
using System.Collections;

public class ProtoVPlayer : MonoBehaviour
{
    public Color playerOriginal;
    public Color playerShadow;
    public float outerX;
    public float outerY;

    [HideInInspector]
    public float movementDirection = 1;

    CircleCollider2D playerCollider;
    bool shadowMode;
    Color currentColor;
    float ballSpeed;

    void Start()
    {
        playerCollider = GetComponent<CircleCollider2D>();
        currentColor = playerOriginal;
        //GetComponent<Rigidbody2D>().AddForce(transform.right * 150);
        //StartCoroutine(IncreaseBallSpeed()); //Increase ball speed gradually
        ballSpeed = 0.3f;
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
            
            //if (Input.touchCount > 0)
            //{
            //    var touch = Input.touches[0];

            //    Debug.Log(touch.phase);

            //    switch (touch.phase)
            //    {
            //        case TouchPhase.Began:
            //            startTime = Time.time;
            //            break;

            //        case TouchPhase.Stationary:
            //            Debug.Log("Stationary");
            //            ShadowMode();
            //            break;

            //        case TouchPhase.Ended:
            //            ShadowModeOff();
            //            break;
            //    }
            //}

            KeyboardMovement();
            PlayerMovementMobile();

            if (Input.GetButton("Fire1"))
            {
                ShadowMode();
            }
            else
                ShadowModeOff();
        }
    }
    
    public void PlayerMovementMobile()
    {
        //Vector3 currentVelocity = GetComponent<Rigidbody2D>().velocity;
        //GetComponent<Rigidbody2D>().velocity = new Vector3(5 * movementDirection, currentVelocity.y, 0);

        //MOTION sensor code
        float delta = Input.acceleration.x * ballSpeed;
        transform.position += new Vector3(delta, 0, 0);
    }
    

    void KeyboardMovement()
    {
        Vector3 currentVelocity = GetComponent<Rigidbody2D>().velocity;
        //Debug.Log(currentVelocity);

        if (Input.GetButton("Fire1"))
        {
            ShadowMode();
        }
        else
            ShadowModeOff();

        if (Input.GetKeyDown(KeyCode.A))
        {
            movementDirection = -1;
            GetComponent<Rigidbody2D>().velocity = new Vector3(5 * movementDirection, currentVelocity.y, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movementDirection = 1;
            GetComponent<Rigidbody2D>().velocity = new Vector3(5 * movementDirection, currentVelocity.y, 0);
        }
        
    }

    //IEnumerator IncreaseBallSpeed()
    //{
    //    while(ProtoVManager.Instance.gamePlaying)
    //    {
    //        yield return new WaitForSeconds(2);
    //        ballSpeed += 0.01f;
    //    }
    //}

    public void ShadowMode()
    {
        shadowMode = true;
        playerCollider.enabled = false;
        GetComponent<SpriteRenderer>().color = playerShadow;
        //GetComponent<Rigidbody2D>().velocity;
        //Invoke("ShadowModeOff",0.5f);
    }

    public void ShadowModeOff()
    {
            shadowMode = false;
            playerCollider.enabled = true;
            GetComponent<SpriteRenderer>().color = currentColor;
    }
}
