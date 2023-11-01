using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateEndState : PlayerState
{
    //궁극기가 종료되고 여기서 데미지 처리할거임.
    //여기서 이펙트나옴
    
    public PlayerUltimateEndState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        //base.Enter(); 애니메이션을 쓰지 않을거기 때문에 잠금

 
        player.UltimateDamageEvent();

        player.playableDirector[0].enabled = false;

        //Debug.Log("호출되었음");


    }

    public override void Exit()
    {
        //base.Exit(); 애니메이션 안쓸거니까 잠금
        //TurnManager.Instance.ultimateQueue.Dequeue();
        TurnManager.Instance.playerUltimate.Remove(TurnManager.Instance.playerUltimate[0]);
        TurnManager.Instance.TurnEnd();
        TurnManager.Instance.UltimateEnd();
        player.isUltimate = false;

    }

    public override void Update()
    {
        base.Update();

        stateMachine.ChangeState(player.idleState);
    }
}
