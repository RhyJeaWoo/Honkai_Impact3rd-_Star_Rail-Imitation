using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumineUlitimateState : EnemyState
{
    private EnemyLumine lumine;
    public LumineUlitimateState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
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

        if (lumine.isMyTurn)
        {
            TurnManager.Instance.TurnEnd(); //턴매니저의 턴 엔드도 풀어 버려야함.(다시 돌려야되니까)

            lumine.isMyTurn = false;
        }
    }

    
}
