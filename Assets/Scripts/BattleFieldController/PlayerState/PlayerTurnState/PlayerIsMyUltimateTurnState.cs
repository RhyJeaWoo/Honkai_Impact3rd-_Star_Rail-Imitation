using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerIsMyUltimateTurnState : PlayerState
{
    //궁극기를 눌렀을때, 자기 상태를 기다리는 중
    private bool ultimateReserved = false; // 궁극기 예약 여부를 나타내는 플래그
    public PlayerIsMyUltimateTurnState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        //base.Enter();
        Debug.Log(player.name + "이 궁대기 상태에 진입하였음");
    

    }

    public override void Exit()
    {
        //base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.isUltimate)
        {

            //stateMachine.ChangeState(player.ultimateWaitState);
            stateMachine.ChangeState(player.ulimateCutSceneState);
            return;
        }

        // 다른 플레이어가 이미 턴을 잡았는지 확인
        bool otherPlayerIsTurn = false;
        foreach (PlayerController otherPlayer in TurnManager.Instance.playable)
        {
            if (otherPlayer != player && otherPlayer.isMyTurn)
            {
                otherPlayerIsTurn = true;
                Debug.Log("누군가가 턴을 잡았음");
                break;
            }
        }

        if (otherPlayerIsTurn)
        {
            // 다른 플레이어가 턴을 이미 잡았으므로 궁극기를 발동하지 않고 상태를 유지
            return;
        }

        if (!ultimateReserved && player.IsReservingUltimate)
        {
            // 궁극기를 예약하고 턴 종료 상태로 변경
            ultimateReserved = true;
            player.IsReservingUltimate = false;
            //TurnManager.Instance.TurnEnd();
        }

       

       
    }
        
       

    
}