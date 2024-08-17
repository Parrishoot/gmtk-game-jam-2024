using System;
using UnityEngine;

public class CoolSelectable : MonoBehaviour
{
    public Action OnClick { get; set; }

    public Action OnHoverStart { get; set; }

    public Action OnHoverStop { get; set; }

    private void OnMouseDown() {
        OnClick?.Invoke();
    }

    private void OnMouseEnter() {
        OnHoverStart?.Invoke();
    }

    private void OnMouseExit() {
        OnHoverStop?.Invoke();
    }
}
