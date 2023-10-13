using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumineStrAtk2State : EnemyState
{
    private EnemyLumine lumine;
    public LumineStrAtk2State(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
