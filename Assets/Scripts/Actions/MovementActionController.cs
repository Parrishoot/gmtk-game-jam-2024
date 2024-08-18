using System.Collections.Generic;
using UnityEngine;

public class MovementActionController : TargetableActionController<MovementActionMetadata>
{
    // Store the paths to avoid recalculating
    private Dictionary<HexSpaceManager, List<HexSpaceManager>> availablePaths = new Dictionary<HexSpaceManager, List<HexSpaceManager>>();

    public MovementActionController(CharacterManager occupantManager, MovementActionMetadata meta) : base(occupantManager, meta)
    {
    }

    protected override bool IsValidTargetSpace(HexSpaceManager targetHex)
    {
        List<HexSpaceManager> path = PathFinder.GetPath(characterManager.Hex.ParentBoardManager, characterManager.Hex.Coordinate, targetHex.Coordinate);

        bool validPath = path != null && path.Count <= Meta.Range + 1 && !targetHex.ChildBoardManager.HasOccupants();

        if(validPath) {
            availablePaths[targetHex] = path;
        } 

        return validPath;
    }

    protected override void PerformAction(HexSpaceManager selectedHex)
    {
        characterManager.MovementController.OnMovementFinished += EndAction;
        characterManager.MovementController.Move(availablePaths[selectedHex]);
    }

    public override void ProcessSelection(HexSpaceManager manager)
    {
        if(!TargetableHexes.Contains(manager)) {
            Debug.Log("Clicking hex without a valid target - not processing");
            return;
        }

        ResetTargets();

        PerformAction(manager);
    }

    protected override Color TargetColor()
    {
        return Color.yellow;
    }

    private void EndAction()
    {
        ActionEnded?.Invoke();
    }
}
