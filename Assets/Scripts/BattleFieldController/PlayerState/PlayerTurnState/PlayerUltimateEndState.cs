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

 
        player.UltimateDamageEvent();

        player.playableDirector[0].enabled = false;

        //Debug.Log("ȣ��Ǿ���");


    }

    public override void Exit()
    {
        //base.Exit(); �ִϸ��̼� �Ⱦ��Ŵϱ� ���
        //TurnManager.Instance.ultimateQueue.Dequeue();
        TurnManager.Instance.playerUltimate.Remove(TurnManager.Instance.playerUltimate[0]);
        TurnManager.Instance.TurnEnd();
        TurnManager.Instance.UltimateEnd();
        player.isUltimate = false;

    }

    public override void Update()
    {
        base.Update();

        stateMachine.ChangeState(player.idleState);
    }
}
