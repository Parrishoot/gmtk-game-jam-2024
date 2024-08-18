using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealAction", menuName = "ScriptableObjects/ActionMeta/HealAction", order = 3)]
public class HealActionMetadata : ActionMetadata
{
    [field:SerializeReference]    
    public int HealAmount { get; private set; } = 2;   

    [field:SerializeReference]
    public int Range { get; private set; } = 1;    

    public override CharacterActionController GetController(MoveableOccupantManager occupantManager)
    {
        return new HealActionController(occupantManager, this);
    }

    public override string GetDescription()
    {
        return "Heal for " + HealAmount.ToString();
    }
}
