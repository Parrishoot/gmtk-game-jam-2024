using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action<CharacterType> TurnStarted { get; set; }

    public Action<CharacterType> TurnEnded { get; set; }

    [SerializeField]
    private PlayerTurnManager playerTurnController;

    [SerializeField]
    private TurnController enemyTurnController;

    private Dictionary<CharacterType, TurnController> turnControllers = new Dictionary<CharacterType, TurnController>();

    private CharacterType currentControllingPlayer;

    // TODO: Update this
    public bool IsPlayersTurn() {
        return true;
    }

    protected override void Awake()
    {
        base.Awake();

        turnControllers[CharacterType.PLAYER] = playerTurnController;
        turnControllers[CharacterType.ENEMY] = enemyTurnController;
    }

    public void Start() {
        StartTurn(CharacterType.PLAYER);
    }

    public void EndCurrentTurn() {
        
        TurnEnded?.Invoke(currentControllingPlayer);
        turnControllers[currentControllingPlayer].EndTurn();

        currentControllingPlayer = currentControllingPlayer == CharacterType.PLAYER ? CharacterType.ENEMY : CharacterType.PLAYER;

        StartTurn(currentControllingPlayer);
    }

    private void StartTurn(CharacterType characterType) {
        currentControllingPlayer = characterType;
        
        TurnStarted?.Invoke(currentControllingPlayer);
        turnControllers[currentControllingPlayer].StartTurn();
    }
}
