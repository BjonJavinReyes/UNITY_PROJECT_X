using UnityEngine;
using System.Collections;

public class bouncyWall : MonoBehaviour 
{
	// Use this for initialization
	void Start ()
	{
		if( Mathf.RoundToInt( transform.rotation.eulerAngles.z ) % 90 != 0 )
			Debug.Log("bouncyWall: Error with orientation: should be multiples of 90 ");
	}
	
	// Update is called once per frame
	void Update (){
	}
	
	Vector3 rotateBulletVertical( Vector3 bulletRotation )
	{
		return new Vector3( bulletRotation.x, ( Mathf.CeilToInt( bulletRotation.y ) + 180) % 360, bulletRotation.z ); 
	}
	
	Vector3 rotateBulletHorizontal( Vector3 bulletRotation )
	{
		return new Vector3( bulletRotation.x, bulletRotation.y, 360 - Mathf.CeilToInt( bulletRotation.z ) );
	}
	
	void OnTriggerEnter( Collider collider )
	{
		//Debug.Log("inside trigger");
		GameObject collidedObject = collider.gameObject;
		Vector3 bulletRotation = collidedObject.transform.rotation.eulerAngles;
		//Debug.Log( bulletRotation );
		//Debug.Log( Mathf.RoundToInt( Mathf.Abs( transform.rotation.eulerAngles.z ) ) / 90 );
		int intAngle = Mathf.RoundToInt( Mathf.Abs( transform.rotation.eulerAngles.z ) ) ;
		if( ( intAngle / 90 ) % 2 == 1 )		//to check if angle is 270 or 90
		{
			collidedObject.transform.rotation = Quaternion.Euler( rotateBulletVertical( bulletRotation ) );
			//Debug.Log("1st");
		}
		else
		{
			collidedObject.transform.rotation = Quaternion.Euler( rotateBulletHorizontal( bulletRotation ) );
			//Debug.Log("2nd");
		}
		collidedObject.rigidbody.velocity = new Vector3( 0, 0, 0 );
        collidedObject.rigidbody.AddRelativeForce( 300, 0, 0 );
	}
}
