using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndTurnButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void Start() {
        GameManager.Instance.TurnStarted += (characterType) => {
            if (characterType == CharacterType.PLAYER) {
                gameObject.SetActive(true);
            }
        };

        GameManager.Instance.TurnEnded += (characterType) => {
            if (characterType == CharacterType.PLAYER) {
                gameObject.SetActive(false);
            }
        };
    }

    public void ButtonClicked() {
        GameManager.Instance.EndCurrentTurn();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * 1.125f, .1f).SetEase(Ease.InOutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, .1f).SetEase(Ease.InOutSine);
    }
}
