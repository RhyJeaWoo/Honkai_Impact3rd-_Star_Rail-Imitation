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
        //여기서 스킬 준비 애니메이션 실행
        Debug.Log(player.name + "턴 스킬 선택 상태임");

        //player.toPlayerPos = TurnManager.Instance.PlayerTranfrom;
        
        if (player.CompareTag("Elysia")) //이 턴을 제어받은 컨트롤러가 엘리시아 라는 힐러라면,
        {
            // Elysia인 경우에만 스킬 실행
            player.stateMachine.ChangeState(player.giveBuffState);
        }
        else
        {
            // 다른 캐릭터인 경우에는 다른 상태로 전환 (공격 상태 등)
            //player.stateMachine.ChangeState(player.skillState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

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

            //player.stateMachine.ChangeState(player.giveBuffState);
        }
    }
}
