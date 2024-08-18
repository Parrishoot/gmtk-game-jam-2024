using System.Collections;
using System.Collections.Generic;
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
        CharacterActionController selectedAction = selectedEnemy.ActionPool.Actions.GetRandomSelection().GetController(selectedEnemy);

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
