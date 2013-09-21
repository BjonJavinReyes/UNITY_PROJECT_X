using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	
	swipeMonitor swipeScript;
	CharacterAnim animScript;
	public static Vector3 constVel; // PUT IN CONSTANTS SCRIPT
	public bool charInAir = false;
	public GameObject character;
	public float startYpos;
	
	bool swipeOff = false;
	
	public Vector3 rotAngleLeft;
	public Vector3 rotAngleRight;
	public int delta = 1;
	
	public bool swipe = false;
	
	// Use this for initialization
	void Start () 
	{
		rotAngleLeft = new Vector3(0,180,0);
		rotAngleRight = new Vector3(0,0,0);
		swipeScript = gameObject.GetComponent("swipeMonitor") as swipeMonitor;
		animScript = character.GetComponent("CharacterAnim") as CharacterAnim;
		
		startYpos = gameObject.transform.position.y;
		constVel = new Vector3(8f * delta,0,0); // CHANGE THIS UPON AXIS CHANGE
		
		swipe = swipeScript.swipe.swipeActive;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		constVel = new Vector3(8f * delta,0,0);
		if(!swipeOff)
		{
			swipe = swipeScript.swipe.swipeActive;
		}
		//Quaternion rot = new Quaternion();
		//rot.eulerAngles = new Vector3(0,0,0);
		//transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		gameObject.rigidbody.AddForce(0,-32,0,ForceMode.Force);

		//**************** DROID *********
		if(swipe && Mathf.Abs(swipeScript.swipe.swipeAngle) <=90 )
		{
			delta = 1;
			
			transform.eulerAngles = rotAngleRight;
			
			if(charInAir == false )
			{
				rigidbody.velocity = constVel;
			}
			else if(charInAir == true)
			{
				if(gameObject.transform.position.y <= startYpos)
				{
					charInAir = false;
					animScript.isJumping = false;
					animScript.waitingForIdleAnim = true;
				}
			}
		}
		else if(swipe && Mathf.Abs(swipeScript.swipe.swipeAngle) >90)
		{
			delta = -1;
			
			transform.eulerAngles = rotAngleLeft;
			
			if(charInAir == false )
			{
				rigidbody.velocity = constVel;
			}
			else if(charInAir == true)
			{
				if(gameObject.transform.position.y <= startYpos)
				{
					charInAir = false;
					animScript.isJumping = false;
					animScript.waitingForIdleAnim = true;
				}
			}
		}
		
		

		//************************ PC **********
		
		
	
		if(Input.GetKey("a"))
		{
			delta = -1;
			
			transform.eulerAngles = rotAngleLeft;
			
			if(charInAir == false )//&& rigidbody.velocity.z <= 8)
			{
				rigidbody.velocity = constVel;
				//rigidbody.AddForce(0,0,27,ForceMode.Acceleration);
			}
			else if(charInAir == true)
			{
				if(gameObject.transform.position.y <= startYpos)
				{
					charInAir = false;
					animScript.isJumping = false;
					animScript.waitingForIdleAnim = true;
				}
			}
		}
		if(Input.GetKey("d"))
		{
			delta = 1;
			
			transform.eulerAngles = rotAngleRight;
			
			if(charInAir == false )//&& rigidbody.velocity.z <= 8)
			{
				rigidbody.velocity = constVel;
				//rigidbody.AddForce(0,0,27,ForceMode.Acceleration);
			}
			else if(charInAir == true)
			{
				if(gameObject.transform.position.y <= startYpos)
				{
					charInAir = false;
					animScript.isJumping = false;
					animScript.waitingForIdleAnim = true;
				}
			}
		}
		 
		 
	}
	
	
	
	void FixedUpdate()
	{
		// ************************* PC ***************
		
		
		if(Input.GetKey("a") && Input.GetKeyDown(KeyCode.Space) && charInAir == false )
		{
			charInAir = true;
			gameObject.rigidbody.AddForce(25 * delta,42,0,ForceMode.Impulse); // CHANGE THIS UPON AXIS CHANGE

			animScript.isJumping = true;
			
			if(Mathf.Abs(rigidbody.velocity.x) > 2.5) // CHANGE THIS UPON AXIS CHANGE
			{
				//print ("ypos: "+transform.position.y+" startY+: "+startYpos + 0.5);
				character.animation.Play("jumpWhileRun");
				animScript.updateJumpAnimSpeed();
				animScript.waitingForIdleAnim = false;

			
			}
			else
			{
				character.animation.Play("jump");
				animScript.waitingForIdleAnim = false;
			}
			
		}
		
		
		
		
		// *********************** DROID ***************
		if(swipeScript.swipe.swipeAngle > 40 && swipeScript.swipe.swipeAngle <= 120 && charInAir == false)
		{
			charInAir = true;
			gameObject.rigidbody.AddForce(25 * delta,42,0,ForceMode.Impulse); // CHANGE THIS UPON AXIS CHANGE

			animScript.isJumping = true;
			
			if(Mathf.Abs(rigidbody.velocity.x) > 2.5) // CHANGE THIS UPON AXIS CHANGE
			{
				//print ("ypos: "+transform.position.y+" startY+: "+startYpos + 0.5);
				character.animation.CrossFade("jumpWhileRun");
				animScript.updateJumpAnimSpeed();
				animScript.waitingForIdleAnim = false;

			
			}
			else
			{
				character.animation.Play("jump");
				animScript.waitingForIdleAnim = false;
			}
			
		}
		
	}
	
	public void changeDelta(int tapAngle)
	{
		if(tapAngle <= 90)
		{
			delta = 1;
			
		}
		else
		{
			delta = -1;
		}
	}
	
	public void disableSwipe()
	{
		swipe = false;
		swipeOff = true;
	}
	
	public void enableSwipe()
	{
		swipe = true;
		swipeOff = false;
	}
	
}
