using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomTargetSelector : TargetSelector
{
    public override void BeginSelection(TargetableActionControllerBase action)
    {
        action.ProcessSelection(action.TargetableHexes.GetRandomSelection());
    }

    public override void EndSelection(TargetableActionControllerBase action)
    {

    }
}
