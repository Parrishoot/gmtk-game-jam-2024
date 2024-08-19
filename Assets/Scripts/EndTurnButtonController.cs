using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButtonController : MonoBehaviour
{
    public void Start() {
        GameManager.Instance.TurnStarted += (characterType) => {
            if (characterType == CharacterType.PLAYER) {
                gameObject.SetActive(true);
            }
        };
    }

    public void ButtonClicked() {
        GameManager.Instance.EndCurrentTurn();
        gameObject.SetActive(false);
    }
}
