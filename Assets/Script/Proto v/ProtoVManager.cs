using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProtoVManager : MonoBehaviour
{
    #region Instance
    //Static Singleton Instance
    public static ProtoVManager _Instance = null;

    //property to get instance
    public static ProtoVManager Instance
    {
        get
        {
            //if we do not have Instance yet
            if (_Instance == null)
            {
                _Instance = (ProtoVManager)FindObjectOfType(typeof(ProtoVManager));
            }
            return _Instance;
        }
    }
    #endregion

    public GameObject platform;
    public GameObject platformHolder;
    public GameObject collectibles;
    public GameObject collectiblesHolder;
    public Vector3 platformSpawnPosition;
    public Text score;

    [HideInInspector]
    public bool gamePlaying;

    [HideInInspector]
    public int scoreCount;

    // Use this for initialization
    void Start ()
    {
        gamePlaying = true;
        StartCoroutine(GeneratePlatform());
        //StartCoroutine(GenerateCollectibles());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Menu();
    }

    public IEnumerator GeneratePlatform()
    {
        int specialPlatformCount=0;

        while (gamePlaying)
        {
            GameObject tempP = Instantiate(platform);
            tempP.transform.position = platformSpawnPosition;
            tempP.transform.rotation = Quaternion.Euler(0, 0, 0);
            tempP.transform.SetParent(platformHolder.transform, false);

            int r;

            if (specialPlatformCount == 2)
            {
                r = Random.Range(7, 10);
            }
            else if (Time.timeSinceLevelLoad > 60)
            {
                r = Random.Range(1, 10);
            }
            else if(Time.timeSinceLevelLoad>40)
            {
                r = Random.Range(3, 10);
            }
            else if(Time.timeSinceLevelLoad > 15)
            {
                r = Random.Range(5, 10);
            }
            else
            {
                r = Random.Range(7, 10);
            }

            
            if (r == 5|| r == 6)//BOUNCE
            {
                tempP.GetComponent<ProtoPlatform>().myType = ProtoPlatform.PlatformType.Bouncing;
                tempP.GetComponent<ProtoPlatform>().power = 150;
                tempP.SetActive(true);
                specialPlatformCount++;
            }
            else if(r==3|| r == 4)//BLINKING
            {
                tempP.GetComponent<ProtoPlatform>().myType = ProtoPlatform.PlatformType.Blinking;
                tempP.SetActive(true);
                specialPlatformCount++;
            }
            else if(r==1|| r == 2)//MOVING
            {
                tempP.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                tempP.transform.localPosition = new Vector3(-1.3f, platformSpawnPosition.y, 0);
                tempP.GetComponent<ProtoPlatform>().myType = ProtoPlatform.PlatformType.Moving;
                tempP.SetActive(true);
                specialPlatformCount++;
            }
            else
            {
                tempP.GetComponent<ProtoPlatform>().myType = ProtoPlatform.PlatformType.Normal;
                tempP.SetActive(true);
                if(specialPlatformCount>0)
                    specialPlatformCount--;
            }
            Debug.Log(specialPlatformCount);


            if(Random.Range(0,10)<5)
            {
                GameObject tempP2 = Instantiate(collectibles);
                tempP2.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), -3.5f, 0);
                tempP2.transform.rotation = Quaternion.Euler(0, 0, 0);
                tempP2.transform.SetParent(tempP.transform, true);
            }
            

            yield return new WaitForSeconds(2f);

            //if (Time.timeSinceLevelLoad > 10)
            //    Camera.main.transform.rotation = Quaternion.Euler(0,0,180);
        }
    }

    public IEnumerator GenerateCollectibles()
    {
        yield return new WaitForSeconds(5f);

        while (gamePlaying)
        {
            GameObject tempP = Instantiate(collectibles);
            tempP.transform.position = new Vector3(Random.Range(-2.5f,2.5f), Random.Range(0,-2.5f),0);
            tempP.transform.rotation = Quaternion.Euler(0, 0, 0);
            tempP.transform.SetParent(collectiblesHolder.transform, false);

            yield return new WaitForSeconds(5f);

            Destroy(tempP);
        }
    }

    public void SetScore()
    {
        score.text = "Score: " + scoreCount.ToString();
    }

    public void GameOver()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Menu()
    {
        SceneManager.LoadScene(1);
    }

}
