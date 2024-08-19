using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionMetadata : ScriptableObject
{
    [field:SerializeReference]
    public String Name { get; set; }

    [field:SerializeReference]
    public int Cost { get; set; } = 2;

    public abstract CharacterActionController GetController(CharacterManager occupantManager);

    public abstract String GetDescription();
}
