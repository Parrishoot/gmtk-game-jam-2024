using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionController : TargetableActionController<AttackActionMetadata>
{
    public AttackActionController(CharacterManager moveableOccupantManager, AttackActionMetadata meta) : base(moveableOccupantManager, meta)
    {
    }

    protected override Color TargetColor()
    {
        return Color.red;
    }

    protected override bool IsValidTargetSpace(HexSpaceManager targetHex)
    {
        return targetHex.Occupant != null && 
                targetHex.Occupant.IsDamageable() && 
                targetHex.Occupant.CharacterType != characterManager.CharacterType;
    }

    protected override void PerformAction(HexSpaceManager selectedHex)
    {
        characterManager.AnimationController.Attack(selectedHex.OccupantPivot.position, () => EndAction(selectedHex));
    }

    private void EndAction(HexSpaceManager selectedHex)
    {
        selectedHex.Occupant.HealthController.Damage(Meta.Damage + characterManager.GetAdjacencyBonuses());
        ActionEnded?.Invoke();
    }
}
