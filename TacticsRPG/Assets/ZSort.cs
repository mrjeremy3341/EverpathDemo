using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSort : MonoBehaviour
{
    public bool isUnit = false;

    private void Update()
    {
        if (isUnit)
        {
            BattleUnit unit = GetComponent<BattleUnit>();
            float zPos = unit.currentCell.transform.position.y;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, zPos - .25f);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.y);
        }
    }
}
