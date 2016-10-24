using UnityEngine;
using System.Collections;

public class ObstaclePooler : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject platform;
    public Vector3 spawnPosition;

	// Use this for initialization
	void Start ()
    {
	
	}
	

    public IEnumerator GenerateObstacle(int noOfObstacle)
    {
        StartCoroutine(GeneratePlatform());

        while(GameManager.Instance.gamePlaying && noOfObstacle>0)
        {
            GameObject tempO = Instantiate(obstacle);
            tempO.transform.position = spawnPosition;
            tempO.transform.rotation = Quaternion.Euler(0, 0, 90);
            tempO.transform.SetParent(transform, false);

            noOfObstacle--;

            yield return new WaitForSeconds(2f);
        }

        //GameManager.Instance.GameOver();
        Debug.Log("time:" + Time.timeSinceLevelLoad);

    }

    public IEnumerator GeneratePlatform()
    {
        while (GameManager.Instance.gamePlaying)
        {
            GameObject tempP = Instantiate(platform);
            tempP.transform.position = new Vector3(9,-3.39f,0);
            tempP.transform.rotation = Quaternion.Euler(0, 0, 0);
            tempP.transform.SetParent(transform, false);

            yield return new WaitForSeconds(5f);
        }
    }

    //IEnumerator MoveToStart()
    //{
    //    GameObject objectToMove = this.gameObject;

    //    Vector3 end = new Vector3(0, 0, -10);
    //    float speed = 2f;

    //    float t = 0;
    //    while (objectToMove.transform.position != end)
    //    {
    //        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.fixedDeltaTime);
    //        yield return new WaitForEndOfFrame();
    //        t += Time.deltaTime;
    //    }
    //}
}
