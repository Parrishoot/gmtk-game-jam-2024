using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyActionSelectState : GenericState<EnemyTurnManager>
{
    int TOTAL_ACTIONS = 3;

    int remainingActions;

    private CharacterActionController selectedAction;

    private float StartTime;

    public EnemyActionSelectState(EnemyTurnManager stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnd()
    {
        
    }

    public override void OnStart()
    {
        StartTime = Time.time;

        remainingActions = TOTAL_ACTIONS;
        ChooseAction();
    }

    private void ChooseAction() {
        
        HexMasterManager.Instance.ZoomFinished -= ChooseAction;

        List<CharacterManager> enemies = TeamMasterManager.Instance.Managers[CharacterType.ENEMY].Roster;

        // TODO: UPDATE THIS LOGIC
        CharacterManager selectedEnemy = enemies.GetRandomSelection();

        List<CharacterActionController> actionControllers = selectedEnemy.ActionPool.Actions.Select(x => x.GetController(selectedEnemy)).ToList();
        foreach(CharacterActionController actionController in actionControllers) {
            actionController.Load();
        }

        List<CharacterActionController> validActions = actionControllers.Where(x => x.IsValid()).ToList();

        if(validActions.Count == 0) {
            CheckEnd();
            return;
        }

        remainingActions--;

        selectedAction = validActions.GetRandomSelection();

        Debug.Log("Enemy performing: " + selectedAction.GetType().ToString());

        selectedAction.ActionEnded += CheckEnd;

        if(selectedEnemy.Hex.ParentBoardManager.ContainingHex != null &&
            HexMasterManager.Instance.ActiveHex != selectedEnemy.Hex.ParentBoardManager.ContainingHex) {
            
            HexMasterManager.Instance.ZoomFinished += BeginAction;
            selectedEnemy.Hex.ParentBoardManager.ContainingHex.ZoomIn();
        }
        else {
            BeginAction();
        }

    }

    private void BeginAction() {
        selectedAction.Begin();
    }

    public override void OnUpdate(float deltaTime)
    {
        if(Time.time - StartTime > 4) {
            Debug.LogWarning("Enemy is softlocked - shouldn't have gotten here!");
            selectedAction.ActionEnded -= CheckEnd;

            StateMachine.ChangeState(StateMachine.IdleState);
            GameManager.Instance.EndCurrentTurn();
        }
    }

    private void CheckEnd() {

        HexMasterManager.Instance.ZoomFinished -= BeginAction;

        if(remainingActions == 0) {
            StateMachine.ChangeState(StateMachine.IdleState);
            GameManager.Instance.EndCurrentTurn();
        }
        else {
            
            if(HexMasterManager.Instance.ActiveHex != null) {
                HexMasterManager.Instance.ZoomFinished += ChooseAction;
                HexMasterManager.Instance.ZoomOut();
                return;
            }
            else {
                ChooseAction();
            }
        }
    }
}
