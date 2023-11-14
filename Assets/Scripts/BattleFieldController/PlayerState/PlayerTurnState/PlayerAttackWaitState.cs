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

        //여기서 심볼 타겟 다시 잡기.
        StartVoice();

    }

    private void StartVoice()
    {
        if (player.CompareTag("Mei"))
        {
            SoundManager.instance.SFXPlay("TurnStart", player.playerSound[0]);
        }
        else if (player.CompareTag("Kiana"))
        {
            SoundManager.instance.SFXPlay("TurnStart", player.playerSound[0]);
        }
        else if (player.CompareTag("Elysia"))
        {
            SoundManager.instance.SFXPlay("TurnStart", player.playerSound[0]);
        }
        else if (player.CompareTag("Durandal"))
        {
            SoundManager.instance.SFXPlay("TurnStart", player.playerSound[0]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();

        //TurnManager.Instance.ChangeEnemyTarget();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //여기서 공격 모션 준비 idle 실행
            //확정으로 키 입력시 작동
            player.stateMachine.ChangeState(player.targetMoveState);
          
        
        }

        if (Input.GetKeyDown(KeyCode.E) && TurnManager.Instance.SkillStack > 0)
        {
            //키변경시 스킬 준비 모션으로 이동
            
            player.stateMachine.ChangeState(player.skillWaitState);

      
        }else if(TurnManager.Instance.SkillStack == 0 && Input.GetKeyDown(KeyCode.E)) 
        { 
            Debug.Log("스킬 포인트가 모자랍니다."); 
        }
        

    }
}
