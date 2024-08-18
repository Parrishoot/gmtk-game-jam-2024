using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetSelector: MonoBehaviour
{
    public abstract void BeginSelection(TargetableActionControllerBase action);

    public abstract void EndSelection(TargetableActionControllerBase action);
}
