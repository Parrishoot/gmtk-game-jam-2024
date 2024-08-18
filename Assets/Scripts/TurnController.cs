using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnController : StateMachine
{
    public Action TurnOver { get; set; }

    public abstract void StartTurn();

    public void EndTurn() {
        TurnOver?.Invoke();
    }
}
