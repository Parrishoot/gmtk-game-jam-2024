using System;
using System.Collections.Generic;
using UnityEngine;

public class DiveActionController : TargetableActionController<DiveActionMetadata>
{
    public DiveActionController(CharacterManager moveableOccupantManager, DiveActionMetadata meta) : base(moveableOccupantManager, meta)
    {
    }

    protected override bool IsValidTargetSpace(HexSpaceManager targetHex)
    {
        return !targetHex.IsOccupied() && targetHex.ChildBoardManager != null;
    }

    protected override void PerformAction(HexSpaceManager selectedHex)
    {
        BoardManager childBoardManager = selectedHex.ChildBoardManager;
        HexSpaceManager childCenter = childBoardManager.GetClosestOpenHexTo(childBoardManager.LayoutController.GetCenter().Coordinate);

        characterManager.transform.position = childCenter.OccupantPivot.position;
        childCenter.Occupy(characterManager);
        characterManager.MaterialController.Hide();   
    }

    protected override Color TargetColor()
    {
        return Color.blue;
    }
}
