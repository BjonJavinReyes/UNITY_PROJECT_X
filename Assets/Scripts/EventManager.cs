using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour 
{
	public delegate void someHandler();
	public static event someHandler handlerMethod;
	
	
	public delegate void angleEventHandler();
	public static event angleEventHandler angleEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(200,100,200,40),"event"))
		{
			handlerMethod();
		}
	}
}
