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

        TurnManager.Instance.target_simbol.SetActive(true);


        player.vircam.MoveToTopOfPrioritySubqueue();

        player.toPlayerPos = player.transform.position;


        //player.toPlayerPos = TurnManager.Instance.PlayerTranfrom;


        //Debug.Log("턴을 획득 하였음.");
        //여기서 기본공격 준비 애니메이션 실행
        //player.vircam.transform.position = player.transform.position + new Vector3(0,-0.3f,-9.7f);

        //  player.moveCamPos = player.vircam.transform.position; //먼저 가상 카메라 초기 위치 좌표를 저장함
        //   player.virCamRot = player.vircam.transform.rotation; //가상 카메라 회전 값을 저장

        // 내 오브젝트 위치값을 Vector에 저장

        //player.virCamPos = player.vircam.transform.position;//똑같이 카메라의 원래 값을 저장함.

        //버그 발생 오브젝트가 X좌표 0일경우 문제가 발생하지 않는다. 하지만,

        //

        //player.vircam



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
