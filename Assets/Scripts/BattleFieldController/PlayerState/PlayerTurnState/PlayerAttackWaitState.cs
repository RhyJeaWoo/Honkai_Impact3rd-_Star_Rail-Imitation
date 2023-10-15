using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackWaitState : PlayerState
{

    //���� ������ ���� ���� ������ ������
    public PlayerAttackWaitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(player.name + "�� ����(�Ϲݰ���) ������");
        TurnManager.Instance.target_simbol.SetActive(true);
        //���⼭ �⺻���� �غ� �ִϸ��̼� ����
        //player.vircam = 
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

       //TurnManager.Instance.WhoisTurn(player.gameObject);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //���⼭ ���� ��� �غ� idle ����
            //Ȯ������ Ű �Է½� �۵�
            player.stateMachine.ChangeState(player.attackState);
            //Debug.Log("Q�� ������");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Ű����� ��ų �غ� ������� �̵�
            player.stateMachine.ChangeState(player.skillwaitState);
            //Debug.Log("E�� ������");
        }

    }
}
