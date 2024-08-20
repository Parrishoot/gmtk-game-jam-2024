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

    [SerializeField]
    private Image background;

    [SerializeField]
    private Transform pivot;

    private CharacterManager characterManager;

    private ActionMetadata actionMetadata;

    private CharacterActionController characterActionController;

    public void Init(CharacterManager characterManager, ActionMetadata actionMetadata) {

        titleText.text = actionMetadata.Name;
        descriptionText.text = actionMetadata.GetDescription(characterManager.GetAdjacencyBonuses()).ToUpper();
        costText.text = actionMetadata.Cost.ToString();

        characterActionController = actionMetadata.GetController(characterManager);
        characterActionController.Load();

        if(TeamMasterManager.Instance.Managers[characterManager.CharacterType].ActionPoints < actionMetadata.Cost || !characterActionController.IsValid()) {
            selectable.interactable = false;
            
            Color color = Color.white;
            color.a = .6f;

            titleText.SetAlpha(.6f);
            descriptionText.SetAlpha(.6f);
            costText.SetAlpha(.6f);
            background.color = color;

            transform.localScale = Vector3.one * .9f;
        }

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
        pivot.DOScale(Vector3.one * 1.125f, .1f).SetEase(Ease.InOutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pivot.DOScale(Vector3.one * (selectable.interactable ? 1f : .9f), .1f).SetEase(Ease.InOutSine);
    }
}
