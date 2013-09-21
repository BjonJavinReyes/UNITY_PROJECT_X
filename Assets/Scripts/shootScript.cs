using UnityEngine;
using System.Collections;
using System;

public class shootScript : MonoBehaviour 
{
	
	Color[] colors = { Color.yellow, Color.red};
    int colorIndex = 0;
	public int count = 0;
	public int tapEq = 0;
	
	public GameObject sphere;
    public GameObject portalBullet;
	
	public bool moveHandBone = false;
	bool moveUp = true;
	bool handMoving = false;
	bool continueRotating = true;
	int angleX = 0;
	int tapAngle = 80;
	Vector3 angleFinal;
	
	String msg = "blah";
	public String tapMessage = "null";
	String angleMessage;
	
	public Camera cam;
	
	public Transform hand_r;
	public Transform palm_r;
	public Transform forearm_R;
	public Transform biceps_R;
	public Transform biceps_L;
	public Transform rootBone;
	
	Vector2 tap;
	
	Vector3 origTarget = new Vector3(0,83,-170);
	Vector3 target = new Vector3(0,83,-170);
	CharacterAnim animScript;
	touchMonitor tapScript;
	movement moveScript;
	bool execAgain = false;
	
	bool tempSwipeDisable = false;
	
    //bulletSpawn bulletSpawn;
	Helpers helperScript;
	
	
	
	public delegate void angleEventHandler();
	public static event angleEventHandler angleEvent;
	
	
	
	
	// Use this for initialization
	void Start () 
	{
		moveScript = sphere.GetComponent("movement") as movement;
		animScript = gameObject.GetComponent("CharacterAnim") as CharacterAnim;
		tapScript = sphere.GetComponent("touchMonitor") as touchMonitor;
       // bulletSpawn = sphere.GetComponent("bulletSpawn") as bulletSpawn;
		moveHandBone = false;
		touchMonitor.tapMonitor += tapDetected;
		angleEvent += angleMethod;
		// HANDLE THE CASE WHERE HE INSTANTIATES MULTIPLE BULLETS WHEN ANGLES R LOW...
		// USE EVENTS FOR SWIPE 
		// EVENTS FOR ANIMS
	}
	
	void OnGUI()
    {
        GUI.Label(new Rect(0, 310, 100, 410), msg);
		GUI.Label(new Rect(690,100,50,50),angleMessage);
		GUI.Label(new Rect(200,300,200,100),tapMessage);
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		
		if(moveHandBone)
		{
			
			tapScript.msg2 = "inside move handbone";
			animScript.idleAnimStart = false;
			//animScript.idleStartTime = (int)Time.time;
			print("inside movehandbone!");
			
			
         	if (angleX > tapAngle)
            {
				angleX = tapAngle;
                moveUp = false;
				//angleEvent();
            }
            else if (angleX < 0)
            {
                angleX = 0;
                handMoving = false;
                moveHandBone = false;
                continueRotating = false;
                moveUp = true;
            }
			
			if(angleX == tapAngle)
			{
				moveUp = false;
				angleEvent();
			}
			
			
			if(continueRotating && moveUp)
			{
				angleX += 16; // Speed control for Up rotation - this varies with framrate of game .. so equalize it using some own function!
			}
			else if(continueRotating && !moveUp)
			{
				angleX -= 16;// Speed control for Down rotation
			}
			
			if(continueRotating == false)
			{
				msg = "shot "+tapEq+"times";
				animScript.idleAnimStart = true;
				animScript.idleStartTime = (int)Time.time;
				angleX = 0;
				removeTransforms();
				palm_r.transform.eulerAngles = new Vector3(0,180,90); // reset the palm rotation to idle position
				//tapPosition = new Vector2(-1,-1);
				tapEq = 0;
				
				if(tempSwipeDisable)
				{
					tempSwipeDisable = false;
					moveScript.enableSwipe();
				}
			}
			
			handMoving = true;
		}
		else
		{
			angleX = 0;
			tapScript.msg2 = "out";
		}
			
		
	}
	
	
	void angleMethod()
	{
		bulletSpawn();
	}
	
	void tapDetected(Vector2 tapPosition)
	{
		//tapPosition = tapScript.tap.sendValues();
		//msg = "DETECT!: "+tapPosition;
			
		Vector3 posVector = cam.WorldToScreenPoint(transform.position);
		//Debug.Log(posVector);
       	Vector2 vectorTwo = GUIUtility.ScreenToGUIPoint( new Vector2( posVector.x, posVector.y ) );             //posVector.x maybe replaced by posVector.z based on main camera's rotation.
		int angle = Mathf.RoundToInt(  Helpers.angleCalc( tapPosition, posVector ) );
		int[] resolvedAngles = Helpers.resolveRotation( angle );
		angleFinal = new Vector3( 0, resolvedAngles[0], resolvedAngles[1] );
       
		tapAngle = angle;
		
		//angleMessage = angle.ToString();
		
		moveHandBone = true;
		continueRotating = true;
		addTransforms();
		
		
		if((moveScript.delta == 1) && Mathf.Abs(tapAngle) > 90 )
		{
			moveScript.changeDelta(Mathf.Abs(tapAngle));
			sphere.transform.eulerAngles = new Vector3(0,180,0);
			if(tapAngle > 0)
			{
				tapAngle = tapAngle - ((tapAngle - 90) * 2);
			}
			else
			{
				tapAngle = Mathf.Abs(tapAngle);
				tapAngle = tapAngle - ((tapAngle - 90) * 2);
				tapAngle = -tapAngle;
			}
			
			if(moveScript.swipe)
			{
				moveScript.disableSwipe();
				tempSwipeDisable = true;
			}
			
		}
		else if((moveScript.delta == -1) && Mathf.Abs(tapAngle) <= 90 )
		{
			moveScript.changeDelta(Mathf.Abs(tapAngle));
			sphere.transform.eulerAngles = new Vector3(0,0,0);
			
			//tapAngle = tapAngle + 180;
		}
		else if((moveScript.delta) == -1 && Mathf.Abs(tapAngle) > 90)
		{
			if(tapAngle > 0)
			{
				tapAngle = tapAngle - ((tapAngle - 90) * 2);
			}
			else
			{
				tapAngle = Mathf.Abs(tapAngle);
				tapAngle = tapAngle - ((tapAngle - 90) * 2);
				tapAngle = -tapAngle;
			}
			
		}
		//else if( (moveScript.delta) == 1 && Mathf.Abs(tapAngle - 90) <= 90 )
		
		
		
		tapAngle = tapAngle + 90;
		//angleMessage = tapAngle.ToString();
	}
	
	void removeTransforms()
	{
		animation["idle0"].RemoveMixingTransform(rootBone);
		animation["idle1"].RemoveMixingTransform(rootBone);
		animation["idle2"].RemoveMixingTransform(rootBone);
		animation["idle3"].RemoveMixingTransform(rootBone);
		animation["run"].RemoveMixingTransform(rootBone);
		animation["run"].RemoveMixingTransform(biceps_L);
	}
	
	void addTransforms()
	{
		
		animation["idle0"].AddMixingTransform(rootBone);
		animation["idle1"].AddMixingTransform(rootBone);
		animation["idle2"].AddMixingTransform(rootBone);
		animation["idle3"].AddMixingTransform(rootBone);
		animation["run"].AddMixingTransform(rootBone);
		animation["run"].AddMixingTransform(biceps_L);
	}
	
	void LateUpdate()
	{
		// PC SHOOT - CLICK
//		if(Input.GetMouseButtonDown(0))
//		{
//			moveHandBone = true;
//			continueRotating = true;
//			addTransforms();
//				
//		}
		
		
		
		
		// DROID SHOOT - TAP
		
		
		
		
		if( moveHandBone )
		{
			//print("angleX: "+angleX);
			if(animation.IsPlaying("run"))
			{
				//Debug.Log ("hangle: "+hand_r.localEulerAngles);
				hand_r.localEulerAngles = new Vector3(0,90,-150 - angleX); //biceps_R.transform.eulerAngles.z
				forearm_R.localEulerAngles = new Vector3(0,0,-20); // Refined look while shooting
				//Debug.Log ("hangle: "+hand_r.localEulerAngles);
				handMoving = true;
			}
			else
			{
				hand_r.localEulerAngles = new Vector3(0,83,-170-angleX);
			}
			palm_r.localEulerAngles = new Vector3(0,0,-50);
			//hand_r.localRotation = Quaternion.Euler(new Vector3(0,90,0+angleZ));
				
		}
	}
	
	float angleCalc(Vector2 a, Vector2 b)		//function to calculate angle
    {
        float diffx = a.x - b.x;
        float diffy = a.y - b.y;
        //Debug.Log(diffx.ToString()+","+diffy.ToString()+"difference="+a.x.ToString()+","+a.y.ToString()+"-"+b.x.ToString()+","+b.y.ToString());
        return (Mathf.Atan2(diffy, diffx) * Mathf.Rad2Deg);
    }
	
	void bulletSpawn()
	{
		//msg = "came here at: "+Time.time;
		GameObject bulletInstance = (GameObject)Instantiate( portalBullet, palm_r.position, Quaternion.Euler(angleFinal) );
		count++;
      	portalBullet bulletScript = bulletInstance.GetComponent<portalBullet>();
        bulletScript.setColor( colors[colorIndex] );
        colorIndex = (colorIndex + 1) % 2;
		angleMessage = "inst: "+count;
	}
	
}
