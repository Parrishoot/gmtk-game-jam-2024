using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoolSelectable : MonoBehaviour
{
    public Action OnClick { get; set; }

    public Action OnHoverStart { get; set; }

    public Action OnHoverStop { get; set; }

    private void OnMouseDown() {
        
        if (IsBlocked()) {
            return;
        }
        
        OnClick?.Invoke();
    }

    private void OnMouseEnter() {
        
        // if (IsBlocked()) {
        //     return;
        // }

        OnHoverStart?.Invoke();
    }

    private void OnMouseExit() {
        
        // if (IsBlocked()) {
        //     return;
        // }

        OnHoverStop?.Invoke();
    }

    private bool IsBlocked() {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for(int i = 0; i < raycastResults.Count; i++) {
            if(raycastResults[i].gameObject.layer == LayerMask.NameToLayer("BlockUI")) {
                return true;
            }
        } 

        return false;
    }
}
