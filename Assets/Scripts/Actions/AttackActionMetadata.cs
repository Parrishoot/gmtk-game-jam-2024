using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "ScriptableObjects/ActionMeta/AttackAction", order = 2)]
public class AttackActionMetadata : ActionMetadata
{
    [field:SerializeReference]
    public int Damage { get; private set; } = 1;

    [field:SerializeReference]
    public int Range { get; private set; } = 1;

    public override CharacterActionController GetController(CharacterManager characterManager)
    {
        return new AttackActionController(characterManager, this);
    }

    public override string GetDescription()
    {
        return "Attack an enemy for " + Damage + " damage";
    }
}
