using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "ScriptableObjects/ActionMeta/AttackAction", order = 2)]
public class AttackActionMetadata : TargetableActionMetadata
{
    [field:SerializeReference]
    public int Damage { get; private set; } = 1;

    public override CharacterActionController GetController(CharacterManager characterManager)
    {
        return new AttackActionController(characterManager, this);
    }

    public override string GetDescription(int adjacencyBonuses)
    {
        return "Attack an adjacent enemy for " + (Damage + adjacencyBonuses) + " damage";
    }
}
