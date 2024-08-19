using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TargetableActionControllerBase : CharacterActionController
{
    public List<HexSpaceManager> TargetableHexes { get; private set; } = new List<HexSpaceManager>();

    protected TargetableActionControllerBase(CharacterManager characterManager) : base(characterManager)
    {
    }

    
    protected abstract Color TargetColor();

    protected abstract bool IsValidTargetSpace(HexSpaceManager targetHex);

    protected abstract void PerformAction(HexSpaceManager selectedHex);

    public override bool IsValid()
    {
        return TargetableHexes.Count > 0;
    }

    public override void Cancel()
    {
        ResetTargets();
        
    }

    public virtual void ProcessSelection(HexSpaceManager manager)
    {
        if(!TargetableHexes.Contains(manager)) {
            Debug.Log("Clicking hex without a valid target - not processing");
            return;
        }

        ResetTargets();

        PerformAction(manager);

        ActionEnded?.Invoke();
    }

    public void ResetTargets() {
        foreach(HexSpaceManager targetHex in TargetableHexes) {
            targetHex.MaterialController.ResetColor();
        }
        characterManager.TargetSelector.EndSelection(this);
    }
}
