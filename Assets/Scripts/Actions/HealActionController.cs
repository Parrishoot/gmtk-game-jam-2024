using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealActionController : CharacterActionControllerWithMetadata<HealActionMetadata>
{
    private List<HexSpaceManager> targetableHexes = new List<HexSpaceManager>();

    public HealActionController(MoveableOccupantManager moveableOccupantManager, HealActionMetadata meta) : base(moveableOccupantManager, meta)
    {
    }

    public override void Begin()
    {
        List<HexSpaceManager> hexesInDistance = occupantManager.Hex.GetHexesInRange(Meta.Range);
        hexesInDistance.Add(occupantManager.Hex);

        foreach(HexSpaceManager targetHex in hexesInDistance) {
            if(targetHex.Occupant != null && targetHex.Occupant.IsDamageable() && targetHex.Occupant.HealthController.CanBeHealed()) {
                targetableHexes.Add(targetHex);
                targetHex.MaterialController.SetColor(Color.green);
            }
        }

        HexMasterManager.Instance.OnHexClicked += CheckHeal;
    }

    private void CheckHeal(HexSpaceManager manager)
    {
        if(!targetableHexes.Contains(manager)) {
            Debug.Log("Clicking hex without a valid target - not processing");
            return;
        }

        foreach(HexSpaceManager targetHex in targetableHexes) {
            targetHex.MaterialController.ResetColor();
        }

        HexMasterManager.Instance.OnHexClicked -= CheckHeal;

        manager.Occupant.HealthController.Heal(Meta.HealAmount);
        ActionEnded?.Invoke();
    }
}
