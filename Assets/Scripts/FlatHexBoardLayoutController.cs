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

    public override Vector3 GetPositionForCoordinate(Vector2Int coordinate)
    {
        return new Vector3((coordinate.x - Mathf.Floor(Board.GetLength(0) / 2)) * HexHorizontalOffset, 
                           0f, 
                           -((coordinate.y - Mathf.Floor(Board.GetLength(1) / 2)) * HexVerticalOffset + (coordinate.x % 2 * HexVerticalOffset / 2f)));
    }
}
