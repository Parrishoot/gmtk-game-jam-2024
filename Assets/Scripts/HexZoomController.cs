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

    private const float ZOOM_AMOUNT = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        hexSpaceManager.EventManager.ZoomIn += ZoomIn;
        hexSpaceManager.EventManager.ZoomOut += ZoomOut;

        hexSpaceManager.EventManager.FadeIn += FadeIn;
        hexSpaceManager.EventManager.FadeOut += FadeOut;

        hexSpaceManager.EventManager.Hide += Hide;
    }

    public void ZoomIn() {

        // Only allow one highlighted Hex at a time
        // TODO: Change this if I need to add more than 1 layer
        if(HexMasterManager.Instance.ActiveHex != null || !hexSpaceManager.ContainsBoard()) {
            return;
        }

        HexMasterManager.Instance.ActiveHex = hexSpaceManager;
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
        if(HexMasterManager.Instance.ActiveHex != hexSpaceManager) {
            return;
        }

        HexMasterManager.Instance.ActiveHex = hexSpaceManager;
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
        hexSpaceManager.MaterialController.FadeIn(TransitionConfig.ZOOM_TIME, TransitionConfig.ZOOM_TWEEN_TYPE);

        if(hexSpaceManager.IsOccupied()) {
            hexSpaceManager.Occupant.FadeIn(TransitionConfig.ZOOM_TIME, TransitionConfig.ZOOM_TWEEN_TYPE);
        }
    }

    private void FadeOut() {
        hexSpaceManager.MaterialController.FadeOut(TransitionConfig.ZOOM_TIME, TransitionConfig.ZOOM_TWEEN_TYPE);

        if(hexSpaceManager.IsOccupied()) {
            hexSpaceManager.Occupant.FadeOut(TransitionConfig.ZOOM_TIME, TransitionConfig.ZOOM_TWEEN_TYPE);
        }
    }

    private void Hide() {
        hexSpaceManager.MaterialController.Hide();
    }
}
