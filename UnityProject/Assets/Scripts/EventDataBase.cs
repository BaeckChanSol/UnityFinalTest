using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="event_Database",menuName = "BCS/이벤트 데이터베이스 생성")]
public class EventDataBase : ScriptableObject
{
    public List<EventForm> Events;
}
