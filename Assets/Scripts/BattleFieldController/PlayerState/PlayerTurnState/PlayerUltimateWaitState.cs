using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateWaitState : PlayerState //여기서 궁극기를 발동할 타겟 지정할거임
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

        //먼저 여기서 공격할 상대를 지정하는 코드를 짜자

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}
