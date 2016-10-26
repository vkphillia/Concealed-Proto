using UnityEngine;
using System.Collections;

public class ObstaclePooler : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject platform;
    public Vector3 spawnPosition;
    int platformCount;

    public IEnumerator GenerateObstacle()
    {
        //StartCoroutine(GeneratePlatform());

        while(GameManager.Instance.gamePlaying)
        {
            GameObject tempO = Instantiate(obstacle);
            tempO.transform.position = spawnPosition;
            tempO.transform.rotation = Quaternion.Euler(0, 0, 90);
            tempO.transform.SetParent(transform, false);

            if (Time.timeSinceLevelLoad > 20 && platformCount == 0)
            {
                GeneratePlatform();
                platformCount++;
            }
            else if (Time.timeSinceLevelLoad > 30 && platformCount == 1)
            {
                GeneratePlatform();
                platformCount++;
            }
            else if (Time.timeSinceLevelLoad > 10 && Time.timeSinceLevelLoad < 20)
                yield return new WaitForSeconds(1.64f);
            else if (Time.timeSinceLevelLoad > 30)
                yield return new WaitForSeconds(1.64f);
            else
                yield return new WaitForSeconds(1.64f * 2f);
        }

        //GameManager.Instance.GameOver();
        //Debug.Log("time:" + Time.timeSinceLevelLoad);

    }

    void GeneratePlatform()
    {
        GameObject tempP = Instantiate(platform);
        tempP.transform.position = new Vector3(9, -1.9f, 0);
        tempP.transform.rotation = Quaternion.Euler(0, 0, 0);
        tempP.transform.SetParent(transform, false);
        tempP.GetComponent<PlatformControl>().isMoving = true;
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
