using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateEndState : PlayerState
{
    
    public PlayerUltimateEndState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        //base.Enter(); �ִϸ��̼��� ���� �����ű� ������ ���
    }

    public override void Exit()
    {
        //base.Exit(); �ִϸ��̼� �Ⱦ��Ŵϱ� ���
    }

    public override void Update()
    {
        base.Update();
    }
}
