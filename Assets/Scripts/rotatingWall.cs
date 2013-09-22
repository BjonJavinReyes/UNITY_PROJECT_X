using UnityEngine;
using System.Collections;

public class rotatingWall : MonoBehaviour, SwitchInterface 
{	
	SwitchClass booleanClass;
	public bool[] targetVals = new bool[]{ false, true };
	float rotationsPerMinute = 10.0f;
	int speed = 1;
	
	// Use this for initialization
	void Start()
	{
		booleanClass = new SwitchClass( (long)targetVals.Length );
		//Debug.Log("rotatingWall:"+ booleanClass.getState());
	}
	
	// Update is called once per frame
	void Update()
	{
		if( targetVals[booleanClass.getState()] )
			transform.Rotate( 0f, 0f , 6.0f * rotationsPerMinute * Time.deltaTime * speed );
	}
	
	public void incrementState()
	{
		booleanClass.incrementState();
	}
	
	public void changeState( int updatedState )
	{
		booleanClass.changeState( updatedState );
	}
	
	public int getTargetValsLength()
	{
		return targetVals.Length;
	}
}