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
        selectable.OnClick += ProcessClick;
        
        selectable.OnHoverStart += () => hexSpaceManager.EventManager.HoverStart?.Invoke();
        selectable.OnHoverStop += () => hexSpaceManager.EventManager.HoverEnd?.Invoke();

        hexSpaceManager.ParentBoardManager.EventManager.EnableBoard += TurnOn;
        hexSpaceManager.ParentBoardManager.EventManager.DisableBoard += TurnOff;


        if (hexSpaceManager.ParentBoardManager.BoardEnabled) {
            TurnOn();
        } 
        else {
            TurnOff();
        } 
    }

    private void ProcessClick() {

        HexMasterManager.Instance.OnHexClicked?.Invoke(hexSpaceManager);
        hexSpaceManager.EventManager.Clicked?.Invoke();

        // TODO: TURN THIS BACK ON
        if(isSelectable && hexSpaceManager.ChildBoardManager != null && hexSpaceManager.ChildBoardManager.HasOccupants()) {   
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
