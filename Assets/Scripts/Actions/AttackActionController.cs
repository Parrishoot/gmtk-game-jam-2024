using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionController : CharacterActionControllerWithMetadata<AttackActionMetadata>
{
    private List<HexSpaceManager> targetableHexes = new List<HexSpaceManager>();

    public AttackActionController(CharacterManager moveableOccupantManager, AttackActionMetadata meta) : base(moveableOccupantManager, meta)
    {
    }

    public override void Begin()
    {
        List<HexSpaceManager> hexesInDistance = characterManager.Hex.GetHexesInRange(Meta.Range);

        foreach(HexSpaceManager targetHex in hexesInDistance) {
            if(IsValidAttackSpace(targetHex)) {
                targetableHexes.Add(targetHex);
                targetHex.MaterialController.SetColor(Color.red);
            }
        }

        HexMasterManager.Instance.OnHexClicked += CheckAttack;
    }

    private bool IsValidAttackSpace(HexSpaceManager targetHex) {
        return targetHex.Occupant != null && 
                targetHex.Occupant.IsDamageable() && 
                targetHex.Occupant.CharacterType != characterManager.CharacterType;
    }

    public override void Cancel()
    {
        foreach(HexSpaceManager targetHex in targetableHexes) {
            targetHex.MaterialController.ResetColor();
        }

        HexMasterManager.Instance.OnHexClicked -= CheckAttack;
    }

    private void CheckAttack(HexSpaceManager manager)
    {
        if(!targetableHexes.Contains(manager)) {
            Debug.Log("Clicking hex without a valid target - not processing");
            return;
        }

        Cancel();

        manager.Occupant.HealthController.Damage(Meta.Damage);
        ActionEnded?.Invoke();
    }
}
