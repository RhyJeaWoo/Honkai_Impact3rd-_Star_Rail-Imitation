using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LumineSelectState : EnemyState
{
    //���⼭ ��̳��� ������ �������� ������������.
    //Ȯ���� �����Ű�, ��̳��� ������ �Ϲ� ���� 50% �׸��� ������ 25% ������2 25% �� ����
    //���⼭ ��̳��� ü���� 30% ������ ������ ���
    //��̳״� ���� ������ ���� �����ϰ� Ư�� ������ �ߵ���.


    private EnemyLumine lumine;
    public LumineSelectState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
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
