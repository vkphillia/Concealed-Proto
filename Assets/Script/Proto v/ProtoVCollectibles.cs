using UnityEngine;
using System.Collections;

public class ProtoVCollectibles : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ProtoVManager.Instance.scoreCount++;//Increase score
            ProtoVManager.Instance.SetScore();

            Destroy(this.gameObject);
        }
    }
}
