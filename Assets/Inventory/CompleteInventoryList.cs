using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CompleteInventoryList : SerializedScriptableObject
{
    public Dictionary<string, InventoryItem> completeInventoryList = new Dictionary<string, InventoryItem>();
}
