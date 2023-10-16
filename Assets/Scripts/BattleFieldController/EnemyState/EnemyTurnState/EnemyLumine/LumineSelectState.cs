using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LumineSelectState : EnemyState
{
    //여기서 루미네의 패턴을 랜덤으로 돌려버릴꺼임.
    //확률로 돌릴거고, 루미네의 패턴은 일반 공격 50% 그리고 강공격 25% 강공격2 25% 로 나감
    //여기서 루미네의 체력이 30% 밑으로 떨어질 경우
    //루미네는 위의 패턴을 전부 무시하고 특수 공격을 발동함.
    int RandomPatter = Random.Range(0, 3);
     
    private EnemyLumine lumine;
    public LumineSelectState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
       
     
    }

    public override void Enter()
    {
        base.Enter();
        lumine.vircam.MoveToTopOfPrioritySubqueue();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

     
            if(lumine.curhp < lumine.maxhp/2 )
            { }
            else
            {
                stateMachine.ChangeState(lumine.norAtkState);
            }
          
       
      



    }

   
}
