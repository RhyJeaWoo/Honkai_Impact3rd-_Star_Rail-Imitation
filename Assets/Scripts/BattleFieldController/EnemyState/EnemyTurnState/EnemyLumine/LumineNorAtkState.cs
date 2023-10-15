using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumineNorAtkState : EnemyState
{
    private EnemyLumine lumine;
    public LumineNorAtkState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.time = 3f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.time < 0)
        {
            enemy.stateMachine.ChangeState(lumine.idleState);
            lumine.isMyTurn = false;
            TurnManager.Instance.TurnEnd();
        }
    }
}
