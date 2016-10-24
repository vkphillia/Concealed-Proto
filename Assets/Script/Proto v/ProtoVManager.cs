using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public Vector3 platformSpawnPosition;


    public bool gamePlaying;

    // Use this for initialization
    void Start ()
    {
        gamePlaying = true;
        StartCoroutine(GeneratePlatform());
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

    public void GameOver()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
