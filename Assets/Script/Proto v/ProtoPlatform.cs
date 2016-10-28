using UnityEngine;
using System.Collections;

public class ProtoPlatform : MonoBehaviour
{
    public enum PlatformType
    {
        Normal,LeftSide,RightSide,Blinking,Bouncing,Moving
    }

    public PlatformType myType;
    
    public float power;
    public float platformUpSpeed;

    void Start()
    {
        if(myType==PlatformType.Blinking)
        {
            StartCoroutine(Blinking());
        }
    }
    
	// Update is called once per frame
	void Update ()
    {
        if (ProtoVManager.Instance.gamePlaying)
        {
            if(myType != PlatformType.LeftSide && myType != PlatformType.RightSide)
            {
                transform.Translate(Vector3.up.normalized * platformUpSpeed);
            }

            if (myType == PlatformType.Moving)
            {
                transform.Translate(Vector3.right.normalized * 0.02f);
            }
            
            if (transform.position.y > 7)
                Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("coll");

        if (other.gameObject.tag == "Player" && myType==PlatformType.LeftSide)
        {
            if(Time.timeSinceLevelLoad > 90)
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(other.gameObject.transform.right * (power+50));
            else
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(other.gameObject.transform.right * power);
        }
        else if(other.gameObject.tag == "Player" && myType == PlatformType.RightSide)
        {
            if (Time.timeSinceLevelLoad > 90)
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(-other.gameObject.transform.right * (power+50));
            else
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(-other.gameObject.transform.right * power);
        }
        else if (other.gameObject.tag == "Player" && myType == PlatformType.Bouncing)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(other.gameObject.transform.up * power);
        }
        //else if(other.gameObject.tag == "Player")
        //{
        //    if (other.gameObject.GetComponent<Rigidbody2D>().velocity.x < 1)
        //        other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(other.gameObject.GetComponent<Rigidbody2D>().velocity.x + 1, 0, 0);
        //    //Debug.Log(other.gameObject.GetComponent<Rigidbody2D>().velocity);
        //}

    }

    IEnumerator Blinking()
    {
        while (ProtoVManager.Instance.gamePlaying)
        {
            yield return new WaitForSeconds(0.5f);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            yield return new WaitForSeconds(0.5f);
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }
    }

}
