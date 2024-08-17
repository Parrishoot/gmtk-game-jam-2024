using UnityEngine;

public class HexSelectable : MonoBehaviour
{
    [SerializeField]
    private CoolSelectable selectable;

    [SerializeField]
    private Collider hexCollider;

    [SerializeField]
    private HexSpaceManager hexSpaceManager;

    private bool isSelectable = true;

    // Start is called before the first frame update
    void Start()
    {
        selectable.OnClick += CheckZoom;

        hexSpaceManager.ParentBoardManager.EventManager.EnableBoard += TurnOn;
        hexSpaceManager.ParentBoardManager.EventManager.DisableBoard += TurnOff;


        if (hexSpaceManager.ParentBoardManager.BoardEnabled) {
            TurnOn();
        } 
        else {
            TurnOff();
        } 
    }

    private void CheckZoom() {
        if(isSelectable) {
            hexSpaceManager.ZoomIn();
        }
    }

    private void TurnOn() {
        isSelectable = true;
        hexCollider.enabled = true;
    }

    private void TurnOff() {
        isSelectable = false;
        hexCollider.enabled = false;
    }


}
