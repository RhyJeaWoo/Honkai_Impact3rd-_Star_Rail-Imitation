using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateState : PlayerState
{
    public PlayerUltimateState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.cureng = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.cureng += 5;
    }

    public override void Update()
    {
        base.Update();
    }

}
