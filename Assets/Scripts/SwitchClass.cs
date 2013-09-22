using UnityEngine;
using System.Collections;

public class SwitchClass : SwitchInterface 
{
	private int state;
	private long targetValsLength;
	
	public SwitchClass( long targetValsLength )
	{
		this.state = 0;
		this.targetValsLength = targetValsLength;
	}
	
	public SwitchClass( int state, int targetValsLength )
	{
		this.state = state;
		this.targetValsLength = (long)targetValsLength;
	}
	
	public int getState()
	{
		return state;
	}
	
	public void incrementState()
	{
		try
		{
			state = (state + 1) % (int)targetValsLength;
		}
		catch(System.DivideByZeroException)
		{
			Debug.Log("SwitchClass: targetValsLength = 0");
		}
		catch(System.IndexOutOfRangeException){
		}
	}
	
	public void changeState( int updatedState )
	{
		state = updatedState;
	}
	
	public int getTargetValsLength(){
		return -1;
	}
}