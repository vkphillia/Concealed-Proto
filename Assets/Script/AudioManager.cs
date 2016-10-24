using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    #region Instance
    //Static Singleton Instance
    public static AudioManager _Instance = null;

    //property to get instance
    public static AudioManager Instance
    {
        get
        {
            //if we do not have Instance yet
            if (_Instance == null)
            {
                _Instance = (AudioManager)FindObjectOfType(typeof(AudioManager));
            }
            return _Instance;
        }
    }
    #endregion

    AudioSource bgMusic;

    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()
    {
        bgMusic = GetComponent<AudioSource>();
        
    }

    public void PlayMusic()
    {
        StartCoroutine(FadeIn(bgMusic));
    }

    IEnumerator FadeIn(AudioSource audio)
    {
        Debug.Log("playing");
        //yield return new WaitForSeconds(2f);
        audio.Play();
        audio.volume = 0;
        float audio2vol = 0;
        while (audio2vol < 0.5f)
        {
            audio2vol += (0.2f * Time.deltaTime);
            audio.volume = audio2vol;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
