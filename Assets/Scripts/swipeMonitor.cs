using UnityEngine;
using System.Collections;
using System;

public class Swipe
{
    public float swipeDistance;
    public float swipeAngle;
	public bool swipeStart = false;
	public bool swipeEnd = false;
    public bool swipeActive;         //Indicates if occuring on screen presently
    public int fingerId;

    public Swipe()
    {
        this.fingerId = -1;
    }

    public void updateValues(float swipeDistance, float swipeAngle, int Id)
    {
        this.swipeDistance = swipeDistance;
        this.swipeAngle = swipeAngle;
        this.swipeActive = true;
        this.fingerId = Id;
        swipeMonitor.msg5 = "swipeDist " + swipeDistance.ToString();
        swipeMonitor.msg6 = "swipeAngle " + swipeAngle.ToString();
    }

    public void makeInactive()
    {
        this.swipeActive = false;
    }
	
	public void resetFingerId()
	{
		this.fingerId = -1;
	}
	
}

public class swipeMonitor : MonoBehaviour {
    public Swipe swipe = new Swipe();
    public float minSwipeDist = 15.0f;		//minimum distance to detect swipe
    public Touch touch;
    public bool execFlag = false;
	
	CharacterAnim animScript;
	public GameObject character;
	
    public string msg3 = "msg3";
    public string msg4 = "msg4";
    public static string msg5 = "msg5";
    public static string msg6 = "msg6";
	private static int counter = 0;

	// Use this for initialization
	void Start () 
	{
		animScript = character.GetComponent("CharacterAnim") as CharacterAnim;
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(330, 0, 430, 100), msg3);
        GUI.Label(new Rect(440, 0, 540, 100), msg4);
        GUI.Label(new Rect(550, 0, 650, 100), msg5);
        GUI.Label(new Rect(0, 210, 100, 210), msg6);
    }

    public void setTouch(Touch transferredTouch)
    {
        touch = transferredTouch;
        execFlag = true;
		counter += 1;
		msg3 = "swipe Counter:" + counter;
    }

	// Update is called once per frame
	void LateUpdate ()
    {
        if (execFlag)
            touchThread();
	}
    public void touchThread()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touchUpdate = Input.GetTouch(i);
            if (touchUpdate.fingerId == touch.fingerId)
            {
                switch (touchUpdate.phase)
                {
                    case TouchPhase.Moved:
						swipe.swipeEnd = false;
                        float swipeDist = Mathf.Abs(touchUpdate.position.magnitude - touch.position.magnitude);
                        if (swipeDist > minSwipeDist)
                        {
							if(swipe.swipeStart == false)
							{
								swipe.swipeStart = true;
								//animScript.playStartWalkAnim();
							}
                            float angle = Helpers.angleCalc(touchUpdate.position, touch.position);
                            swipe.updateValues(swipeDist, angle, touch.fingerId);	
                        }
                        else
                        {
                            msg6 = "making inactive";
                            swipe.makeInactive();
                        }
                        break;
                    case TouchPhase.Ended:
						swipe.swipeEnd = true;
						swipe.swipeStart = false;
                        swipe.makeInactive();
						swipe.resetFingerId();
                        execFlag = false;
						animScript.idleStartTime = (int)Time.time;
                        break;
                }
            }
        }
    }
}
