using UnityEngine;
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

    void Start()
    {
        playerCollider = GetComponent<CircleCollider2D>();
        currentColor = playerOriginal;
        //GetComponent<Rigidbody2D>().AddForce(transform.up * forcePower);
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        if(GameManager.Instance.gamePlaying)
        {
            //transform.position =new Vector3(transform.position.x, Mathf.PingPong(-2.0f,Time.time*2f ), transform.position.z);
            //MobileControl();
            //KeyboardControl();
            //if (GetComponent<Rigidbody2D>().velocity.y == 0)
            //{
            //    Debug.Log("force given" + GetComponent<Rigidbody2D>().velocity.y);
            //    GetComponent<Rigidbody2D>().AddForce(transform.up * forcePower);
            //}

//            Debug.Log("force:" + GetComponent<Rigidbody2D>().velocity.y);

            if (Input.GetButton("Fire1"))
            {
                ShadowMode();
            }
            else
                ShadowModeOff();

            if(transform.position.y<-5.5f)
            {
                GameManager.Instance.GameOver(false,true);
            }
            
        }
        else
        {
            //GetComponent<Rigidbody2D>().isKinematic = true;
        }
        
	}

    public  void JumpClick()
    {
        if(GetComponent<Rigidbody2D>().velocity.y ==0)
        {
            Debug.Log("force given" + GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().AddForce(transform.up * forcePower);
        }
        
    }

    public void ShadowModeBtnClick()
    {
        if (!shadowMode)
            ShadowMode();
    }

    //void KeyboardControl()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
            
    //    }
    //    else if(Input.GetKeyDown(KeyCode.S))
    //    {
            
    //    }
    //    else
    //    {
    //        ShadowModeOff();
    //    }
    //}

    //void MobileControl()
    //{
    //    int count = Input.touchCount;

    //    if(count==0)
    //    {
    //        ShadowModeOff();
    //    }

    //    for (int i = 0; i < count; i++)
    //    {
    //        Touch touch = Input.GetTouch(i);

    //        if (touch.position.x < Screen.width / 2)
    //        {
    //            if (GetComponent<Rigidbody2D>().velocity.y == 0)
    //            {
    //                //Debug.Log("force given" + GetComponent<Rigidbody2D>().velocity.y);
    //                GetComponent<Rigidbody2D>().AddForce(transform.up * forcePower);
    //            }
    //        }
    //        else if (touch.position.x > Screen.width / 2)
    //        {
    //            if (!shadowMode)
    //                ShadowMode();
    //        }
    //    }
    //}

    void ShadowMode()
    {
        shadowMode = true;
        playerCollider.enabled = false;
        GetComponent<SpriteRenderer>().color = playerShadow;
        //Debug.Log("force:" + GetComponent<Rigidbody2D>().velocity.y);
        //Debug.Log("shadowMode:" + sMode);
    }
    int i;

    public void ShadowModeOff()
    {
        if(i!=1)
        {
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
