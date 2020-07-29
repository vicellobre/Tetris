using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Extends MonoBehaviour to support invoking 
/// one integer argument UnityEvents
/// </summary>
public class StringEventInvoker : MonoBehaviour
{
	protected Dictionary<EventName, UnityEvent<string>> unityEvents =
		new Dictionary<EventName, UnityEvent<string>>();

	/// <summary>
	/// Adds the given listener for the given event name
	/// </summary>
	/// <param name="eventName">event name</param>
	/// <param name="listener">listener</param>
	public void AddListener(EventName eventName, UnityAction<string> listener)
	{
		// only add listeners for supported events
		if (unityEvents.ContainsKey(eventName))
		{
			unityEvents[eventName].AddListener(listener);
		}
	}
}