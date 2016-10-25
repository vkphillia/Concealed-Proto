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
        StartCoroutine(GenerateCollectibles());
    }

    public IEnumerator GeneratePlatform()
    {
        while (gamePlaying)
        {
            GameObject tempP = Instantiate(platform);
            tempP.transform.position = platformSpawnPosition;
            tempP.transform.rotation = Quaternion.Euler(0, 0, 0);
            tempP.transform.SetParent(platformHolder.transform, false);

            yield return new WaitForSeconds(2f);
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
}
