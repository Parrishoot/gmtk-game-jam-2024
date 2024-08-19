using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCardController : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    [SerializeField]
    private TMP_Text costText;

    [SerializeField]
    private Selectable selectable;

    private CharacterManager characterManager;

    private ActionMetadata actionMetadata;

    public void Init(CharacterManager characterManager, ActionMetadata actionMetadata) {

        titleText.text = actionMetadata.Name;
        descriptionText.text = actionMetadata.GetDescription(characterManager.GetAdjacencyBonuses());
        costText.text = actionMetadata.Cost.ToString();

        this.characterManager = characterManager;
        this.actionMetadata = actionMetadata;

        
    }

    public void StartAction() {
        characterManager.ActionPool.StartAction(actionMetadata);
        MovePanelManager.Instance.Clear();
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartAction();
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
