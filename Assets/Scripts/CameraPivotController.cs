using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraPivotController : Singleton<CameraPivotController>
{

    private Vector3 startingPos;

    private void Start() {
        startingPos = transform.position;
    }

    public void SetHexHighlight(HexSpaceManager hexSpaceManager) {
        transform.DOMove(startingPos + hexSpaceManager.GetOffset(), TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);
    }

    public void ResetPosition() {
        transform.DOMove(startingPos, TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);
    }
}
