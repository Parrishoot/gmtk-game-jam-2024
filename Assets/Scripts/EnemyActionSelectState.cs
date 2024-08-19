using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyActionSelectState : GenericState<EnemyTurnManager>
{

    private CharacterActionController selectedAction;

    public EnemyActionSelectState(EnemyTurnManager stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnd()
    {
        
    }

    public override void OnStart()
    {
        List<CharacterManager> enemies = TeamMasterManager.Instance.Managers[CharacterType.ENEMY].Roster;

        // TODO: UPDATE THIS LOGIC
        CharacterManager selectedEnemy = enemies.GetRandomSelection();

        List<CharacterActionController> actionControllers = selectedEnemy.ActionPool.Actions.Select(x => x.GetController(selectedEnemy)).ToList();
        foreach(CharacterActionController actionController in actionControllers) {
            actionController.Load();
        }

        List<CharacterActionController> validActions = actionControllers.Where(x => x.IsValid()).ToList();

        if(validActions.Count == 0) {
            End();
            return;
        }

        selectedAction = validActions.GetRandomSelection();

        Debug.Log("Enemy performing: " + selectedAction.GetType().ToString());

        selectedAction.ActionEnded += End;

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
        
    }

    private void End() {
        StateMachine.ChangeState(StateMachine.IdleState);
        GameManager.Instance.EndCurrentTurn();
    }
}
