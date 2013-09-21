using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
	
	public GameObject character;
	
	// Use this for initialization
	void Start () 
	{
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.position = new Vector3(character.transform.position.x, transform.position.y, transform.position.z);
	
	}
}
