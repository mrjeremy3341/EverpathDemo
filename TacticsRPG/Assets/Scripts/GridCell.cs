using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteList
{
    public Sprite[] sprites;
}

public class GridCell : MonoBehaviour
{
    public BattleUnit currentUnit;
    public bool selectable = false;
    [Range(0, 8)]
    public int elevation;
    [Range(0, 3)]
    public int grassLevel;
    public CellType cellType;
    public GridCell[] neighbors;
    public SpriteList[] spriteLists;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D cellCollider;
    public PathNode pathNode;

    public void SetNeighbor(CellDirection direction, GridCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

    private void Update()
    {
        //TODO: Add dedicated tile visuals script to each cell, get the tile assignment out fo update to increase performance

        Sprite[] sprites = spriteLists[grassLevel].sprites;
        if(elevation == 0)
        {
            spriteRenderer.sprite = null;
            cellCollider.enabled = false;
        }
        else
        {
            spriteRenderer.sprite = sprites[elevation - 1];
            cellCollider.offset = new Vector2(1, elevation * .12f); 
        }
    }
}
