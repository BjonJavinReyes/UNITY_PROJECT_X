using UnityEngine;
using System.Collections;

public class spikyWall : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider collider )
	{
		playerDead deathScript = collider.gameObject.GetComponent< playerDead >();
		deathScript.setDead();
	}
}