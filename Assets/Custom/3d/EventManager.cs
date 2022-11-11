using System.Collections.Generic;
using UnityEngine.Events;

public class EventManager
{
    private readonly Dictionary<string, UnityEvent> evs = new Dictionary<string, UnityEvent>();

    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (null != _instance)
            {
                return _instance;
            }

            _instance = new EventManager();

            return _instance;
        }
    }

    public void AddEventListener(string eventName, UnityAction action)
    {
        if (evs.TryGetValue(eventName, out var a))
        {
            a.AddListener(action);
        }
        else
        {
            var evt = new UnityEvent();
            evt.AddListener(action);
            evs.Add(eventName, evt);
        }
    }

    public void DispatchEvent(string eventName)
    {
        if (evs.TryGetValue(eventName, out var a))
        {
            a.Invoke();
        }
    }

    public void RemoveEventListeners(string eventName)
    {
        evs.Remove(eventName);
    }

    public void RemoveEventListeners(string eventName, UnityAction action)
    {
        if (!evs.TryGetValue(eventName, out var unityEvent)) return;
        unityEvent.RemoveListener(action);
        if (unityEvent.GetPersistentEventCount() == 0)
        {
            evs.Remove(eventName);
        }
    }

    public void RemoveAllEventListeners()
    {
        evs.Clear();
    }
}