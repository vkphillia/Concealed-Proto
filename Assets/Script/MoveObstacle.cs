using UnityEngine;
using System.Collections;

public class MoveObstacle : MonoBehaviour
{
   

	// Update is called once per frame
	void Update ()
    {
        if (GameManager.Instance.gamePlaying)
        {
            //transform.Translate(Vector3.up * Time.deltaTime * 2);
        }
        else
        {
            //Debug.Log("not playing");
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            //Debug.Log("triggered");
            //other.GetComponent<Rigidbody2D>().isKinematic = true;
            other.gameObject.GetComponent<PlayerControl>().IncreaseSize();
        }
    }
    
}
