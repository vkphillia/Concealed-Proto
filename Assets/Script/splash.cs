using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class splash : MonoBehaviour
{
    SpriteRenderer tempSR;

    void Start()
    {
        tempSR = GetComponent<SpriteRenderer>();

        StartCoroutine(FadeInOut.Instance.FadeGameObject(tempSR, 0, 1, 1));
        StartCoroutine(SplashEverything());
    }
    
    IEnumerator SplashEverything()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(FadeInOut.Instance.FadeGameObject(tempSR, 1, 0, 0.5f));
        yield return new WaitForSeconds(0.5f);
        LoadGame();
    }
    
    void LoadGame()
    {
        SceneManager.LoadScene(1);

    }

}
