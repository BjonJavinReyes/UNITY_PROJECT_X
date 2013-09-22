using UnityEngine;
using System.Collections;

public class mainSwitch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collision)
    {
		//Debug.Log("collided");
        GameObject collObject = collision.gameObject;
        if ( collObject.name == Constants.PORTALBULLET )
            transform.parent.GetComponent<Switch>().incrementState();
    }
}
