using System;
using System.Collections.Generic;
using UnityEngine;

public class DiveActionController : CharacterActionControllerWithMetadata<DiveActionMetadata>
{

    private List<HexSpaceManager> targetableHexes = new List<HexSpaceManager>();

    public DiveActionController(CharacterManager moveableOccupantManager, DiveActionMetadata meta) : base(moveableOccupantManager, meta)
    {
    }

    public override void Begin()
    {
        List<HexSpaceManager> hexesInDistance = characterManager.Hex.GetHexesInRange(Meta.Range);
        hexesInDistance.Add(characterManager.Hex);

        foreach(HexSpaceManager targetHex in hexesInDistance) {
            if(!targetHex.IsOccupied() && targetHex.ChildBoardManager != null) {
                targetableHexes.Add(targetHex);
                targetHex.MaterialController.SetColor(Color.blue);
            }
        }

        HexMasterManager.Instance.OnHexClicked += CheckDive;
    }

    public override void Cancel()
    {
        foreach(HexSpaceManager targetHex in targetableHexes) {
            targetHex.MaterialController.ResetColor();
        }

        HexMasterManager.Instance.OnHexClicked -= CheckDive;
    }

    private void CheckDive(HexSpaceManager manager)
    {
        //TODO: REVIEW THIS
        if(!targetableHexes.Contains(manager)) {
            Debug.Log("Clicking hex without a valid target - not processing");
            return;
        }

        Cancel();

        BoardManager childBoardManager = manager.ChildBoardManager;
        HexSpaceManager childCenter = childBoardManager.GetClosestOpenHexTo(childBoardManager.LayoutController.GetCenter().Coordinate);

        childCenter.Occupy(characterManager);
        characterManager.MaterialController.Hide();       
        
        ActionEnded?.Invoke();
    }
}
