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

    public override Vector3 GetPositionForCoordinate(Vector2Int coordinate) {
        return new Vector3((coordinate.x - Mathf.Floor(Board.GetLength(0) / 2)) * HexHorizontalOffset + (coordinate.y % 2 * HexHorizontalOffset / 2f), 
                           0f, 
                           -((coordinate.y - Mathf.Floor(Board.GetLength(1) / 2)) * HexVerticalOffset));
    }
}
