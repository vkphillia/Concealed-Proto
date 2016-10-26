using UnityEngine;
using System.Collections;

public class PlatformControl : MonoBehaviour
{
    public float power;
    public bool isMoving;

	// Update is called once per frame
	void FixedUpdate ()
    {
        if (GameManager.Instance.gamePlaying && isMoving)
        {
            //Debug.Log(transform.position);
            transform.Translate(-Vector3.right.normalized * 0.05f);

            if (transform.position.x < -9)
                Destroy(gameObject);
            //if (transform.position.x >= 33f && GameManager.Instance.levelNo == 0)
            //{
            //    Debug.Log("camera Reached1");
            //    GameManager.Instance.GameOver(false,false);
            //}
            //else if(transform.position.x >= 70f && GameManager.Instance.levelNo == 1)
            //{
            //    Debug.Log("camera Reached2");
            //    GameManager.Instance.GameOver(false,false);
            //}
            //else if(transform.position.x >= 88f && GameManager.Instance.levelNo == 2)
            //{
            //    Debug.Log("camera Reached3");
            //    GameManager.Instance.GameOver(false,false);
            //}
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("coll");

        if (other.gameObject.tag == "Player")
        {
            //other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-1,0);
            Debug.Log(Time.timeSinceLevelLoad);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(other.gameObject.transform.up * power);
        }
    }

    //IEnumerator Restart()
    //{
    //    Debug.Log("Playing");
    //    GameManager.Instance.gamePlaying = false;
    //    GetComponentInChildren<CircleCollider2D>().enabled = false;
    //    GetComponentInChildren<Rigidbody2D>().isKinematic = true;
    //    GameObject objectToMove = this.gameObject;
    //    Vector3 end = new Vector3(0, 0, -10);
    //    float speed = 12f;
    //    float t = 0;
    //    while (objectToMove.transform.position != end)
    //    {
    //        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.fixedDeltaTime);
    //        yield return new WaitForEndOfFrame();
    //        t += Time.deltaTime;
    //    }
    //}
}
