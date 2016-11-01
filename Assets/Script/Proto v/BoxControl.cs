using UnityEngine;
using System.Collections;

public class BoxControl : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Inside");

        //if (other.gameObject.tag == "Player")
        //    Destroy(this.gameObject);
    }
}
