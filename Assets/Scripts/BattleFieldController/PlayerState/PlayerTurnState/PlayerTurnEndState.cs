using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnEndState : PlayerState
{
    public PlayerTurnEndState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    //턴종료를 하기 위한 스테이트
    //어떤 동작이 아닌, 카메라, 또는 이펙트 종료, 또는 캐릭터들의 상태를 완전히 종료하고 자연스러운 상태로 변환하기 위한 과정.
    //여기서는 오버라이드를 쓰지 않은거임.

    public override void Enter()
    {
        //base.Enter();
        player.time = 1;
    }

    public override void Exit()
    {
        //base.Exit();

        for (int i = 0; i < TurnManager.Instance.enemys.Count && i < TurnManager.Instance.EnemyInitialPosition.Length; i++)
        {
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                TurnManager.Instance.enemys[i].transform.position = TurnManager.Instance.EnemyInitialPosition[i];
            }
        }

    }

    public override void Update()
    {
        //base.Update();
        if(player.time < 0.66f)
        {
            player.isMyTurn = false;
            TurnManager.Instance.TurnEnd();
           // player.isMyTurn = false;
            player.stateMachine.ChangeState(player.idleState);
        }
    }

    

   
}
