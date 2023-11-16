using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWhereGiveBuffState : PlayerState
{
    public PlayerWhereGiveBuffState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        TurnManager.Instance.Player_target_simbol.SetActive(true); // 활성화
        TurnManager.Instance.Enemy_target_simbol.SetActive(false);


        for (int i = 0; i < TurnManager.Instance.playable.Count; i++)
        {
            if (!TurnManager.Instance.playable[i].isMyTurn) //만약 내 턴이 아닌 플레이어블들이 있다면.
            {
                TurnManager.Instance.playable[i].skin[0].enabled = true;
                TurnManager.Instance.playable[i].skin[1].enabled = true; //그 턴에 한해 비화성화

            }
        }

        Debug.Log("버프를 줄 수 있는 상태에 진입하였음.");

        player.vircam[2].MoveToTopOfPrioritySubqueue();
        //player.CastDefensiveSkill();


        // Elysia인 경우에만 스킬 실행
    }

    public override void Exit()
    {
        base.Exit();

        TurnManager.Instance.Player_target_simbol.SetActive(false); // 활성화
    }

    public override void Update()
    {
        base.Update();
        TurnManager.Instance.ChangePlayerTarget();

        // Debug.Log("target_simbol 좌표 :" + TurnManager.Instance.target_simbol.transform.position);





        if (Input.GetKeyDown(KeyCode.E))
        {

            player.stateMachine.ChangeState(player.buffgiveState);


        }else if (Input.GetKeyDown(KeyCode.Q))
        {
            player.stateMachine.ChangeState(player.turnGetState);
        }
    }

   
}
