using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateWaitState : PlayerState
{
    public PlayerUltimateWaitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.toEnemyPos = TurnManager.Instance.EnemyTransForm;//타겟 지정


        // 타겟 방향으로 회전함
        player.transform.LookAt(player.toEnemyPos);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
}
