using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBorder : MonoBehaviour
{
    public GridCell gridCell;

    public SpriteRenderer spriteNW;
    public SpriteRenderer spriteNE;
    public SpriteRenderer spriteSW;
    public SpriteRenderer spriteSE;

    public SpriteList[] spriteLists;

    public void SetSprites()
    {
        SpriteList spriteList = spriteLists[gridCell.elevation];

        spriteNW.sprite = spriteList.sprites[0];
        spriteNE.sprite = spriteList.sprites[1];
        spriteSW.sprite = spriteList.sprites[2];
        spriteSE.sprite = spriteList.sprites[3];
    }

    public void SetVisible()
    {
        if (gridCell.neighbors[6] == null || gridCell.neighbors[6].elevation < gridCell.elevation)
        {
            spriteNW.gameObject.SetActive(true);
        }

        if (gridCell.neighbors[0] == null || gridCell.neighbors[0].elevation < gridCell.elevation)
        {
            spriteNE.gameObject.SetActive(true);
        }

        if (gridCell.neighbors[5] != null && gridCell.neighbors[5].elevation < gridCell.elevation && gridCell.neighbors[6].elevation < gridCell.elevation)
        {
            spriteSW.gameObject.SetActive(true);
        }

        if (gridCell.neighbors[1] != null && gridCell.neighbors[0].elevation < gridCell.elevation && gridCell.neighbors[0].elevation < gridCell.elevation) 
        { 
            spriteSE.gameObject.SetActive(true);
        }
    }
}
