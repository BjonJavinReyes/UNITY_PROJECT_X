using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	public GameObject sphere;
	//CharacterAnim animScript;
	public float charSphereOffset = 0;
	// Use this for initialization
	void Start () {
		//animScript = gameObject.GetComponent("CharacterAnim") as CharacterAnim;
		charSphereOffset = Mathf.Abs(transform.position.y - sphere.transform.position.y);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 pos1 = new Vector3(sphere.transform.position.x,sphere.transform.position.y+charSphereOffset, transform.position.z); // CHANGE THIS UPON AXIS CHANGE
		gameObject.transform.position = pos1;
		gameObject.transform.rotation = sphere.transform.rotation;
		
	}
}
