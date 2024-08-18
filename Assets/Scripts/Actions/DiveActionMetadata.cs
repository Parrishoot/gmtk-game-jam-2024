using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dive", menuName = "ScriptableObjects/ActionMeta/Dive", order = 4)]
public class DiveActionMetadata : ActionMetadata
{

    [field:SerializeReference]
    public int Range { get; set; } = 1;

    public override CharacterActionController GetController(MoveableOccupantManager occupantManager)
    {
        return new DiveActionController(occupantManager, this);
    }

    public override string GetDescription()
    {
        return "Dive baby, dive!";
    }
}
