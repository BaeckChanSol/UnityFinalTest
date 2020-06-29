using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public EventDataBase eventDatabase =null;
    
    private static EventHandler _instance;

    public static EventHandler Instance => _instance;
   

    public delegate void EventDelegate();

    public Dictionary<string, List<EventDelegate>> events;

    private void Awake()
    {
        _instance = this;
        events = new Dictionary<string, List<EventDelegate>>();
        
        if(eventDatabase == null)
            Debug.LogError(message: "eventDatabase가 등록되지 않았습니다.");
    }

    public void Subscribe(string name, EventDelegate handler)
    {
        if (!eventDatabase.Events.Exists(e => e.eventID == name))
        {
            Debug.LogError(message: $"{name}이라는 이벤트는 존재하지 않습니다.");
            return;
        }
        if(events.ContainsKey(name))
        {
            var dels = events[name];
            if (dels == null) events[name] = new List<EventDelegate>();
            dels.Add(handler);
        }
        else
        {
            events.Add(name, new List<EventDelegate>());
            events[name].Add(handler);
        }
    }

    public void Emit(string name)
    {
        if (!eventDatabase.Events.Exists(e => e.eventID == name))
        {
            Debug.LogError(message: $"{name}이라는 이벤트는 존재하지 않습니다.");
            return;
        }

        foreach (var e in events[name])
            e.Invoke();
    }
}

