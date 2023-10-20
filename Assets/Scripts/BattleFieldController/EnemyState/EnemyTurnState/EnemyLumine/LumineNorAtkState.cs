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
       
     
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


     
           
        if (lumine.anim.GetCurrentAnimatorStateInfo(0).IsName("NorAtk") && lumine.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Debug.Log("루미네 NorAtk 이 조건문은 실행되었음");

            lumine.isMyTurn = false;
            TurnManager.Instance.TurnEnd();

            stateMachine.ChangeState(lumine.idleState);
        }
        else
        {
            Debug.Log("루미네 NorAtk 이 조건문은 실행되지않았음");
        }
          
     
    }
}
