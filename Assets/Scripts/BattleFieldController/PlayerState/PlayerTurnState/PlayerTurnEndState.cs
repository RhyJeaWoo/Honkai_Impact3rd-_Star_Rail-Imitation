using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnEndState : PlayerState
{
    public PlayerTurnEndState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    //�����Ḧ �ϱ� ���� ������Ʈ
    //� ������ �ƴ�, ī�޶�, �Ǵ� ����Ʈ ����, �Ǵ� ĳ���͵��� ���¸� ������ �����ϰ� �ڿ������� ���·� ��ȯ�ϱ� ���� ����.
    //���⼭�� �������̵带 ���� ��������.

    public override void Enter()
    {
        //base.Enter();
        player.time = 1;
    }

    public override void Exit()
    {
        //base.Exit();

        for (int i = 0; i < TurnManager.Instance.enemys.Count && i < TurnManager.Instance.EnemyInitialPosition.Length; i++)
        {
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                TurnManager.Instance.enemys[i].transform.position = TurnManager.Instance.EnemyInitialPosition[i];
            }
        }

    }

    public override void Update()
    {
        //base.Update();
        if(player.time < 0.66f)
        {
            player.isMyTurn = false;
            TurnManager.Instance.TurnEnd();
           // player.isMyTurn = false;
            player.stateMachine.ChangeState(player.idleState);
        }
    }

    

   
}
