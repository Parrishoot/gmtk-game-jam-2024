using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerTargetSelector : TargetSelector
{
    protected TargetableActionControllerBase action;

    public override void BeginSelection(TargetableActionControllerBase action)
    {
        this.action = action;

        HexMasterManager.Instance.OnHexClicked += Process;
    }

    public override void CancelSelection(TargetableActionControllerBase action)
    {
        HexMasterManager.Instance.OnHexClicked -= Process;
    }

    private void Process(HexSpaceManager targetHex) {
        action.ProcessSelection(targetHex);    
    }
}
