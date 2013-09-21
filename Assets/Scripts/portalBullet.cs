using UnityEngine;
using System.Collections;

public class portalBullet : MonoBehaviour {

    public Color color;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    void initProcess()
    {
        particleSystem.startColor = color;
        rigidbody.AddRelativeForce( 500, 0, 0 );       //CDV(Camera dependent variable
        //Destroy( gameObject, 4 ); //<<<<<<<<<<<<<<<<<<<<<< What does this mean? >>>>>>>>>>>>>>>>>>>>>>>>>
    }

    public void setColor( Color color )
    {
        this.color = color;
        initProcess();
		Debug.Log(" here ");
    }

    void OnTriggerEnter( Collider collider )
    {
		//Debug.Log ("bullet Rotation="+transform.rotation);
        if( collider.gameObject.name != Constants.BouncyWall && collider.gameObject.name != Constants.Switch )
		{
			//Debug.Log("Trigger:destroying");
			Destroy( gameObject );
		}
    }
	
	void OnCollisionEnter( Collision collision )
	{
		//Debug.Log("Collider:destroying");
		Destroy( gameObject );
	}
}