using UnityEngine;
using System.Collections;

public class portalProperty : MonoBehaviour {
	public GameObject portalPrefab;			//portal Prefab to be dragged and dropped
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collision)				//CDV
	{
		GameObject collObject = collision.gameObject;
		if(collObject.name == "portalBullet")
		{
			int zValue,yValue;
			int optValueY = Mathf.CeilToInt( transform.rotation.eulerAngles.y );
			int optValueZ = Mathf.CeilToInt(transform.rotation.eulerAngles.z);
			int collOptValueY = Mathf.CeilToInt(collObject.transform.rotation.eulerAngles.y);
			if( ( collOptValueY - optValueY ) < 2) //Opposite Direction to instantiate,same y
			{
				//Debug.Log("opposite instantiate");
				yValue = optValueY + 180;
				zValue = 360 - optValueZ;
			}
			else 			//Same direction to instantiate, opposite y
			{
				//Debug.Log("same instantiate");
				yValue = optValueY;
				zValue = optValueZ;
			}
			//Debug.Log("yValue="+yValue);
			//Debug.Log("zValue="+zValue);
			Quaternion instantiateAngle = Quaternion.Euler(new Vector3(0, yValue, zValue));			//prevents gimbal lock
			GameObject portalInstance = (GameObject)Instantiate( portalPrefab, transform.position, instantiateAngle);
		}
	}
}
