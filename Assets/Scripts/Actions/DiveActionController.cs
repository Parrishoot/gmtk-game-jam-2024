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
        HexMasterManager.Instance.ZoomOnClick = false;
        characterManager.AnimationController.Dive(selectedHex.OccupantPivot.position, () => End(selectedHex));
    }

    private void End(HexSpaceManager selectedHex) {

        characterManager.Hide();
        characterManager.transform.localScale = Vector3.one;

        BoardManager childBoardManager = selectedHex.ChildBoardManager;
        HexSpaceManager childCenter = childBoardManager.GetClosestOpenHexTo(childBoardManager.LayoutController.GetCenter().Coordinate);

        characterManager.transform.position = childCenter.OccupantPivot.position;
        childCenter.Occupy(characterManager);

        childBoardManager.boardControlManager.UpdateControlType();

        HexMasterManager.Instance.ZoomOnClick = true;

        ActionEnded?.Invoke();
    }

    protected override Color TargetColor()
    {
        return Color.blue;
    }
}
