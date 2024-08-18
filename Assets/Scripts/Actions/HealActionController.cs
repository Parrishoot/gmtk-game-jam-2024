using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealActionController : TargetableActionController<HealActionMetadata>
{
    public HealActionController(CharacterManager moveableOccupantManager, HealActionMetadata meta) : base(moveableOccupantManager, meta)
    {
    }

    protected override Color TargetColor()
    {
        return Color.green;
    }

    protected override bool IsValidTargetSpace(HexSpaceManager targetHex)
    {
        return targetHex.Occupant != null && 
                targetHex.Occupant.IsDamageable() && 
                targetHex.Occupant.HealthController.CanBeHealed() && 
                targetHex.Occupant.CharacterType == characterManager.CharacterType;
    }

    protected override void PerformAction(HexSpaceManager selectedHex)
    {
        selectedHex.Occupant.HealthController.Heal(Meta.HealAmount);
    }
}
