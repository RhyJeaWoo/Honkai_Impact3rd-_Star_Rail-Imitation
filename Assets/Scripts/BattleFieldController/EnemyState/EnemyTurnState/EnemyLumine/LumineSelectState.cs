using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LumineSelectState : EnemyState
{
    //���⼭ ��̳��� ������ �������� ������������.
    //Ȯ���� �����Ű�, ��̳��� ������ �Ϲ� ���� 50% �׸��� ������ 25% ������2 25% �� ����
    //���⼭ ��̳��� ü���� 30% ������ ������ ���
    //��̳״� ���� ������ ���� �����ϰ� Ư�� ������ �ߵ���.
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
