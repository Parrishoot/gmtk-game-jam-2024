using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HexZoomController : MonoBehaviour
{
    [SerializeField]
    private HexSpaceManager hexSpaceManager;

    [SerializeField]
    private CoolSelectable hexSelectable;

    [SerializeField]
    private MeshRenderer hexMeshRenderer;

    private const float ZOOM_AMOUNT = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        hexSpaceManager.EventManager.ZoomIn += ZoomIn;
        hexSpaceManager.EventManager.ZoomOut += ZoomOut;

        hexSpaceManager.EventManager.FadeIn += FadeIn;
        hexSpaceManager.EventManager.FadeOut += FadeOut;

        hexSpaceManager.EventManager.Hide += Hide;

        hexSelectable.OnClick += hexSpaceManager.ZoomIn; 
    }

    public void ZoomIn() {

        // Only allow one highlighted Hex at a time
        // TODO: Change this if I need to add more than 1 layer
        if(ActiveHexManager.Instance.ActiveHex != null || !hexSpaceManager.ContainsBoard()) {
            return;
        }

        ActiveHexManager.Instance.ActiveHex = hexSpaceManager;
        CameraPivotController.Instance.SetHexHighlight(hexSpaceManager);

        // Scale the parent and slide it back at the same rate
        transform.parent.DOScale(Vector3.one * ZOOM_AMOUNT, TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);

        // For some reason couldn't get this math to work - this close enough for the jam :(
        transform.parent.DOMove(transform.parent.position - (hexSpaceManager.GetOffset() * (ZOOM_AMOUNT * .8f)), TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);

        hexSpaceManager.FadeOut();

        hexSpaceManager.ChildBoardManager.ZoomIn();
        
        hexSpaceManager.ChildBoardManager.EnableBoard();
        hexSpaceManager.ParentBoardManager.DisableBoard();
        
    }

    public void ZoomOut() {

        // Only allow one highlighted Hex at a time
        // TODO: Change this if I need to add more than 1 layer
        if(ActiveHexManager.Instance.ActiveHex != hexSpaceManager) {
            return;
        }

        ActiveHexManager.Instance.ActiveHex = hexSpaceManager;
        CameraPivotController.Instance.ResetPosition();

        transform.parent.DOScale(Vector3.one, TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);
        transform.parent.DOMove(transform.parent.position + (hexSpaceManager.GetOffset() * (ZOOM_AMOUNT * .8f)), TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);
        
        hexSpaceManager.FadeIn();

        transform.DOScale(Vector3.one, TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);
        hexSpaceManager.ChildBoardManager.ZoomOut();

        hexSpaceManager.ParentBoardManager.EnableBoard();
        hexSpaceManager.ChildBoardManager.DisableBoard();
    } 

    private void FadeIn() {
        hexMeshRenderer.material.DOFade(1f, TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);
    }

    private void FadeOut() {
        hexMeshRenderer.material.DOFade(0f, TransitionConfig.ZOOM_TIME).SetEase(TransitionConfig.ZOOM_TWEEN_TYPE);
    }

    private void Hide() {
        Color color = hexMeshRenderer.material.color;
        color.a = 0f;

        hexMeshRenderer.material.color = color;
    }
}
