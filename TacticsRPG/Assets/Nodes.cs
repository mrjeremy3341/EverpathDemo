using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Nodes : MonoBehaviour
{
    NodeManager nodeManager;
    public bool nodeSelected;
    public bool nodeCompleted;
    public List<Nodes> connectedNodes = new List<Nodes>();
    //public NodeEvents nodeEvent;
    public EventSO nodeEvent;
    
    Button button;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => NodeClicked());
        nodeManager = GetComponentInParent<NodeManager>();
        image = GetComponent<Image>();
        if (nodeEvent != null)
        {
            image.sprite = nodeEvent.nodeIcon;
        }        
    }

    void NodeClicked()
    {
        Debug.Log("clicked");
        if (!nodeManager.navigatable)
        {
            Debug.Log("Not ready");
            return;
        }
        if (nodeManager.currentNode == this)
        {
            Debug.Log("Already on node");
            return;
        }
        if (!nodeManager.currentNode.connectedNodes.Contains(this))
        {
            Debug.Log("Node not connected");
            return;
        }
        else
        {
            StartCoroutine(nodeManager.SwitchNode(this));            
            Debug.Log("Node Switched");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nodeSelected)
        {
            image.color = Color.green;
        }

        if (!nodeSelected && !nodeCompleted)
        {
            image.color = Color.red;
        }

        if (nodeCompleted && !nodeSelected)
        {
            image.color = Color.cyan;
        }
    }
}
