using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnManager : TurnController
{
    public EmptyState IdleState { get; private set; }

    public EnemyActionSelectState EnemyActionSelectState { get; private set; }

    public override void StartTurn()
    {
        ChangeState(EnemyActionSelectState);
    }

    void Awake()
    {
        IdleState = new EmptyState(this);
        EnemyActionSelectState = new EnemyActionSelectState(this);
    }
}
