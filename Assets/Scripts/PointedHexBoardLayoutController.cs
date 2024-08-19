using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointedHexBoardLayoutController : HexBoardLayoutController
{
    public override float HexWidth => (hexSize + hexBuffer) * Mathf.Sqrt(3);

    public override float HexHeight => (hexSize + hexBuffer) * 2;

    public override float HexHorizontalOffset => HexWidth;

    public override float HexVerticalOffset => .75f * HexHeight;

    protected override int[,] Configuration => new int[,]
    {       
        {0, 1, 1, 1, 0},
          {1, 1, 1, 1, 0},
        {1, 1, 1, 1, 1},
          {1, 1, 1, 1, 0},
        {0, 1, 1, 1, 0}
    };

    public override CubedCoordinate GetCubedCoordinates(Vector2Int a)
    {
        int q = a.x - (a.y - (a.y % 2)) / 2;
        int r = a.y;

        return new CubedCoordinate(q, r, -(q + r));
    }

    public override Vector3 GetPositionForCoordinate(Vector2Int coordinate) {
        return new Vector3((coordinate.x - Mathf.Floor(Board.GetLength(0) / 2)) * HexHorizontalOffset + (coordinate.y % 2 * HexHorizontalOffset / 2f), 
                           0f, 
                           -((coordinate.y - Mathf.Floor(Board.GetLength(1) / 2)) * HexVerticalOffset));
    }

    public override List<HexSpaceManager> GetAdjacentObjects(Vector2Int coordinate) {
        
        int hexOffset = coordinate.y  % 2;

        List<Vector2Int> adjacentOffsets = new List<Vector2Int>{
            new Vector2Int(hexOffset - 1, -1),
            new Vector2Int(hexOffset, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(hexOffset - 1, 1),
            new Vector2Int(hexOffset, 1)
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
}
