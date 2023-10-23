using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateEndState : PlayerState
{
    //�ñرⰡ ����ǰ� ���⼭ ������ ó���Ұ���.
    //���⼭ ����Ʈ����
    
    public PlayerUltimateEndState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        //base.Enter(); �ִϸ��̼��� ���� �����ű� ������ ���

        player.vircam[0].MoveToTopOfPrioritySubqueue();

        player.UltimateDamageEvent();

        Debug.Log("ȣ��Ǿ���");


    }

    public override void Exit()
    {
        //base.Exit(); �ִϸ��̼� �Ⱦ��Ŵϱ� ���
    }

    public override void Update()
    {
        base.Update();
        player.isUltimate = false;
        TurnManager.Instance.TurnEnd();
        stateMachine.ChangeState(player.idleState);
    }
}
