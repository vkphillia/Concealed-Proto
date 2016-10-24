using UnityEngine;
using System.Collections;

public class BGMovement : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        if (GameManager.Instance.gamePlaying)
        {
            transform.Translate(Vector3.left * speed);
        }
    }
	
}
