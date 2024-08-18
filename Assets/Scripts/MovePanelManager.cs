using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePanelManager : Singleton<MovePanelManager>
{
    [SerializeField]
    public GameObject moveCardPrefab; 

    [SerializeField]
    private Transform moveCardTransform;

    private List<MoveCardController> currentCards = new List<MoveCardController>();

    public void Clear() {
        foreach(MoveCardController moveCardController in currentCards) {
            Destroy(moveCardController.gameObject);
        }

        currentCards = new List<MoveCardController>();
    } 

    public void SetCards(CharacterManager characterManager) {
        Clear();

        foreach(ActionMetadata action in characterManager.ActionPool.Actions) {
            MoveCardController moveCardController = Instantiate(moveCardPrefab, moveCardTransform).GetComponent<MoveCardController>();
            moveCardController.Init(characterManager, action);
            currentCards.Add(moveCardController);
        }
    }
}
