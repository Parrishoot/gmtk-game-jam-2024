using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : TurnController
{
    public EmptyState IdleState { get; private set; }

    public PlayerActionSelectState PlayerActionSelectState { get; private set; }

    public override void StartTurn()
    {
        ChangeState(PlayerActionSelectState);
    }

    void Awake()
    {
        IdleState = new EmptyState(this);
        PlayerActionSelectState = new PlayerActionSelectState(this);
    }
}
