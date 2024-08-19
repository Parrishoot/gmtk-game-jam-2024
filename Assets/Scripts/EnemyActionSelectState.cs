using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyActionSelectState : GenericState<EnemyTurnManager>
{
    public EnemyActionSelectState(EnemyTurnManager stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnd()
    {
        
    }

    public override void OnStart()
    {
        List<CharacterManager> enemies = TeamManager.Instance.Roster[CharacterType.ENEMY];

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

        CharacterActionController selectedAction = validActions.GetRandomSelection();

        selectedAction.ActionEnded += End;

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
