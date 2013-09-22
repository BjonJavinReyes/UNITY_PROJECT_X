using UnityEngine;
using System.Collections;
using System;
using System.Runtime.CompilerServices;
using System.Reflection;

public class Switch : MonoBehaviour
{
    public GameObject target;          //GameObject controlled by the Trigger
	
	int targetValsLength;
    bool playerNear = false;
    float guiValue = -1;
	object script; 
	Type scriptType;
	bool flag = false;
	
    // Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update ()
    {
		try
		{
			if( target != null && !flag )
			{
				flag = true;
				script = target.GetComponent( target.name );				//Connected object name = script's name attached to connect object.
		    	scriptType = script.GetType();
				targetValsLength =( int ) scriptType.InvokeMember( "getTargetValsLength", BindingFlags.InvokeMethod,
		            null, script, null );
				//Debug.Log( targetValsLength );
			}
		}
		catch(Exception){
			Debug.Log("script name not same as gameobject name");
		}
		
	}
	


    public void incrementState()
    {
         scriptType.InvokeMember( "incrementState", BindingFlags.InvokeMethod,
            null, script, null );
    }

    void changeState( int state )
    {
        scriptType.InvokeMember( "changeState", BindingFlags.InvokeMethod,
            null, script, new object[]{ state } );
    }

    void invertFlags()
    {
        playerNear = !playerNear;
    }
	
    void OnTriggerEnter( Collider collider )
    {
        GameObject collObject = collider.gameObject;
        if ( collObject.name == Constants.PortalBullet && !playerNear )                
        {
       		magneticAbsorption( collObject );
        }
        else if ( collObject.name == Constants.PlayerName)
        {
            invertFlags();
        }
    }
	
	void magneticAbsorption( GameObject collObject )		//Magnetic absorption of portalBullet
	{
		Vector2 portalBulletPosition = new Vector2( collObject.transform.position.x, collObject.transform.position.y );    //CDV(Camera dependent variable)
        Vector2 destinationPoint = new Vector2( transform.position.x, transform.position.y );       //CDV(Camera dependent variable)
		
		//Debug.Log("destination point= " + destinationPoint);
		//Debug.Log("portal bullet position= " + portalBulletPosition);
        
		float turnAngle = Helpers.angleCalc( destinationPoint, portalBulletPosition );

        //Debug.Log( turnAngle );
        Quaternion turnAngleQuat = Quaternion.Euler( new Vector3( 0, 0, turnAngle ) );     //CDV(Camera dependent variable) );
        collObject.transform.rotation = turnAngleQuat;
		
        collObject.rigidbody.velocity = new Vector3( 0, 0, 0 );
        collObject.rigidbody.AddRelativeForce( 50, 0, 0 );                  //CDV(Camera dependent variable
	}

    void OnTriggerExit(Collider collider)
    {
        if ( collider.gameObject.name == Constants.PlayerName )
        {
            invertFlags();
        }
    }

    int getDeltaY()
    {
        return (int)( transform.rotation.eulerAngles.x % 180 );                      //CDV(Camera dependent variable
    }

    int getDeltaX()
    {
        bool temp = transform.rotation.eulerAngles.x > 179;                         //CDV(Camera dependent variable
        return ( Convert.ToInt32( temp ) * 2 ) - 1;
    }

    void OnGUI()
    {
        if( playerNear )
        {   
            Vector3 posVector = Camera.main.WorldToScreenPoint( transform.position );
            Vector2 vectorTwo = GUIUtility.ScreenToGUIPoint( new Vector2( posVector.x, posVector.y ) );          //CDV(Camera dependent variable

            if ( getDeltaY() == 0 )
            {
                guiValue = GUI.HorizontalSlider( new Rect( vectorTwo.x + 40, Screen.height - vectorTwo.y + ( 100 * getDeltaX() ), 80, 120 ), 1.0F, 0.0F, Convert.ToSingle( targetValsLength ) );
                changeState( (int)guiValue );
            }
            else if ( getDeltaY() == 90 )
            {
                guiValue = GUI.VerticalSlider( new Rect( vectorTwo.x + ( 120 * getDeltaX() ), Screen.height - vectorTwo.y - 40, 120, 60 ), 1.0F, 0.0F, Convert.ToSingle( targetValsLength ) );
                changeState( (int)guiValue );
            }
        }
    }
}