using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealAction", menuName = "ScriptableObjects/ActionMeta/HealAction", order = 3)]
public class HealActionMetadata : TargetableActionMetadata
{
    [field:SerializeReference]    
    public int HealAmount { get; private set; } = 2;   

    public override CharacterActionController GetController(CharacterManager occupantManager)
    {
        return new HealActionController(occupantManager, this);
    }

    public override string GetDescription(int adjacencyBonuses)
    {
        return "Heal for " + (HealAmount + adjacencyBonuses).ToString();
    }
}
