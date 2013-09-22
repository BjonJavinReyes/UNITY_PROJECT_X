using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterAnim : MonoBehaviour {
	public GameObject sphere;
	
	
	
	bool startRun = true; //default is false 
	public int idleStartTime;
	public bool idleAnimStart = false;
	public bool waitingForIdleAnim = true;
	bool animSequenceComplete = true;
	int[] randomArray;// random anim sequence
	int nsize = 4;
	int refNo;
	public bool isJumping = false;
	//bool charInAir = false;
	ArrayList intArray; 
	public bool swipeEnd = false; // used to check when swipe ends.. When user swipes for first time, its true
	
	swipeMonitor swipeScript;
	//movement moveScript;
	//follow followScript;
	
	
	
	void Start () {
		
		idleStartTime = (int)Time.time;
		idleAnimStart = true;
		randomArray = new int[nsize];
		refNo = 0;
		intArray = new ArrayList();
		swipeScript = sphere.GetComponent("swipeMonitor") as swipeMonitor;
		//moveScript = sphere.GetComponent("movement") as movement;
		//followScript = gameObject.GetComponent("follow") as follow;
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		//animation.Play("Default Take");
		
		//updateAnimStatus();
		//charInAir = moveScript.charInAir;

		// generate a random numbered Array for the random animation sequence
		if(animSequenceComplete)
		{
			
			intArray.Clear();
			for(int i=0;i<nsize;i++)
			{
				intArray.Add(i);
			}
			
			for(int i=0;i<nsize;i++)
			{
				int r = shuffleTheArray();
				randomArray[i] = r;
			}
			
			/*
			for(int i=0;i<nsize;i++)
			{
				print("randArray["+i+"]: "+randomArray[i]);
			}
			*/
			animSequenceComplete = false;
		}
		
		//Idle animation
		if(idleAnimStart == true && (Time.time - idleStartTime ) > 3 )
		{
			if(refNo < nsize)
			{
				animation.CrossFade("idle"+randomArray[refNo], 5f);
				idleAnimStart = false;
				waitingForIdleAnim = false;
				refNo += 1;
			}
			else
			{
				animSequenceComplete = true;
				refNo = 0;
			}
			
		}
		
		
		// **************************DROID CONTROLS
		
		/*if(swipeScript.swipe.swipeStart)
		{
			animation.Play ("startWalk");
		}
		*/
		if(swipeScript.swipe.swipeActive )
		{
			
			//swipeScript.msg3 = "swipeActive!";
			
			idleAnimStart = false;
			updateStartWalkStatus();
			if(startRun && !(gameObject.animation.IsPlaying("jumpWhileRun") || gameObject.animation.IsPlaying("jump")))
			{
				animation.CrossFade("run");
				
			}
			updateRunAnimSpeed();
			
		}
		else
		{
			//swipeScript.msg3 = "NoSwipe!";
		}
		
		if(swipeScript.swipe.swipeEnd)
		{
			
			if(Mathf.Abs(sphere.rigidbody.velocity.x) > 3.5) // CHANGE THIS UPON AXIS CHANGE
			{
				animation.Play("stopWalk");
				startRun = false;
				updateStopAnimSpeed();
			}
			/*
			else
			{
				animation.CrossFade("stopFast");
				updateFastStopAnimSpeed();
				startRun = false;
			}
			*/
			
			idleAnimStart = true;
			
		}
		
		//******************************* PC CONTROLS
		
		
		if(Input.GetKeyDown("a") || Input.GetKeyDown("d") )
		{
			idleAnimStart = false;
			animation.CrossFade("startWalk",4f);
			updateStartAnimSpeed();
			
			
			
		}
		
		
		if(Input.GetKey("a") || Input.GetKey("d"))
		{
			idleAnimStart = false;
			updateStartWalkStatus();
			if(startRun && !(gameObject.animation.IsPlaying("jumpWhileRun") || gameObject.animation.IsPlaying("jump")))
			{
				animation.CrossFade("run");
				
			}
			updateRunAnimSpeed();
		}
		
		if(Input.GetKeyUp("a") || Input.GetKeyUp("d"))
		{
			
			if(Mathf.Abs(sphere.rigidbody.velocity.x)  > 3.5)
			{
				animation.Play("stopWalk");
				startRun = false;
				updateStopAnimSpeed();
			}
			else
			{
				animation.CrossFade("stopFast");
				updateFastStopAnimSpeed();
				startRun = false;
			}
			
			idleStartTime = (int)Time.time;
			idleAnimStart = true;
			
		}
		
		if(Mathf.Abs(sphere.rigidbody.velocity.x) <0.2)
		{
			updateIdleAnimSatus();
		}
		
	}
	
	
	
	public void playStartWalkAnim()
	{
		animation.CrossFade("startWalk",4f);
		updateStartAnimSpeed();
	}
	
	
	int shuffleTheArray()
	{
		int t;
		int c = intArray.Count;
		int rand = Random.Range(0,c);
		//print ("rand value: "+rand);
		t = (int)intArray[rand];
		intArray.RemoveAt(rand);
		return t;
	}
	
	void updateAnimStatus()
	{
		if(animation.isPlaying)
		{
			swipeScript.msg3 = "animation is playing!!!";
		}
		else
		{
			swipeScript.msg3 = "animation aint playing!";
		}
	}
	
	void updateStartWalkStatus()
	{
		if(animation.IsPlaying("startWalk"))
		{
			//swipeScript.msg3 = "playing startwalk!";
			startRun = false;			
		}
		else
		{
			//swipeScript.msg3 = "startwalk aint playin!";
			startRun = true;
		}
	}
	
	void updateIdleAnimSatus()
	{
		if(waitingForIdleAnim)
		{
			//just wait till he starts an idle anim
		}
		else
		{
			
			
			if(animation.isPlaying)
			{			
				idleAnimStart = false;
				waitingForIdleAnim = false;
				idleStartTime = (int)Time.time;
				
			}
			else
			{
				idleAnimStart = true;
				waitingForIdleAnim = true;
				idleStartTime = (int)Time.time;
			}

		}
	
	}
	
	//Update anim speed along with velocity to give realistic slow and fast walk
	void updateRunAnimSpeed()	
	{
		
		animation["run"].speed = (float)0.2 * Mathf.Abs(sphere.rigidbody.velocity.x);
		/*
		foreach(AnimationState animState in animation)
		{
			animState.speed = (float)0.2*sphere.rigidbody.velocity.z;
			//0.4*sphere.rigidbody.velocity.z plus force of 22 in z direction for the sphere seems realistic
		}
		*/
	}
	
	
	void updateStopAnimSpeed()
	{
		foreach(AnimationState animState in animation)
		{
			animState.speed = 1f;
		}
	}
	
	void updateFastStopAnimSpeed()
	{
		animation["stopFast"].speed = 0.8f;
	}
	
	void updateStartAnimSpeed()
	{
		animation["startWalk"].speed = 4f;
	}
	
	public void updateJumpAnimSpeed()
	{
		animation["jumpWhileRun"].speed = 2f;
	}
}

