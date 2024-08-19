using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RiseAction", menuName = "ScriptableObjects/ActionMeta/RiseAction", order = 4)]
public class RiseActionMetadata : ActionMetadata
{
    public override CharacterActionController GetController(CharacterManager occupantManager)
    {
        return new RiseActionController(occupantManager, this);
    }

    public override string GetDescription(int adjacencyBonuses)
    {
        return "Rise from the depths!";
    }
}
