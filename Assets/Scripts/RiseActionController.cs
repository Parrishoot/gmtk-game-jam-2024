using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseActionController : CharacterActionControllerWithMetadata<RiseActionMetadata>
{
    private HexSpaceManager targetHex;

    public RiseActionController(CharacterManager characterManager, RiseActionMetadata meta) : base(characterManager, meta)
    {
    }

    public override void Begin()
    {

        if(targetHex == null) {
            ActionEnded?.Invoke();
            return;
        }

        TeamMasterManager.Instance.Managers[characterManager.CharacterType].SpendActionPoints(Meta.Cost);

        characterManager.AnimationController.Rise(End);
    }

    private void End() {
        HexSpaceManager updatedHex = characterManager.Hex.ParentBoardManager.ContainingHex;

        characterManager.transform.position = targetHex.OccupantPivot.position;
        characterManager.transform.localScale = Vector3.one * 5;
        targetHex.Occupy(characterManager);

        updatedHex.ChildBoardManager.boardControlManager.UpdateControlType();

        ActionEnded?.Invoke();
    }

    public override void Cancel()
    {
        
    }

    public override bool IsValid()
    {
        return targetHex != null;
    }

    public override void Load()
    {
        if(characterManager.Hex.ParentBoardManager.ContainingHex == null) {
            return;
        }

        List<HexSpaceManager> neighbors = characterManager.Hex.ParentBoardManager.ContainingHex.GetAdjacentHexes();

        foreach(HexSpaceManager neighbor in neighbors) {
            if(!neighbor.IsOccupied() && neighbor.ChildBoardManager.boardControlManager.ControlType == ControlType.EMPTY) {
                targetHex = neighbor;
                return;
            }
        }
    }
}
