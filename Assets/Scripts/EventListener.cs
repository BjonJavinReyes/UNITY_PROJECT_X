using UnityEngine;
using System.Collections;

public class EventListener
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
	
}
