using UnityEngine;
using System.Collections;

public class StoNWall : MonoBehaviour, SwitchInterface 
{
	public GameObject normalWall;			//prefab of normalWall to be dragged here.
	SwitchClass booleanClass;
	bool[] targetVals = new bool[]{ false, true };
	// Use this for initialization
	void Start () 
	{
		booleanClass = new SwitchClass( (long) targetVals.Length);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void instantiateNormalAndDestroy()
	{
		Instantiate( normalWall, transform.position, transform.rotation );
		Destroy( gameObject );
	}
	
	public void incrementState()
	{
		if( booleanClass.getState() == 0 )
		{
			booleanClass.incrementState();
			instantiateNormalAndDestroy();
		}
	}
	
	public void changeState( int State )
	{
		if( booleanClass.getState() == 0 )
		{
			booleanClass.changeState( State );
			instantiateNormalAndDestroy();
		}
	}
	
	public int getTargetValsLength()
	{
		return targetVals.Length;
	}
	
}