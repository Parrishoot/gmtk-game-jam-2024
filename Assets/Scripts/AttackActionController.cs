using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionController : CharacterActionControllerWithMetadata<AttackActionMetadata>
{
    private List<HexSpaceManager> targetableHexes = new List<HexSpaceManager>();

    public AttackActionController(MoveableOccupantManager moveableOccupantManager, AttackActionMetadata meta) : base(moveableOccupantManager, meta)
    {
    }

    public override void Begin()
    {
        List<HexSpaceManager> hexesInDistance = occupantManager.Hex.GetHexesInRange(Meta.Range);

        foreach(HexSpaceManager targetHex in hexesInDistance) {
            if(targetHex.Occupant != null && targetHex.Occupant.IsDamageable()) {
                targetableHexes.Add(targetHex);
                targetHex.MaterialController.SetColor(Color.red);
            }
        }

        HexMasterManager.Instance.OnHexClicked += CheckAttack;
    }

    private void CheckAttack(HexSpaceManager manager)
    {
        if(!targetableHexes.Contains(manager)) {
            Debug.Log("Clicking hex without a valid target - not processing");
            return;
        }

        foreach(HexSpaceManager targetHex in targetableHexes) {
            targetHex.MaterialController.ResetColor();
        }

        HexMasterManager.Instance.OnHexClicked -= CheckAttack;

        manager.Occupant.HealthController.Damage(Meta.Damage);
        ActionEnded?.Invoke();
    }
}
