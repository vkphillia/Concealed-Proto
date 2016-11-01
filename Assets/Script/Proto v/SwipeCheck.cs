using UnityEngine;
using System.Collections;

public class SwipeCheck : MonoBehaviour
{

    float startTime;
    Vector2 startPos;
    bool couldBeSwipe;
    float comfortZone;
    float minSwipeDist;
    float maxSwipeTime;
    bool verticleSwipe;
    bool horizontalSwipe;
     
    ProtoVPlayer player;

    void Start()
    {
        Debug.Log("Swipe started");
        comfortZone = 50;
        maxSwipeTime = 1;
        minSwipeDist = 0.5f;
        player = GetComponent<ProtoVPlayer>();

    }
 
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.touches[0];

            Debug.Log(touch.phase);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    couldBeSwipe = true;
                    startPos = touch.position;
                    startTime = Time.time;
                    break;

                //case TouchPhase.Moved:
                //    if (Mathf.Abs(touch.position.x - startPos.x) < comfortZone)
                //    {
                //        Debug.Log("comfortZone:" + comfortZone + "cal:" + Mathf.Abs(touch.position.x - startPos.x));
                //        couldBeSwipe = false;
                //    }
                //    break;

                case TouchPhase.Stationary:
                    Debug.Log("Stationary");
                    //if ((Time.time - startTime) > 0.05f)
                        //player.ShadowMode();
                    break;

                case TouchPhase.Ended://ALL SWIPE DETECTION HERE
                    //player.ShadowModeOff();
                    var swipeTime = Time.time - startTime;
                    var swipeDist = (touch.position - startPos).magnitude;

                    horizontalSwipe = true;
                    verticleSwipe = true;

                    if (Mathf.Abs(touch.position.x - startPos.x) < comfortZone)
                    {
                        Debug.Log("comfortZone:" + comfortZone + "cal:" + Mathf.Abs(touch.position.x - startPos.x));
                        horizontalSwipe = false;
                    }
                    if(Mathf.Abs(touch.position.y - startPos.y) < comfortZone)
                    {
                        verticleSwipe = false;
                    }

                    if(!verticleSwipe && !horizontalSwipe)
                    {
                        couldBeSwipe = false;
                    }

                    Debug.Log("couldBeSwipe:" + couldBeSwipe);
                    Debug.Log("swipeDist:"+swipeDist);
                    Debug.Log("minSwipeDist:" + minSwipeDist);
                    Debug.Log("SwipeTime:" + swipeTime);
                    Debug.Log("maxSwipeTime:" + maxSwipeTime);


                    if (couldBeSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDist))
                    {
                        // It's a swiiiiiiiiiiiipe!
                        float swipeDirection;

                        if (horizontalSwipe)
                        {
                            swipeDirection = Mathf.Sign(touch.position.x - startPos.x);
                        }
                        else
                        {
                            swipeDirection = Mathf.Sign(touch.position.x - startPos.x);
                            if(swipeDirection==-1)
                                player.ShadowMode();
                            break;
                        }

                        player.movementDirection = swipeDirection;
                        player.PlayerMovementMobile();
                        Debug.Log("Swipe:"+swipeDirection);
                        // Do something here in reaction to the swipe.
                    }
                    break;
            }
        }
    }
}
