using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public class Tap
{
    public Vector2 tap;
    private bool isLatest;

    public Vector2 sendValues()
    {
        if (isLatest == true)
        {
            isLatest = false;
            return this.tap;
        }
        else
            return new Vector2(-1,-1);
    }

    public void updateValues(Vector2 touch)
    {
        this.tap = touch;
        isLatest = true;
    }
}

public class touchMonitor : MonoBehaviour {
	public delegate void tapEventHandler( Vector2 tap);
	public static event tapEventHandler tapMonitor;
    public Tap tap = new Tap();
    public string msg = "msg";
    public string msg1 = "msg1";
    public string msg2 = "msg2";
    public swipeMonitor swipeScript;
	private static int counter = 0;

	// Use this for initialization
	void Start ()
    {
        swipeScript = gameObject.GetComponentInChildren<swipeMonitor>();
	}

    void OnGUI()
    {
        GUI.Label(new Rect(0, 100, 100, 200), msg);
        GUI.Label(new Rect(110, 0, 210, 100), msg1);
        GUI.Label(new Rect(220, 0, 320, 100), msg2);
    }

	// Update is called once per frame
	void Update ()
    {
        for ( int i = 0; i < Input.touchCount; i++ )
        {
            Touch touch = Input.GetTouch( i );
            msg1 = "touchid " + touch.fingerId.ToString();
            //msg2 = "swipe " + swipe.fingerId.ToString();
			if( touch.fingerId != swipeScript.swipe.fingerId )
			{
	            if( touch.phase == TouchPhase.Ended )
	            {
                    msg = "was a tap";
                    tap.updateValues( touch.position );
					Vector2 touchPos = tap.sendValues();
					tapMonitor(touchPos);
					counter += 1;
					msg2 = "tap count:" + counter;
	            }
	            else if( touch.phase == TouchPhase.Moved )
	            {
	                msg = "swipe";
	                if ( swipeScript.execFlag == false)
					{
	                    swipeScript.setTouch( touch );
	                }
	            }
			}
        }
	}
}
