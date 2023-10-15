using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    //턴이 되기 전까지 대기하는 상태
    public PlayerIdleState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(player.name + "idle 상태가 실행되었음");
     
     

    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log(player.name + "Idle 중");

     

        //TurnManager.Instance.WhoisTurn(player.gameObject);

        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.turnGetState);
        }

    }

}
