using UnityEngine;
using System.Collections;

public class CollectiblesControl : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControl>().collectiblesCollected++;
            //gameover
            GameManager.Instance.GameOver(true,false);
            this.gameObject.SetActive(false);
        }
    }
}
