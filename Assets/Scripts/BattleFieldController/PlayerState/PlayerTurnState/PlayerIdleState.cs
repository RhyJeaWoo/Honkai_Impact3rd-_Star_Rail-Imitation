using System;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    //���� �Ǳ� ������ ����ϴ� ����
    public PlayerIdleState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log(player.name + "idle ���°� ����Ǿ���");
        //player.skin[1].enabled = false;
        //player.skin[0].enabled = false;



    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("idle ���� ����");

    }

    public override void Update()
    {
        base.Update();

        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.turnGetState);
        }

      

        if (TurnManager.Instance.playerUltimate.Count == 0)
        {
            //Debug.Log("�ñر� ����Ʈ�� ��� ����");
        }
        else if (TurnManager.Instance.playerUltimate[0] == null)
        {
            Debug.Log("�ñرⰡ ���Ե��� �ʾ���");
        }
        else if (TurnManager.Instance.playerUltimate[0].name == player.transform.gameObject.name && !TurnManager.Instance.StopTurn)
        {
            player.stateMachine.ChangeState(player.isMyUltimateTurnState);
            // �� ���·� ��ȯ�ϸ� ��̳׳��� �ٸ� ������ �ൿ�� ��� �����Ǿ�� ��.
            Debug.Log("�ñر� ����Ʈ�� �ش� �÷��̾��� ��ü�� �����ϸ� �����");
        }



    }

}


/*
     if (TurnManager.Instance.playerUltimate[0] == null)
      {
          throw new ArgumentOutOfRangeException("�ñرⰡ ���Ե��� �ʾ���");


     }
     else
     {
          Debug.Log("�ο��� �����");
          if (TurnManager.Instance.playerUltimate[0].name == player.transform.gameObject.name)
          {
              player.stateMachine.ChangeState(player.isMyUltimateTurnState);
              //���� �̻��·� �Ѿ�� ��̳׳� �ٸ� ������ �ൿ�� ��� �����Ǿ����.
              Debug.Log("�����");
          }
          else
          {
              Debug.Log("�ñر� ����Ʈ�� ���°� ����");
          }
     }*/