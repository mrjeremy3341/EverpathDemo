using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventDialogue : MonoBehaviour
{
    NodeManager nodeManager;
    public TextMeshProUGUI title;
    public TextMeshProUGUI body;
        
    void Start()
    {
        nodeManager = GetComponentInParent<NodeManager>();
    }

    public void InitializeDialogue(EventSO nodeEvent)
    {
        title.text = nodeEvent.title;
        body.text = nodeEvent.body;
    }

    public void ProceedToEvent()
    {
        nodeManager.BeginEvent();
    }

}
