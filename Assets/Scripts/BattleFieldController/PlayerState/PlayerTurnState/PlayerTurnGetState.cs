using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnGetState : PlayerState
{
    //턴을 받으면 실행되는 상태
    public PlayerTurnGetState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skin[1].enabled = true;
        player.skin[0].enabled = true;

        TurnManager.Instance.Enemy_target_simbol.SetActive(true); //적 마크
        TurnManager.Instance.Player_target_simbol.SetActive(false);//아군 마크


        player.vircam[0].MoveToTopOfPrioritySubqueue();
       

        player.toPlayerPos = player.transform.position;


    }
        
    public override void Exit()
    {
        base.Exit();
       // player.vircam.transform.position = player.virCamPos;
      
    }

    public override void Update()
    {
        base.Update();


        //player.vircam.transform.position = player.moveCamPos + new Vector3(player.ObjPos.transform.position.x, 0, 0);//키아나에서 이걸 -2를 박아버림. 그래서 문제가됨.
       // player.vircam.transform.rotation = player.virCamRot; //회전값 대입

        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.attackWaitState);
        }
    }
}
