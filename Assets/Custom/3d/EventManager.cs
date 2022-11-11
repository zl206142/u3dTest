using System.Collections.Generic;

public delegate void MyAction(params object[] args);

public class EventManager
{
    private readonly Dictionary<string, MyAction> _evs = new();

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

    public void AddEventListener(string eventName, MyAction action)
    {
        if (_evs.TryGetValue(eventName, out var a))
        {
            a += action;
            _evs.Add(eventName, a);
        }
        else
        {
            _evs.Add(eventName, action);
        }
    }

    public void DispatchEvent(string eventName, params object[] args)
    {
        if (_evs.TryGetValue(eventName, out var a))
        {
            a.Invoke(args);
        }
    }

    public void RemoveEventListeners(string eventName)
    {
        _evs.Remove(eventName);
    }

    public void RemoveEventListeners(string eventName, MyAction action)
    {
        if (!_evs.TryGetValue(eventName, out var unityEvent)) return;
        unityEvent -= action;
        if (unityEvent.GetInvocationList().Length == 0)
        {
            _evs.Remove(eventName);
        }
    }

    public void RemoveAllEventListeners()
    {
        _evs.Clear();
    }
}