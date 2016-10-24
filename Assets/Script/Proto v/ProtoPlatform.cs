using UnityEngine;
using System.Collections;

public class ProtoPlatform : MonoBehaviour
{
    public enum PlatformType
    {
        Normal,LeftSide,RightSide
    }

    public PlatformType myType;
    
    public float power;

	// Update is called once per frame
	void Update ()
    {
        if (ProtoVManager.Instance.gamePlaying && myType==PlatformType.Normal)
        {
            //Debug.Log(transform.position);
            transform.Translate(Vector3.up.normalized * 0.025f);

            if (transform.position.y > 7)
                Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("coll");

        if (other.gameObject.tag == "Player" && myType==PlatformType.LeftSide)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(other.gameObject.transform.right * power);
        }
        else if(other.gameObject.tag == "Player" && myType == PlatformType.RightSide)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(-other.gameObject.transform.right * power);
        }
    }
}
