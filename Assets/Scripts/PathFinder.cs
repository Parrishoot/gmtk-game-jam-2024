using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class PathFinder
{

    private class PathNode {

        public HexSpaceManager Hex { get; private set; }

        public int GCost { get; set; } = int.MaxValue;

        public int HCost { get; set; } = int.MaxValue;

        public int FCost => GCost + HCost;

        public PathNode CameFrom { get; set; } = null;

        public PathNode(HexSpaceManager hex) {
            this.Hex = hex;
        }

    }

    public static List<HexSpaceManager> GetPath(BoardManager boardManager, Vector2Int start, Vector2Int end) {

        PathNode[,] grid = InitPathGrid(boardManager);

        PathNode startNode = grid[start.x, start.y];
        PathNode endNode = grid[end.x, end.y];

        List<PathNode> openList = new List<PathNode>() { startNode };
        List<PathNode> closedList = new List<PathNode>();

        
        startNode.HCost = boardManager.LayoutController.CalculateDistance(startNode.Hex.Coordinate, endNode.Hex.Coordinate); 
        startNode.GCost = 0;

        // Cycle to calculate costs
        while(openList.Count > 0) {

            PathNode currentNode = GetLowestFCostNode(openList);
            
            // We reached our destination
            if(currentNode == endNode) {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);

            foreach(PathNode neighborNode in GetNeighbors(currentNode, grid)) {
                
                if(closedList.Contains(neighborNode)) {
                    continue;
                }

                if(neighborNode.Hex.IsOccupied()) {
                    closedList.Add(neighborNode);
                    continue;
                }

                int tentativeGCost = currentNode.GCost + boardManager.LayoutController.CalculateDistance(currentNode.Hex.Coordinate, neighborNode.Hex.Coordinate);

                if(tentativeGCost < neighborNode.GCost) {
                    neighborNode.CameFrom = currentNode;
                    neighborNode.GCost = tentativeGCost;
                    neighborNode.HCost = boardManager.LayoutController.CalculateDistance(currentNode.Hex.Coordinate, endNode.Hex.Coordinate);;
                }

                if(!openList.Contains(neighborNode)) {
                    openList.Add(neighborNode);
                }
            }
        }

        // Inaccessible endpoint
        Debug.LogWarning("No path!");
        return null;
    }

    private static List<PathNode> GetNeighbors(PathNode node, PathNode[,] grid) {
        return node.Hex
        .GetAdjacentHexes()
        .Select(neighbor => grid[neighbor.Coordinate.x, neighbor.Coordinate.y])
        .ToList();
    }

    private static List<HexSpaceManager> CalculatePath(PathNode endNode) {
        
        List<HexSpaceManager> path = new List<HexSpaceManager>();
        
        PathNode currentNode = endNode;
        
        while(currentNode != null) {
            path.Add(currentNode.Hex);
            currentNode = currentNode.CameFrom;
        }
        
        path.Reverse();
        
        return path;
    }

    // TODO: Look into Priortiy Queue
    private static PathNode GetLowestFCostNode(List<PathNode> pathNodeList) {

        PathNode lowestFCostPathNode = pathNodeList[0];

        for(int i = 1; i < pathNodeList.Count; i++) {
            if(pathNodeList[i].FCost < lowestFCostPathNode.FCost) {
                lowestFCostPathNode = pathNodeList[i];
            }
        }

        return lowestFCostPathNode;
    }

    private static PathNode[,] InitPathGrid(BoardManager boardManager) {
        
        HexSpaceManager[,] board = boardManager.LayoutController.Board;

        PathNode[,] pathGrid = new PathNode[board.GetLength(0), board.GetLength(1)];

        for(int i = 0; i < board.GetLength(0); i++) {
            for(int j = 0; j < board.GetLength(1); j++) {

                Vector2Int coordinate = new Vector2Int(j, i);
                
                if(boardManager.LayoutController.Valid(coordinate)) {
                    pathGrid[j, i] = new PathNode(board[j, i]);
                }
            }
        }

        return pathGrid;
    }
}