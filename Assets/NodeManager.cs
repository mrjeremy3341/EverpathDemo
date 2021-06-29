using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public enum NodeEvents
{
    Encounter,
    DialogueOnly,
    BossEncounter,
    Shop
}
public class NodeManager : MonoBehaviour
{    
    public List<Nodes> nodes = new List<Nodes>();    
    public Nodes startNode;
    public bool navigatable;

    public GameObject dialogueUI;
    public GameObject shopUI;

    // To Save
    [ReadOnly]
    public List<Nodes> completedNodes = new List<Nodes>();
    [ReadOnly]
    public Nodes currentNode;

    private void Start()
    {
        Initialize();        
        CloseDialogue();
        CloseShop();
    }

    public void Initialize()
    {
        StartCoroutine(SwitchNode(startNode));
    }

    public void LoadState()
    {
        foreach (var node in completedNodes)
        {
            currentNode.nodeSelected = false;
            currentNode.nodeCompleted = true;
        }
    }

    public IEnumerator SwitchNode(Nodes node)
    {
        navigatable = false;

        if (currentNode != null)
        {
            currentNode.nodeSelected = false;
            currentNode.nodeCompleted = true;

            if (!completedNodes.Contains(currentNode))
            {
                completedNodes.Add(currentNode);
            }
        }

        //yield return new WaitForSeconds(1f);

        currentNode = node;
        currentNode.nodeSelected = true;

        if (currentNode.nodeEvent != null)
        {
            //yield return new WaitForSeconds(0.5f);
            OpenDialogue();
        }

        navigatable = true;
        yield break;
    }

    private void OpenDialogue()
    {
        dialogueUI.GetComponent<EventDialogue>().InitializeDialogue(currentNode.nodeEvent);
        dialogueUI.SetActive(true);        
    }
    private void OpenShop()
    {
        shopUI.SetActive(true);
    }

    public void BeginEvent()
    {
        switch (currentNode.nodeEvent.eventType)
        {
            case NodeEvents.Encounter:

                //Refer to nodeEvent
                SceneManager.LoadScene("BattleScene");

                break;
            case NodeEvents.DialogueOnly:
                break;
            case NodeEvents.BossEncounter:
                break;
            case NodeEvents.Shop:

                OpenShop();

                break;
            default:
                break;
        }

        CloseDialogue();
    }

    private void CloseDialogue()
    {
        dialogueUI.SetActive(false);
    }
    private void CloseShop()
    {
        shopUI.SetActive(false);
    }

}
