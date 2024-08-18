using System.Collections.Generic;
using UnityEngine;

public class MovementActionController : CharacterActionControllerWithMetadata<MovementActionMetadata>
{
    // Store the paths to avoid recalculating
    private Dictionary<HexSpaceManager, List<HexSpaceManager>> availablePaths = new Dictionary<HexSpaceManager, List<HexSpaceManager>>();

    public MovementActionController(CharacterManager occupantManager, MovementActionMetadata meta) : base(occupantManager, meta)
    {
    }

    public override void Begin()
    {
        // Find all reachable hexes within x distance from the current hex
        HexSpaceManager hex = characterManager.Hex;
        List<HexSpaceManager> hexesWithinDistance = hex.GetHexesInRange(Meta.MovementDistance);

        foreach(HexSpaceManager targetHex in hexesWithinDistance) {

            List<HexSpaceManager> path = PathFinder.GetPath(hex.ParentBoardManager, hex.Coordinate, targetHex.Coordinate);

            if(path != null && path.Count <= Meta.MovementDistance + 1 && !targetHex.ChildBoardManager.HasOccupants()) {
                targetHex.MaterialController.SetColor(Color.yellow);
                availablePaths[targetHex] = path; 
            }
        }

        HexMasterManager.Instance.OnHexClicked += CheckBeginMovement;
    }

    public override void Cancel()
    {
        
        HexMasterManager.Instance.OnHexClicked -= CheckBeginMovement;

        foreach(HexSpaceManager targetHex in availablePaths.Keys) {
            targetHex.MaterialController.ResetColor();
        }
    }

    private void CheckBeginMovement(HexSpaceManager manager)
    {
        if(!availablePaths.ContainsKey(manager)) {
            Debug.Log("Clicking hex without an available path - not processing");
            return;
        }

        Cancel();

        characterManager.MovementController.OnMovementFinished += EndAction;
        characterManager.MovementController.Move(availablePaths[manager]);
    }

    private void EndAction()
    {
        ActionEnded?.Invoke();
    }
}
