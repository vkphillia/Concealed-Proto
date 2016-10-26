using UnityEngine;
using System.Collections;

public class MoveObstacle : MonoBehaviour
{
   

	// Update is called once per frame
	void Update ()
    {
        if (GameManager.Instance.gamePlaying)
        {
            transform.Translate(Vector3.up.normalized * 0.05f);

            if (transform.position.x < -5)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            //Debug.Log("triggered");
            other.gameObject.GetComponent<PlayerControl>().IncreaseSize();
        }
    }
    
}
