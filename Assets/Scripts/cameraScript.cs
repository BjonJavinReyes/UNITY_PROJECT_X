using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	//string msg = "Tap angle";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(690,50,100,50), "Exit"))
		{
			Application.Quit();
		}
		
		
	}
}
