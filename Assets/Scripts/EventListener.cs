using UnityEngine;
using System.Collections;

public class EventListener : MonoBehaviour 
{
	void OnEnable()
	{
		EventManager.handlerMethod += handlerMethod;
	}
	
	void OnDisable()
	{
		EventManager.handlerMethod -= handlerMethod;
	}
	
	void handlerMethod()
	{
		Debug.Log("Event handled!");
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
