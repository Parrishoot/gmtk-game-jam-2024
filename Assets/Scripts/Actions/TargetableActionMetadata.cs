using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetableActionMetadata: ActionMetadata
{
    [field:SerializeReference]
    public int Range { get; protected set; } = 1;
}
