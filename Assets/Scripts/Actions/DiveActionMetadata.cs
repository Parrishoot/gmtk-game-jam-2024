using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dive", menuName = "ScriptableObjects/ActionMeta/Dive", order = 4)]
public class DiveActionMetadata : TargetableActionMetadata
{

    public override CharacterActionController GetController(CharacterManager occupantManager)
    {
        return new DiveActionController(occupantManager, this);
    }

    public override string GetDescription(int adjacencyBonuses)
    {
        return "Shrink down to enter the lower battlefield on an adjacent hex";
    }
}
