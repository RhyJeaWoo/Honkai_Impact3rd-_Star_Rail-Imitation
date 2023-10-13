using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillWaitState : PlayerState
{
    public PlayerSkillWaitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        //���⼭ ��ų �غ� �ִϸ��̼� ����
        Debug.Log(player.name + "�� ��ų ���� ������");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //���⼭ ���� ��� �غ� idle ����
            player.stateMachine.ChangeState(player.attackWaitState);
            //�ٽ� ������� ���� ������� �̵�
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            //Ȯ�� Ű ��ư �Է½� �۵�
            player.stateMachine.ChangeState(player.skillState);
        }
    }
}