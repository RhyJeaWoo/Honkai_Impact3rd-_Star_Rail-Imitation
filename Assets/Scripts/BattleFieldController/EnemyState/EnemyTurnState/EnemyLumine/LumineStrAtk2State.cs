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
            Debug.Log("��̳� StrAtk2 �� ���ǹ��� ����Ǿ���");

            lumine.isMyTurn = false;
            TurnManager.Instance.TurnEnd();

            stateMachine.ChangeState(lumine.idleState);
        }
        else
        {
            Debug.Log("��̳� StrAtk2 �� ���ǹ��� ��������ʾ���");
        }


        if (lumine.isMyTurn)
        {
            TurnManager.Instance.TurnEnd(); //�ϸŴ����� �� ���嵵 Ǯ�� ��������.(�ٽ� �����ߵǴϱ�)

            lumine.isMyTurn = false;
        }
    }
}
