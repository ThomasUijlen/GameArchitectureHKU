using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class EventTriggerDecorator
{
    public static GameObject AddTrigger(GameObject _gameObject, EventTriggerType _triggerType, UnityAction<BaseEventData> call)
    {
        EventTrigger crafterTriggers = _gameObject.GetComponent<EventTrigger>();
        if (crafterTriggers == null)
        {
            crafterTriggers = _gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry newEntry = new EventTrigger.Entry();
        newEntry.eventID = _triggerType;
        newEntry.callback.AddListener(call);
        crafterTriggers.triggers.Add(newEntry);

        return _gameObject;
    }
}
