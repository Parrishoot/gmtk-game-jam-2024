using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCardController : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    [SerializeField]
    private Selectable selectable;

    private CharacterManager characterManager;

    private ActionMetadata actionMetadata;

    public void Init(CharacterManager characterManager, ActionMetadata actionMetadata) {

        titleText.text = actionMetadata.Name;
        descriptionText.text = actionMetadata.GetDescription();

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
}
