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


        if (lumine.anim.GetCurrentAnimatorStateInfo(0).IsName("Str2Atk") && lumine.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Debug.Log("루미네 StrAtk2 이 조건문은 실행되었음");

            lumine.isMyTurn = false;
            TurnManager.Instance.TurnEnd();

            stateMachine.ChangeState(lumine.idleState);
        }
        else
        {
            Debug.Log("루미네 StrAtk2 이 조건문은 실행되지않았음");
        }


        if (lumine.isMyTurn)
        {
            TurnManager.Instance.TurnEnd(); //턴매니저의 턴 엔드도 풀어 버려야함.(다시 돌려야되니까)

            lumine.isMyTurn = false;
        }
    }
}
