using UnityEngine;
using System.Collections;
using System;

public class tiltWall : MonoBehaviour, SwitchInterface
{
	SwitchClass switchClass;
	public int state = 0;
	public int[] targetVals = new int[]{ 0, 90, 180, 270 };
	
	// Use this for initialization
	void Start ()
    {
		switchClass = new SwitchClass( (long)targetVals.Length );
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
	
    public void incrementState()
    {
		//Debug.Log("In tiltWall: incremented");
		//playAnimation();
		switchClass.incrementState();
		int[] resolvedValues = Helpers.resolveRotation2( targetVals[switchClass.getState()] );
		transform.rotation = Quaternion.Euler( new Vector3( 0, resolvedValues[0], resolvedValues[1] ) );
    }

    public void changeState( int updatedState )
    {
		//playAnimation();
		switchClass.changeState( updatedState );
		int[] resolvedValues = Helpers.resolveRotation2( targetVals[switchClass.getState()] );
		transform.rotation = Quaternion.Euler( new Vector3( 0, resolvedValues[0], resolvedValues[1] ) );
    }
	
	void playAnimation()
	{
		animation.Play("");			//Mention Animation name.
	}
	
	public int getTargetValsLength()
	{
		return targetVals.Length;
	}
}