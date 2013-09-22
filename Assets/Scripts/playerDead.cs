using UnityEngine;
using System.Collections;

public class playerDead : MonoBehaviour {			//This script is linked to the player, who is to be animated while becoming dead.
	
	bool isPlayerDead = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( isPlayerDead == true )
		{
			//Show GUI for death.
			//play necessary animation.
			//Do stuff like reload level,restore to last checkpoint etc.
		}
	
	}
	
	public void setDead()
	{
		isPlayerDead = true;
	}
}