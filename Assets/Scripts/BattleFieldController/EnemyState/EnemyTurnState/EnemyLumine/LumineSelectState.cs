using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LumineSelectState : EnemyState
{
    //���⼭ ��̳��� ������ �������� ������������.
    //Ȯ���� �����Ű�, ��̳��� ������ �Ϲ� ���� 50% �׸��� ������ 25% ������2 25% �� ����
    //���⼭ ��̳��� ü���� 30% ������ ������ ���
    //��̳״� ���� ������ ���� �����ϰ� Ư�� ������ �ߵ���.
    int RandoPattern = 0;
     
    private EnemyLumine lumine;
    public LumineSelectState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
       
    }

    public override void Enter()
    {
        base.Enter();

        for(int i = 0; i< TurnManager.Instance.playable.Count; i++) 
        {
            if (!TurnManager.Instance.playable[i].isMyTurn)
            {
                TurnManager.Instance.playable[i].skin[0].enabled = true;
                TurnManager.Instance.playable[i].skin[1].enabled = true;
            }
        }

        RandoPattern = Random.Range(0, 3);
        lumine.vircam[0].MoveToTopOfPrioritySubqueue();
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("RandoPattern");
    }

    public override void Update()
    {
        base.Update();

        if(lumine.stateMachine.currentState is LumineSelectState)
        {
            if (RandoPattern == 0)
            {
                stateMachine.ChangeState(lumine.norAtkState);
            }else if(RandoPattern == 1) 
            {
                stateMachine.ChangeState(lumine.norAtkState);
                //stateMachine.ChangeState(lumine.strAtkState);
            }
            else if(RandoPattern == 2)
            {
                stateMachine.ChangeState(lumine.norAtkState);
                //stateMachine.ChangeState(lumine.str2AtkState);
            }

        }


        Debug.Log(lumine.stateMachine.currentState);
            
          
    }

   
}
