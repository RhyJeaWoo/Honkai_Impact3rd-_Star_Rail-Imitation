using System;
using System.Diagnostics.Tracing;
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
        //Debug.Log(player.name + "idle 상태가 실행되었음");
        //player.skin[1].enabled = false;
        //player.skin[0].enabled = false;



    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("idle 에서 나감");

    }

    public override void Update()
    {
        base.Update();

        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.turnGetState);
        }

      

        if (TurnManager.Instance.playerUltimate.Count == 0)
        {
            //Debug.Log("궁극기 리스트가 비어 있음");
        }
        else if (TurnManager.Instance.playerUltimate[0] == null)
        {
            Debug.Log("궁극기가 삽입되지 않았음");
        }
        else if (TurnManager.Instance.playerUltimate[0].name == player.transform.gameObject.name && !TurnManager.Instance.StopTurn)
        {
            player.stateMachine.ChangeState(player.isMyUltimateTurnState);
            // 이 상태로 전환하면 루미네나와 다른 몬스터의 행동은 즉시 정지되어야 함.
            Debug.Log("궁극기 리스트에 해당 플레이어의 객체가 존재하며 통과됨");
        }



    }

}


/*
     if (TurnManager.Instance.playerUltimate[0] == null)
      {
          throw new ArgumentOutOfRangeException("궁극기가 삽입되지 않았음");


     }
     else
     {
          Debug.Log("널에서 통과됨");
          if (TurnManager.Instance.playerUltimate[0].name == player.transform.gameObject.name)
          {
              player.stateMachine.ChangeState(player.isMyUltimateTurnState);
              //만약 이상태로 넘어가면 루미네나 다른 몬스터의 행동은 즉시 정지되어야함.
              Debug.Log("통과됨");
          }
          else
          {
              Debug.Log("궁극기 리스트에 들어온게 없음");
          }
     }*/