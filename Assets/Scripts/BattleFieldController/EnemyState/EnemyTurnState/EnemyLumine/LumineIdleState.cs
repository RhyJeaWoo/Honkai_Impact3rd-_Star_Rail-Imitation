using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumineIdleState : EnemyState
{
    private EnemyLumine lumine;
    public LumineIdleState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
    }

    public override void Enter()
    {
        base.Enter();

        TurnManager.Instance.Enemy_target_simbol.SetActive(false);
        TurnManager.Instance.Player_target_simbol.SetActive(false);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

      

        if (enemy.isMyTurn)
        {
            stateMachine.ChangeState(lumine.selectState);
        }
    }

    
}
