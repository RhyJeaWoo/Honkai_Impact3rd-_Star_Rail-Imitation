using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineStoryboard;

public class PlayerBuffGiveState : PlayerState
{
    public PlayerBuffGiveState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        TurnManager.Instance.target_simbol.SetActive(true); // 활성화

        Debug.Log("버프를 줄 수 있는 상태에 진입하였음.");

        player.vircam[1].MoveToTopOfPrioritySubqueue();
        //player.CastDefensiveSkill();

      
        // Elysia인 경우에만 스킬 실행

    }

    public override void Exit()
    {
        base.Exit();
        TurnManager.Instance.target_simbol.SetActive(false); // 활성화
    }

    public override void Update()
    {
        base.Update();

        TurnManager.Instance.ChangePlayerTarget();

       // Debug.Log("target_simbol 좌표 :" + TurnManager.Instance.target_simbol.transform.position);

       

     

        if (Input.GetKeyDown(KeyCode.E))
        {


            //Debug.Log("키눈 눌렸음");

            player.CastHealSkill();

        }
    }
}
