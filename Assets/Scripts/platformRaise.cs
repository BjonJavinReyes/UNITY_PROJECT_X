using UnityEngine;
using System.Collections;

public class platformRaise : MonoBehaviour, SwitchInterface
{
	public SwitchClass booleanClass;
	public Vector3 target;
	public int speed = 1;
	public Vector3[] targetVals;
	
	// Use this for initialization
	void Start () 
	{
		targetVals = new Vector3[]{ transform.position, new Vector3( transform.position.x , transform.position.y + 20, transform.position.z ) };
		booleanClass = new SwitchClass( (long)targetVals.Length );
		target = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.MoveTowards( transform.position, target, Time.deltaTime * speed );
	}
	
	public void incrementState()
	{
		booleanClass.incrementState();
		target = targetVals[booleanClass.getState()];
		Debug.Log( booleanClass.getState() );
		Debug.Log( target );
	}
	
	public void changeState( int updatedState )
	{
		booleanClass.changeState( updatedState );
		target = targetVals[booleanClass.getState()];
	}
	public int getTargetValsLength()
	{
		return targetVals.Length;
	}
}