using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Instance
    //Static Singleton Instance
    public static GameManager _Instance = null;

    //property to get instance
    public static GameManager Instance
    {
        get
        {
            //if we do not have Instance yet
            if (_Instance == null)
            {
                _Instance = (GameManager)FindObjectOfType(typeof(GameManager));
            }
            return _Instance;
        }
    }
    #endregion

    public GameObject title;
    public GameObject gameOverUI;
    public GameObject instruction;
    public GameObject[] collectibles;
    public GameObject bgChilds;
    public ObstaclePooler obstaclePooler;
    public PlayerControl player;
    public Vector3 playerPosition;
    public bool gamePlaying;
    public int noOfObstacle;
    public int levelNo;
    public Text score;

    void Start()
    {
        AudioManager.Instance.PlayMusic();//audio
    }

    public void PlayGame()
    {
        
        gamePlaying = true;
        player.gameObject.SetActive(true);
        StartCoroutine(obstaclePooler.GenerateObstacle());//obstacles
        player.transform.position = playerPosition;
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        title.SetActive(false);
        StartCoroutine(SetScore());
    }
    
    IEnumerator SetScore()
    {
        int scoreCount = 0;

        while(GameManager.Instance.gamePlaying)
        {
            score.text = "Distance covered: " +scoreCount.ToString();
            yield return new WaitForSeconds(0.2f);
            scoreCount += 1;
        }
    }

    public void GameOver(bool win,bool end)
    {
        if(end)
        {
            gamePlaying = false;
            StartCoroutine(LoadMenu());
            return;
        }

        if(win && player.collectiblesCollected == 3)//GAME END CASE
        {
            gamePlaying = false;
            player.gameObject.SetActive(false);
            bgChilds.SetActive(false);
            StartCoroutine(MoveCameraToStart(new Vector3(93, 1, -10), true,10f));
        }

        else if(win)//Level Over
        {
            levelNo++;   
        }
        else 
        {
            gamePlaying = false;
            StartCoroutine(MoveCameraToStart(new Vector3(0, 1, -10), false,17f));
            player.gameObject.SetActive(true);
        }
    }

    //MOVING CAMERA TO START POINT
    IEnumerator MoveCameraToStart(Vector3 end,bool gameOver,float speed)
    {
        GameObject objectToMove = Camera.main.gameObject;

        //Vector3 end = new Vector3(0, 0, -10);
        //float speed = 17f;

        float t = 0;
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.fixedDeltaTime);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }

        
        if (gameOver)
        {
            //over
            gameOverUI.SetActive(true);
            Invoke("LoadMenu",5F);
        }
        else
        {
            ResetLevel();
        }

    }

    void ResetLevel()
    {
        collectibles[levelNo].SetActive(true);
        player.gameObject.SetActive(true);
        player.Reset();
        GameManager.Instance.gamePlaying = true;
        
        
    }

    public void PlayProtoV()
    {
        SceneManager.LoadScene(2);
    }

    IEnumerator LoadMenu()
    {
        Debug.Log("Reloading scene");
        //score.rectTransform.transform.localPosition = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
