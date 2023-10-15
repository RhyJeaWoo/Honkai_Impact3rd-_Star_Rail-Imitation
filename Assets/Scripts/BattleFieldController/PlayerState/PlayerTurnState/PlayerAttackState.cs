using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.time = 2;
    }

    public override void Exit()
    {
        base.Exit();


    }

    public override void Update()
    {
        base.Update();
        if (player.time < 0)
        {
            player.stateMachine.ChangeState(player.idleState);
            player.isMyTurn = false;
            TurnManager.Instance.TurnEnd();
            TurnManager.Instance.target_simbol.SetActive(false);
        }
    }
}
