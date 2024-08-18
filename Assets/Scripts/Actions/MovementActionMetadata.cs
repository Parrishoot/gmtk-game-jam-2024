using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MovementAction", menuName = "ScriptableObjects/ActionMeta/MovementAction", order = 1)]
public class MovementActionMetadata : ActionMetadata
{
    [field:SerializeField]
    public int MovementDistance { get; set; } = 2;

    public override CharacterActionController GetController(MoveableOccupantManager moveableOccupantManager)
    {
        return new MovementActionController(moveableOccupantManager, this);
    }

    public override string GetDescription()
    {
        return "Move " + MovementDistance + " Spaces";
    }
}
