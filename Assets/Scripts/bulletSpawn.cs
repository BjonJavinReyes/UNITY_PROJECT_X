using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class bulletSpawn : MonoBehaviour 
{
   // public GameObject portalBullet;	//prefab(bullet) to be linked
    Color[] colors = { Color.yellow, Color.red};
    int colorIndex = 0;
    touchMonitor tapScript;
	portalBullet bulletScript;
	//cameraScript camScript;
 
	public GameObject character;
    public GameObject portalBullet;

    public Transform palm_R;
	String msg = "blah";
	public String tapMessage = "null";
	String angleMessage;
	public Camera cam;
	

    void Start()
    {
        tapScript =  gameObject.GetComponentInChildren<touchMonitor>();
		bulletScript = portalBullet.GetComponent("portalBullet") as portalBullet;
		//msg = tapScript.ToString();
		//bicepsBone = character.transform.Find("bicepsSpine");
    }
	
	void OnGUI()
    {
        GUI.Label(new Rect(0, 310, 100, 410), msg);
		GUI.Label(new Rect(690,100,50,50),angleMessage);
		GUI.Label(new Rect(200,300,200,100),tapMessage);
	}
	
    void Update()
    {
        
		//print ("blah");
        Vector2 tapPosition = tapScript.tap.sendValues();
		msg = tapPosition.ToString();
        //if (tapPosition.x > -1)
            //bulletShoot( tapPosition );
 
    }

    float angleCalc(Vector2 a, Vector2 b)		//function to calculate angle
    {
        float diffx = a.x - b.x;
        float diffy = a.y - b.y;
        //Debug.Log(diffx.ToString()+","+diffy.ToString()+"difference="+a.x.ToString()+","+a.y.ToString()+"-"+b.x.ToString()+","+b.y.ToString());
        return (Mathf.Atan2(diffy, diffx) * Mathf.Rad2Deg);
    }

    public void bulletShoot(Vector2 tap)
    {
        Vector3 posVector = cam.WorldToScreenPoint(transform.position);//Camera.mainCamera.WorldToScreenPoint(transform.position);
		Debug.Log(posVector);
       	
		Vector2 vectorTwo = GUIUtility.ScreenToGUIPoint(new Vector2(posVector.x, posVector.y));             //posVector.x maybe replaced by posVector.z based on main camera's rotation.
		
		//tapMessage = "tap: "+tap+", vectorTwo:"+vectorTwo;
		
        float angle = angleCalc( vectorTwo, tap );
		angleMessage = angle.ToString();
        Quaternion bulletRot = new Quaternion();
        bulletRot.eulerAngles = new Vector3( transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle );
		
		
        GameObject bulletInstance = (GameObject)Instantiate( portalBullet, palm_R.position, bulletRot );
        //bullet bulletScript = bulletInstance.GetComponent<bullet>();        //hypothetical bullet script
        bulletScript.setColor( colors[colorIndex] );
        colorIndex = (colorIndex + 1) % 2;
       
        //PLAY SHOOT ANIMATION HERE!!!!

    }


}