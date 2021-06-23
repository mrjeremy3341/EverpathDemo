using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Overworld Events/Event", fileName = "New Event")]
public class EventSO : SerializedScriptableObject
{
    public string title;
    [TextArea(6, 20)]
    public string body;
    public Sprite nodeIcon;

    public NodeEvents eventType;    
}
