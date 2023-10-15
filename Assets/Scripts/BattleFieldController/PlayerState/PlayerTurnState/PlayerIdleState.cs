using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    //���� �Ǳ� ������ ����ϴ� ����
    public PlayerIdleState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(player.name + "idle ���°� ����Ǿ���");
     
     

    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log(player.name + "Idle ��");

     

        //TurnManager.Instance.WhoisTurn(player.gameObject);

        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.turnGetState);
        }

    }

}
