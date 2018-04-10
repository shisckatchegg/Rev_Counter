using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Events
{
	/// <summary>
	/// Expanding this setup to support non-generics has a problem:
	/// that it's possible to attach diferent callbacks to the same event, and when triggering the event,
	/// a casting error will happen.
	/// Expading the event class to abstract itself from generic and non-generic might work?
	/// </summary>
	public class EventManager : MonoBehaviour
	{
		private Dictionary<EventNames, UnityEventBase> EventDictionary;

		private static EventManager _eventManager;

		public static EventManager Instance
		{
			get
			{
				if (!_eventManager)
				{
					_eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

					if (!_eventManager)
					{
						Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
					}
					else
					{
						_eventManager.Init();
					}
				}

				return _eventManager;
			}
		}

		void Init()
		{
			if (EventDictionary == null)
			{
				EventDictionary = new Dictionary<EventNames, UnityEventBase>();
			}
		}

		//TODO: for safety reasons (incorrect casting) the events have to be called ALWAYS with data (even if it´s a nulled struct)
		public static void StartListening<TData>(EventNames eventName, UnityAction<TData> listener) where TData : struct
		{
			UnityEventBase thisEvent = null;
			if (Instance.EventDictionary.TryGetValue(eventName, out thisEvent))
			{
				((GenericEvent<TData>)thisEvent).AddListener(listener);
				Debug.Log("Event " + eventName + " already registered, adding another listener: " + listener.ToString());
			}
			else
			{
				thisEvent = new GenericEvent<TData>();
				((GenericEvent<TData>)thisEvent).AddListener(listener);
				Instance.EventDictionary.Add(eventName, thisEvent);
				Debug.Log("New Event " + eventName + " adding new listener: " + listener.ToString());
			}

		}

		public static void StopListening<TData>(EventNames eventName, UnityAction<TData> listener) where TData : struct
		{
			if (_eventManager == null) return;

			UnityEventBase thisEvent = null;
			if (Instance.EventDictionary.TryGetValue(eventName, out thisEvent))
			{
				((GenericEvent<TData>)thisEvent).RemoveListener(listener);
			}
		}

		public static void TriggerEvent<TData>(EventNames eventName, TData data) where TData : struct
		{
			UnityEventBase thisEvent = null;
			if (Instance.EventDictionary.TryGetValue(eventName, out thisEvent))
			{
				((GenericEvent<TData>)thisEvent).Invoke(data);
			}
		}
	}

	public class GenericEvent<TData> : UnityEvent<TData>
	{
	}

}