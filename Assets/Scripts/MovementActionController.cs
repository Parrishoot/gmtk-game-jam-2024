using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementActionController : CharacterActionControllerWithMetadata<MovementActionMetadata>
{
    // Store the paths to avoid recalculating
    private Dictionary<HexSpaceManager, List<HexSpaceManager>> availablePaths = new Dictionary<HexSpaceManager, List<HexSpaceManager>>();

    public MovementActionController(MoveableOccupantManager occupantManager, MovementActionMetadata meta) : base(occupantManager, meta)
    {
    }

    public override void Begin()
    {
        // Find all reachable hexes within x distance from the current hex
        HexSpaceManager hex = occupantManager.Hex;
        List<HexSpaceManager> hexesWithinDistance = hex.GetHexesInRange(Meta.MovementDistance);

        foreach(HexSpaceManager targetHex in hexesWithinDistance) {

            List<HexSpaceManager> path = PathFinder.GetPath(hex.ParentBoardManager, hex.Coordinate, targetHex.Coordinate);

            if(path != null) {
                targetHex.MaterialController.SetColor(Color.yellow);
                availablePaths[targetHex] = path; 
            }
        }

        HexMasterManager.Instance.OnHexClicked += CheckBeginMovement;
    }

    private void CheckBeginMovement(HexSpaceManager manager)
    {
        if(!availablePaths.ContainsKey(manager)) {
            Debug.Log("Clicking hex without an available path - not processing");
            return;
        }

        HexMasterManager.Instance.OnHexClicked -= CheckBeginMovement;

        foreach(HexSpaceManager targetHex in availablePaths.Keys) {
            targetHex.MaterialController.ResetColor();
        }

        occupantManager.MovementController.OnMovementFinished += EndAction;
        occupantManager.MovementController.Move(availablePaths[manager]);
    }

    private void EndAction()
    {
        ActionEnded?.Invoke();
    }
}
