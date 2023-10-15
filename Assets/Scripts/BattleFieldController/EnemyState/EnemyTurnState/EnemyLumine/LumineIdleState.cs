using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumineIdleState : EnemyState
{
    private EnemyLumine lumine;
    public LumineIdleState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
    }

    public override void Enter()
    {
        base.Enter();

        TurnManager.Instance.target_simbol.SetActive(false);

        enemy.moveCamPos = enemy.vircam.transform.position; //���� ���� ī�޶� �ʱ� ��ġ ��ǥ�� ������
        enemy.virCamRot = enemy.vircam.transform.rotation; //���� ī�޶� ȸ�� ���� ����

       // enemy.ObjPos.transform.position = enemy.transform.position;// �� ������Ʈ ��ġ���� Vector�� ����

        enemy.virCamPos = enemy.vircam.transform.position;//�Ȱ��� ī�޶��� ���� ���� ������.


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.vircam.transform.position = enemy.moveCamPos + new Vector3(enemy.ObjPos.transform.position.x, 0, 0);//Ű�Ƴ����� �̰� -2�� �ھƹ���. �׷��� ��������.
        enemy.vircam.transform.rotation = enemy.virCamRot; //ȸ���� ����

        if (enemy.isMyTurn)
        {
            stateMachine.ChangeState(lumine.selectState);
        }
    }

    
}
