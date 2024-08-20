using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float transitionTime = .1f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, transitionTime).SetEase(Ease.InOutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, transitionTime).SetEase(Ease.InOutSine);
    }
}
