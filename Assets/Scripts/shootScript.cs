using UnityEngine;
using System.Collections;
using System;

public class shootScript : MonoBehaviour 
{
	
	Color[] colors = { Color.yellow, Color.red};
    int colorIndex = 0;
	public int count = 0;
	
	public GameObject sphere;
    public GameObject portalBullet;
	
	public bool moveHandBone = false;
	bool moveUp = true;
	bool handMoving = false;
	bool continueRotating = true;
	float angleX = 0;
	float tapAngle = 80;
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
	bool execAgain = false;
    //bulletSpawn bulletSpawn;
	Helpers helperScript;
	
	// Use this for initialization
	void Start () 
	{
		
		animScript = gameObject.GetComponent("CharacterAnim") as CharacterAnim;
		tapScript = sphere.GetComponent("touchMonitor") as touchMonitor;
       // bulletSpawn = sphere.GetComponent("bulletSpawn") as bulletSpawn;
		moveHandBone = false;
		
	
	}
	
	void OnGUI()
    {
        GUI.Label(new Rect(0, 310, 100, 410), msg);
		GUI.Label(new Rect(690,100,50,50),angleMessage);
		GUI.Label(new Rect(200,300,200,100),tapMessage);
	}
	
	void OnEnabled()
	{
		touchMonitor.tapMonitor += tapDetected;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//msg = count.ToString();
//		Vector2 tapPosition = tapScript.tap.sendValues();
//		
//		if(tapPosition.x > -1)
//		{
//			msg = tapPosition.ToString();
//			
//			Vector3 posVector = cam.WorldToScreenPoint(transform.position);
//			//Debug.Log(posVector);
//	       	Vector2 vectorTwo = GUIUtility.ScreenToGUIPoint( new Vector2( posVector.x, posVector.y ) );             //posVector.x maybe replaced by posVector.z based on main camera's rotation.
//			int angle = Mathf.RoundToInt(  Helpers.angleCalc( tapPosition, posVector ) );
//			int[] resolvedAngles = Helpers.resolveRotation( angle );
//			angleFinal = new Vector3( 0, resolvedAngles[0], resolvedAngles[1] );
//	       
//			tapAngle = angle + 90;
//			
//			angleMessage = angle.ToString();
//			
//			moveHandBone = true;
//			continueRotating = true;
//			addTransforms();
//		}
		
		
		if(moveHandBone)
		{
			
			tapScript.msg2 = "inside move handbone";
			animScript.idleAnimStart = false;
			//animScript.idleStartTime = (int)Time.time;
			print("inside movehandbone!");
			handMoving = true;
			
			
			if(angleX == tapAngle)
			{
				moveUp = false;
                //bulletSpawn();
				
                
			}
            else if (angleX > tapAngle)
            {
                moveUp = false;
            }
            else if (angleX < 0)
            {
                angleX = 0;
                handMoving = false;
                moveHandBone = false;
                continueRotating = false;
                moveUp = true;
            }
			
			if(continueRotating && moveUp)
			{
				angleX += 16f; // Speed control for Up rotation - this varies with framrate of game .. so equalize it using some own function!
			}
			else if(continueRotating && !moveUp)
			{
				angleX -= 16f;// Speed control for Down rotation
			}
			
			if(continueRotating == false)
			{
				animScript.idleAnimStart = true;
				animScript.idleStartTime = (int)Time.time;
				angleX = 0;
				removeTransforms();
				palm_r.transform.eulerAngles = new Vector3(0,180,90); // reset the palm rotation to idle position
				//tapPosition = new Vector2(-1,-1);
			}
			
		}
		else
		{
			angleX = 0;
			tapScript.msg2 = "out";
		}
			
		
	}
	
	void tapDetected(Vector2 tapPosition)
	{
		//tapPosition = tapScript.tap.sendValues();
		msg = tapPosition.ToString();
			
		Vector3 posVector = cam.WorldToScreenPoint(transform.position);
		//Debug.Log(posVector);
       	Vector2 vectorTwo = GUIUtility.ScreenToGUIPoint( new Vector2( posVector.x, posVector.y ) );             //posVector.x maybe replaced by posVector.z based on main camera's rotation.
		int angle = Mathf.RoundToInt(  Helpers.angleCalc( tapPosition, posVector ) );
		int[] resolvedAngles = Helpers.resolveRotation( angle );
		angleFinal = new Vector3( 0, resolvedAngles[0], resolvedAngles[1] );
       
		tapAngle = angle + 90;
		
		angleMessage = angle.ToString();
		
		moveHandBone = true;
		continueRotating = true;
		addTransforms();
	
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
		if(Input.GetMouseButtonDown(0))
		{
			moveHandBone = true;
			continueRotating = true;
			addTransforms();
				
		}
		
		
		
		
		// DROID SHOOT - TAP
		
		
		
		
		if( moveHandBone )
		{
			//print("angleX: "+angleX);
			if(animation.IsPlaying("run"))
			{
				//Debug.Log ("hangle: "+hand_r.localEulerAngles);
				hand_r.localEulerAngles = new Vector3(0,90,biceps_R.transform.eulerAngles.z - angleX);
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
		msg = "came here at: "+Time.time;
		GameObject bulletInstance = (GameObject)Instantiate( portalBullet, palm_r.position, Quaternion.Euler(angleFinal) );
		count++;
      	portalBullet bulletScript = bulletInstance.GetComponent<portalBullet>();
        bulletScript.setColor( colors[colorIndex] );
        colorIndex = (colorIndex + 1) % 2;
	}
	
}
