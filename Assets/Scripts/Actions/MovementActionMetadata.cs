using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MovementAction", menuName = "ScriptableObjects/ActionMeta/MovementAction", order = 1)]
public class MovementActionMetadata : TargetableActionMetadata
{
    public override CharacterActionController GetController(CharacterManager moveableOccupantManager)
    {
        return new MovementActionController(moveableOccupantManager, this);
    }

    public override string GetDescription()
    {
        return "Move " + Range.ToString() + " Spaces";
    }
}
