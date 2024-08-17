using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoardZoomManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager;

    [SerializeField]
    private Transform hexParentTransform;

    void Start() {
        boardManager.EventManager.ZoomIn += ZoomIn;
        boardManager.EventManager.ZoomOut += ZoomOut;
    }

    public void ZoomIn() {

        transform.localScale = Vector3.one / 6;
        
        transform.DOScale(Vector3.one, TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);

        boardManager.ForEachHex((hexSpaceManager) => {
            hexSpaceManager.FadeIn();
        });
    }

    public void ZoomOut() {
        transform.DOScale(Vector3.one / 6, TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);
        boardManager.ForEachHex((hexSpaceManager) => {
            hexSpaceManager.FadeOut();
        });
    }
}
