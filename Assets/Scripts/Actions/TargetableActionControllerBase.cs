using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetableActionControllerBase : CharacterActionController
{
    protected List<HexSpaceManager> targetableHexes = new List<HexSpaceManager>();

    protected TargetableActionControllerBase(CharacterManager characterManager) : base(characterManager)
    {
    }

    
    protected abstract Color TargetColor();

    protected abstract bool IsValidTargetSpace(HexSpaceManager targetHex);

    protected abstract void PerformAction(HexSpaceManager selectedHex);
    
    public override void Cancel()
    {
        foreach(HexSpaceManager targetHex in targetableHexes) {
            targetHex.MaterialController.ResetColor();
        }

        characterManager.TargetSelector.CancelSelection(this);
    }

    public void ProcessSelection(HexSpaceManager manager)
    {
        if(!targetableHexes.Contains(manager)) {
            Debug.Log("Clicking hex without a valid target - not processing");
            return;
        }

        Cancel();

        PerformAction(manager);
        ActionEnded?.Invoke();
    }
}
