using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType // TODO: Create multiple tile types to be used for terrain generation
{
    Empty, Normal
}

public class GridManager : MonoBehaviour
{
    public Vector2Int gridSize;
    public GridCell[] gridCells;
    public GridCell cellPrefab;
    public BattleManager battleManager;
    public int mapSeed;

    public List<GridCell> playerSpawnCells = new List<GridCell>();
    public List<GridCell> enemySpawnCells = new List<GridCell>();

    private void Awake()
    {
        mapSeed = System.Environment.TickCount;
        InitGrid();
        battleManager.SpawnPlayers(playerSpawnCells);
        battleManager.SpawnEnemies(enemySpawnCells);
        battleManager.InitTurns();
    }

    void InitGrid()
    {
        System.Random prng = new System.Random(mapSeed);
        //change later for differnt map size
        gridSize.x = prng.Next(15, 16);
        gridSize.y = prng.Next(15, 16);

        gridCells = new GridCell[gridSize.x * gridSize.y];
        for(int y = 0, i = 0; y < gridSize.y; y++)
        {
            for(int x = 0; x < gridSize.x; x++)
            {
                CreateCell(x, y, i++);
            }
        }

        foreach(GridCell c in gridCells)
        {
            c.cellBorder.SetSprites();
            c.cellBorder.SetVisible();
        }

        // Set ally spawns
        int i1 = (int)(gridSize.x * 1.5f);
        GridCell start1 = gridCells[i1];
        playerSpawnCells.Add(start1);
        foreach(GridCell cell in start1.neighbors)
        {
            playerSpawnCells.Add(cell);
        }

        // Set enemy spawns
        int i2 = (gridSize.x * gridSize.y) - (int)(gridSize.x * 1.5f);
        GridCell start2 = gridCells[i2];
        enemySpawnCells.Add(start2);
        foreach (GridCell cell in start2.neighbors)
        {
            enemySpawnCells.Add(cell);
        }

    }

    void CreateCell(int x, int y, int i)
    {
        // Instantiate Cell
        GridCell cell = gridCells[i] = Instantiate<GridCell>(cellPrefab);
        Vector3 pos = IsoHelperFunctions.XYToIso(new Vector3(x, y, 0f));
        cell.pathNode.position = new Vector2Int(x, y);
        cell.transform.SetParent(this.transform, false);
        cell.transform.localPosition = pos;

        // Get elevation from height map
        float grass = NoiseMap.fBM(x, y, mapSeed, 0.75f, 1, 0.75f);
        cell.grassLevel = Mathf.RoundToInt(3 * grass);

        float height = NoiseMap.fBM(x, y, mapSeed, 0.35f, 1, 0.5f); //TODO: Change this function to be my own custom noise/Perloin map eventually
        cell.elevation = Mathf.RoundToInt(3 * height);
        cell.SetSprite();
        cell.SetCollider();
        
        // Set Neighbors
        if (x > 0)
        {
            cell.SetNeighbor(CellDirection.W, gridCells[i - 1]);
        }
        if (y > 0)
        {
            cell.SetNeighbor(CellDirection.S, gridCells[i - gridSize.x]);
        }
        if (x > 0 && y > 0)
        {
            cell.SetNeighbor(CellDirection.SW, gridCells[i - gridSize.x - 1]);
        }
        if (x < gridSize.x - 1 && y > 0)
        {
            cell.SetNeighbor(CellDirection.SE, gridCells[i - gridSize.x + 1]);
        }
    }

    public void ClearCells()
    {
        foreach(GridCell cell in gridCells)
        {
            cell.selectable = false;
            cell.spriteRenderer.color = Color.white;
        }
    }
}
