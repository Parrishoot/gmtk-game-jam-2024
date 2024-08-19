using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMasterManager : Singleton<HexMasterManager>
{
    public Action ZoomFinished;

    public Action<HexSpaceManager> OnHexClicked { get; set; }

    public HexSpaceManager ActiveHex { get; set ; }

    // FOR DEBUGGING
    public HexSpaceManager HoverHex { get; set ; }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && ActiveHex != null) {
            ZoomOut();
        }
    }

    public void ZoomOut() {
        ActiveHex?.ZoomOut();
        ActiveHex = null;
    }

    public void BroadcastZoomFinished() {
        ZoomFinished?.Invoke();
        ZoomFinished = null;
    }
}
