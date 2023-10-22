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
        //base.Enter(); 애니메이션을 쓰지 않을거기 때문에 잠금
    }

    public override void Exit()
    {
        //base.Exit(); 애니메이션 안쓸거니까 잠금
    }

    public override void Update()
    {
        base.Update();
    }
}
