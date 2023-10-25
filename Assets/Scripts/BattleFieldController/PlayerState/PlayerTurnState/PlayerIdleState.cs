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
        //Debug.Log(player.name + "idle 상태가 실행되었음");
        //player.skin[1].enabled = false;
        //player.skin[0].enabled = false;



    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
   
        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.turnGetState);
        }

        for(int i = 0; i < player.playerList.Count; i++) {

            if (!TurnManager.Instance.StopTurn && player.isUltimate && !player.playerList[i].isMyTurn && !player.isMyTurn) //누군가의 턴이 아니어야만 함 내턴도, 남의턴도, 그러면서 나의 궁
            {
                //여기만 건드리면 될듯?


                player.stateMachine.ChangeState(player.isMyUltimateTurnState);
                //만약 이상태로 넘어가면 루미네나 다른 몬스터의 행동은 즉시 정지되어야함.
            }
        }
      

        //여기서 만약에 내가 상태이상 상태라면은 데미지를 받고 시작하게 만들면된다/
        // ex)if(player.isMyTurn && player.isStatusEffect)로 검사를 해서, PlayerStatusEffectState머신 만들고, 그쪽에서 데미지를 받고 넘어가면 될듯.

    }

}
