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
    public CellBorder cellBorder;

    public void SetNeighbor(CellDirection direction, GridCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

    public Vector2 GetTargetPosition()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y + elevation * 0.25f;

        return new Vector2(x, y);
    }

    public void SetSprite()
    {
        Sprite[] sprites = spriteLists[grassLevel].sprites;
        spriteRenderer.sprite = sprites[elevation];
    }

    public void SetCollider()
    {
        cellCollider.offset = new Vector2(0, elevation * .25f + .03f);
    }
}
