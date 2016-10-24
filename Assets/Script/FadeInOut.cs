using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    #region Instance
    //Static Singleton Instance
    public static FadeInOut _Instance = null;

    //property to get instance
    public static FadeInOut Instance
    {
        get
        {
            //if we do not have Instance yet
            if (_Instance == null)
            {
                _Instance = (FadeInOut)FindObjectOfType(typeof(FadeInOut));
            }
            return _Instance;
        }
    }
    #endregion

    //this function works both as fade in and fade out for gameObjects with sprite renderer
    public IEnumerator FadeGameObject(SpriteRenderer objectToFade, float fromAlpha, float toAlpha, float duration)
    {
        Color temp = objectToFade.color;
        temp.a = fromAlpha;
        Color colorStart = temp;
        temp.a = toAlpha;
        Color colorEnd = temp;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            objectToFade.color = Color.Lerp(colorStart, colorEnd, t / duration);
            yield return null;
        }
        if (objectToFade.color.a != toAlpha)
        {
            objectToFade.color = colorEnd;
        }
    }


    //this function works both as fade in and fade out for canvas elements with canvas group
    public IEnumerator FadeCanvasElements(CanvasGroup objectToFade, float fromAlpha, float toAlpha, float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            objectToFade.alpha = Mathf.Lerp(fromAlpha, toAlpha, t / duration);
            yield return null;
        }
        if (objectToFade.alpha != toAlpha)
        {
            objectToFade.alpha = toAlpha;
        }
    }

}
