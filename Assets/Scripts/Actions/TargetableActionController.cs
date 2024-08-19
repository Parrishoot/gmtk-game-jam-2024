using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetableActionController<T> : TargetableActionControllerBase
where T: TargetableActionMetadata
{
    protected T Meta { get; private set; }

    protected TargetableActionController(CharacterManager characterManager, T meta) : base(characterManager)
    {
        this.Meta = meta;
    }

    public override void Load()
    {
        List<HexSpaceManager> hexesInDistance = characterManager.Hex.GetHexesInRange(Meta.Range);

        foreach(HexSpaceManager targetHex in hexesInDistance) {
            if(IsValidTargetSpace(targetHex)) {
                TargetableHexes.Add(targetHex);
            }
        }
    }

    public override void Begin()
    {
        foreach(HexSpaceManager targetHex in TargetableHexes) {
            if(IsValidTargetSpace(targetHex)) {
                targetHex.MaterialController.SetColor(TargetColor());
            }
        }

        characterManager.TargetSelector.BeginSelection(this);
    }
}
