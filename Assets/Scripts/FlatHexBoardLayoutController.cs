using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatHexBoardLayoutController : HexBoardLayoutController
{
    public override float HexWidth => (hexSize + hexBuffer) * 2;

    public override float HexHeight => (hexSize + hexBuffer) * Mathf.Sqrt(3);

    public override float HexHorizontalOffset => .75f * HexWidth;

    public override float HexVerticalOffset => HexHeight;

    protected override int[,] Configuration => new int[,]
    {       
        {0, 1, 1, 1, 0},
          {1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1},
          {1, 1, 1, 1, 1},
        {0, 0, 1, 0, 0}
    };

    public override List<HexSpaceManager> GetAdjacentObjects(Vector2Int coordinate)
    {
         
        
        int hexOffset = coordinate.x  % 2;

        List<Vector2Int> adjacentOffsets = new List<Vector2Int>{
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, hexOffset),
            new Vector2Int(-1, hexOffset - 1),
            new Vector2Int(1, hexOffset - 1),
            new Vector2Int(1, hexOffset)
        };

        List<HexSpaceManager> adjacentObjects = new List<HexSpaceManager>();

        foreach(Vector2Int offset in adjacentOffsets) {

            Vector2Int coordinateToCheck = coordinate + offset;

            if(Valid(coordinateToCheck)) {
                adjacentObjects.Add(Board[coordinateToCheck.x, coordinateToCheck.y]);
            }
        }

        return adjacentObjects;
    
    }

    public override CubedCoordinate GetCubedCoordinates(Vector2Int a)
    {
        int q = a.x;
        int r = a.y - (a.x - (a.x&1)) / 2;

        return new CubedCoordinate(q, r, -(q + r));
    }

    public override Vector3 GetPositionForCoordinate(Vector2Int coordinate)
    {
        return new Vector3((coordinate.x - Mathf.Floor(Board.GetLength(0) / 2)) * HexHorizontalOffset, 
                           0f, 
                           -((coordinate.y - Mathf.Floor(Board.GetLength(1) / 2)) * HexVerticalOffset + (coordinate.x % 2 * HexVerticalOffset / 2f)));
    }
}
