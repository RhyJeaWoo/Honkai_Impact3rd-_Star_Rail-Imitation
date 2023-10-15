using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackWaitState : PlayerState
{

    //턴을 받으면 가장 먼저 시작할 상태임
    public PlayerAttackWaitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(player.name + "턴 선택(일반공격) 상태임");
        TurnManager.Instance.target_simbol.SetActive(true);
        //여기서 기본공격 준비 애니메이션 실행
        //player.vircam.transform.position = player.transform.position + new Vector3(0,-0.3f,-9.7f);
        player.virCamPos = player.vircam.transform.position; //먼저 버츄얼 카메라 초기 위치 좌표를 저장함
        player.virCamRot = player.vircam.transform.rotation; //카메라 회전 값을 저장

        player.ObjPos = player.transform.position;


        //여기서 카메라 조정을 다시 해야될 거 같음. 버그가 있음.
        //그리고 일단 캐릭터들 완성부터 해야될거 같음.

       //player.vircam


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.vircam.transform.position = player.virCamPos + new Vector3(player.ObjPos.x, 0,0);
        player.vircam.transform.rotation = player.virCamRot;

       //TurnManager.Instance.WhoisTurn(player.gameObject);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //여기서 공격 모션 준비 idle 실행
            //확정으로 키 입력시 작동
            player.stateMachine.ChangeState(player.attackState);
            //Debug.Log("Q가 눌렸음");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //키변경시 스킬 준비 모션으로 이동
            player.stateMachine.ChangeState(player.skillwaitState);
            //Debug.Log("E가 눌렸음");
        }

    }
}
