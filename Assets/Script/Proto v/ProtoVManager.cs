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
    public GameObject gameOverPopUp;
    public Vector3 platformSpawnPosition;
    public Text score;
    public Text finalScore;

    [HideInInspector]
    public bool gamePlaying;

    [HideInInspector]
    public int scoreCount;

    // Use this for initialization
    void Start ()
    {
        gamePlaying = true;
        StartCoroutine(GeneratePlatform());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Menu();
    }

    public IEnumerator GeneratePlatform()
    {
        //yield return new WaitForSeconds(2f);

        int specialPlatformCount=0;
        float platformSpeed =0.015f;

        while (gamePlaying)
        {
            GameObject tempP = Instantiate(platform);
            tempP.transform.position = new Vector3(Random.Range(-2.76f,2.76f), platformSpawnPosition.y,0);
            tempP.transform.localScale = new Vector3(1,Random.Range(0.2f,1),1);
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

            if (platformSpeed < 0.06f)
                platformSpeed = 0.015f + Mathf.FloorToInt(Time.timeSinceLevelLoad) / 2000f;

            //Debug.Log(platformSpeed);
            
            if (r == 5|| r == 6)//BOUNCE
            {
                tempP.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                tempP.GetComponent<ProtoPlatform>().myType = ProtoPlatform.PlatformType.Bouncing;
                tempP.GetComponent<ProtoPlatform>().power = 150;
                tempP.GetComponent<ProtoPlatform>().platformUpSpeed = platformSpeed;
                tempP.SetActive(true);
                specialPlatformCount++;
            }
            else if(r==3|| r == 4)//BLINKING
            {
                tempP.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                tempP.GetComponent<ProtoPlatform>().myType = ProtoPlatform.PlatformType.Blinking;
                tempP.GetComponent<ProtoPlatform>().platformUpSpeed = platformSpeed;
                tempP.SetActive(true);
                specialPlatformCount++;
            }
            else if(r==1|| r == 2)//MOVING
            {
                tempP.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                tempP.transform.localPosition = new Vector3(-1.3f, platformSpawnPosition.y, 0);
                tempP.GetComponent<ProtoPlatform>().myType = ProtoPlatform.PlatformType.Moving;
                tempP.GetComponent<ProtoPlatform>().platformUpSpeed = platformSpeed;
                tempP.SetActive(true);
                specialPlatformCount++;
            }
            else//NORMAL
            {
                tempP.GetComponent<ProtoPlatform>().myType = ProtoPlatform.PlatformType.Normal;
                tempP.GetComponent<ProtoPlatform>().platformUpSpeed = platformSpeed;
                tempP.GetComponent<ProtoPlatform>().power = 150;
                tempP.SetActive(true);
                if(specialPlatformCount>0)
                    specialPlatformCount--;
            }
            //Debug.Log(specialPlatformCount);

            //COLLECTIBLES
            if(Random.Range(0,10)<11)
            {
                GameObject tempP2 = Instantiate(collectibles);
                tempP2.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), -5.5f, 0);
                tempP2.transform.rotation = Quaternion.Euler(0, 0, 0);
                tempP2.transform.SetParent(tempP.transform, true);
            }
            

            yield return new WaitForSeconds(Random.Range(2,3));

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
        score.text =  scoreCount.ToString();
    }

    public void GameOver()
    {
        StopAllCoroutines();
        gamePlaying = false;
        finalScore.text = score.text;
        score.gameObject.SetActive(false);
        gameOverPopUp.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Menu()
    {
        SceneManager.LoadScene(1);
    }

}
