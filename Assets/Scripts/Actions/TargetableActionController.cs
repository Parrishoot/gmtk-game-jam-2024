using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetableActionController<T> : CharacterActionControllerWithMetadata<T>
where T: TargetableActionMetadata
{
    private List<HexSpaceManager> targetableHexes = new List<HexSpaceManager>();

    protected TargetableActionController(CharacterManager characterManager, T meta) : base(characterManager, meta)
    {
    }

    protected abstract Color TargetColor();

    protected abstract bool IsValidTargetSpace(HexSpaceManager targetHex);

    protected abstract void PerformAction(HexSpaceManager selectedHex);

    public override void Begin()
    {
        List<HexSpaceManager> hexesInDistance = characterManager.Hex.GetHexesInRange(Meta.Range);

        foreach(HexSpaceManager targetHex in hexesInDistance) {
            if(IsValidTargetSpace(targetHex)) {
                targetableHexes.Add(targetHex);
                targetHex.MaterialController.SetColor(TargetColor());
            }
        }

        HexMasterManager.Instance.OnHexClicked += CheckTargetSelected;
    }

    
    public override void Cancel()
    {
        foreach(HexSpaceManager targetHex in targetableHexes) {
            targetHex.MaterialController.ResetColor();
        }

        HexMasterManager.Instance.OnHexClicked -= CheckTargetSelected;
    }

    private void CheckTargetSelected(HexSpaceManager manager)
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
