using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private TurnController playerTurnController;

    // TODO: Update this
    public bool IsPlayersTurn() {
        return true;
    }

    public void Start() {
        playerTurnController.StartTurn();
    }
}
