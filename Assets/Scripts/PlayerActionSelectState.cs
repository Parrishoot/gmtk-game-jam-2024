using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionSelectState : GenericState<PlayerTurnManager>
{
    private CharacterManager currentCharacter;

    public PlayerActionSelectState(PlayerTurnManager stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnd()
    {
        HexMasterManager.Instance.OnHexClicked -= CheckDisplayActions;
    }

    public override void OnStart()
    {
        HexMasterManager.Instance.OnHexClicked += CheckDisplayActions;
    }

    private void CheckDisplayActions(HexSpaceManager manager)
    {
        if(!manager.IsOccupied()) {
            return;
        }

        foreach(CharacterManager characterManager in TeamManager.Instance.Roster[CharacterType.PLAYER]) {
            if(manager.Occupant.Equals(characterManager)) {
                MovePanelManager.Instance.SetCards(characterManager);
                currentCharacter = characterManager;
                return;
            }
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        if(Input.GetMouseButtonDown(1) && currentCharacter != null) {
            currentCharacter.ActionPool.CancelAction();
            MovePanelManager.Instance.Clear();
            currentCharacter = null;
        }
    }
}
