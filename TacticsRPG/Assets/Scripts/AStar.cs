using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar // TODO: Make range and path calculations take into account elevation differences, +1 movment cost to movment greater than 1 elevation step
{  
    public static List<GridCell> FindMovementRange(GridCell startCell, int range)
    {
        PathNode startNode = startCell.pathNode;
        
        Queue<PathNode> process = new Queue<PathNode>();
        List<PathNode> nodesInRange = new List<PathNode>();

        process.Enqueue(startNode);
        startNode.visited = true;

        while(process.Count > 0)
        {
            PathNode currentNode = process.Dequeue();
            nodesInRange.Add(currentNode);

            if(FindPath(startNode.gridCell, currentNode.gridCell).Count < range)
            {
                foreach (PathNode n in GetNeighbors4(currentNode))
                {
                    if (!n.visited)
                    {
                        n.previousNode = currentNode;
                        n.visited = true;

                        process.Enqueue(n);
                    }
                }
            }
        }

        List<GridCell> cellsInRange = new List<GridCell>();
        foreach(PathNode n in nodesInRange)
        {
            cellsInRange.Add(n.gridCell);
        }
        ClearCells(cellsInRange);

        return cellsInRange;
    }

    public static List<GridCell> FindAttackRange(GridCell startCell, int range)
    {
        PathNode startNode = startCell.pathNode;

        Queue<PathNode> process = new Queue<PathNode>();
        List<PathNode> nodesInRange = new List<PathNode>();

        process.Enqueue(startNode);
        startNode.visited = true;

        while (process.Count > 0)
        {
            PathNode currentNode = process.Dequeue();
            nodesInRange.Add(currentNode);

            if (FindPath(startNode.gridCell, currentNode.gridCell).Count < range)
            {
                foreach (PathNode n in GetNeighbors8(currentNode))
                {
                    if (!n.visited)
                    {
                        n.previousNode = currentNode;
                        n.visited = true;

                        process.Enqueue(n);
                    }
                }
            }
        }

        List<GridCell> cellsInRange = new List<GridCell>();
        foreach (PathNode n in nodesInRange)
        {
            cellsInRange.Add(n.gridCell);
        }
        ClearCells(cellsInRange);

        return cellsInRange;
    }

    public static List<GridCell> FindPath(GridCell startCell, GridCell targetCell) //TODO: Maybe optimize the search if i think it is needed
    {
        PathNode startNode = startCell.pathNode;
        PathNode targetNode = targetCell.pathNode;

        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();
        openList.Add(startNode);

        while(openList.Count > 0)
        {
            PathNode currentNode = openList[0];
            for(int i = 1; i < openList.Count; i++)
            {
                if(openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach(PathNode n in GetNeighbors4(currentNode))
            {
                if (closedList.Contains(n))
                {
                    continue;
                }

                int newMoveCost = currentNode.gCost + GetDistance(currentNode, n);
                if(newMoveCost < n.gCost || !openList.Contains(n))
                {
                    n.gCost = newMoveCost;
                    n.hCost = GetDistance(n, targetNode);
                    n.previousNode = currentNode;

                    if (!openList.Contains(n))
                    {
                        openList.Add(n);
                    }
                }
            }
        }

        return null;
    }

    static void ClearCells(List<GridCell> processedCells)
    {
        foreach(GridCell cell in processedCells)
        {
            cell.pathNode.visited = false;
            cell.pathNode.gCost = 0;
            cell.pathNode.hCost = 0;
            cell.pathNode.previousNode = null;
        }
    }

    static int GetDistance(PathNode nodeA, PathNode nodeB) // TODO: Maybe change distance calculation
    {
        int dstX = Mathf.Abs(nodeA.position.x - nodeB.position.x);
        int dstY = Mathf.Abs(nodeA.position.y - nodeB.position.y);

        return dstX + dstY;
    }

    static List<PathNode> GetNeighbors4(PathNode currentNode)
    {
        List<PathNode> neighbors = new List<PathNode>();
        for(int i = 0; i < 8; i++)
        {
            if(i == 0 || i == 2 || i == 4 || i == 6)
            {
                if(currentNode.gridCell.neighbors[i] != null)
                {
                    neighbors.Add(currentNode.gridCell.neighbors[i].pathNode);
                }
            }
        }

        return neighbors;
    }

    static List<PathNode> GetNeighbors8(PathNode currentNode)
    {
        List<PathNode> neighbors = new List<PathNode>();
        for (int i = 0; i < 8; i++)
        {
            if (currentNode.gridCell.neighbors[i] != null)
            {
                neighbors.Add(currentNode.gridCell.neighbors[i].pathNode);
            }
        }

        return neighbors;
    }

    static List<GridCell> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<GridCell> path = new List<GridCell>();
        PathNode currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode.gridCell);
            currentNode = currentNode.previousNode;
        }
        path.Reverse();

        return path;
    }
}
