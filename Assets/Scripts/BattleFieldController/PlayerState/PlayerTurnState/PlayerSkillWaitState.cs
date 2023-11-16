using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillWaitState : PlayerState
{
    public PlayerSkillWaitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {


        base.Enter();

        SoundManager.instance.SFXPlay("", player.PlayerVoice[1]);




        Debug.Log(player.name + "턴 스킬 선택 상태임");

       

    }

    public override void Exit()
    {
        base.Exit();
      
    }

    public override void Update()
    {
        base.Update();

        //여기서 스킬 준비 애니메이션 실행
        if (player.CompareTag("Elysia")) //이 턴을 제어받은 컨트롤러가 엘리시아 라는 힐러라면,
        {

            player.stateMachine.ChangeState(player.whereGiveBuffState);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //여기서 공격 모션 준비 idle 실행
                player.stateMachine.ChangeState(player.attackWaitState);
                //다시 누를경우 원래 모션으로 이동
            }



            if (Input.GetKeyDown(KeyCode.E))
            {
                //확정 키 버튼 입력시 작동
                player.stateMachine.ChangeState(player.targetMoveState);
            }
      
        }

     
    }
}
