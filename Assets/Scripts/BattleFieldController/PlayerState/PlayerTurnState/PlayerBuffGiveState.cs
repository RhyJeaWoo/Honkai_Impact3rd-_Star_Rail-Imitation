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
        player.vircam[3].MoveToTopOfPrioritySubqueue();

        TurnManager.Instance.SkillStackUse();

    }

    public override void Exit()
    {
        base.Exit();
        TurnManager.Instance.Enemy_target_simbol.SetActive(true); // 활성화
        

        for (int i = 0; i < TurnManager.Instance.enemys.Count && i < TurnManager.Instance.EnemyInitialPosition.Length; i++)
        {
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                TurnManager.Instance.enemys[i].transform.position = TurnManager.Instance.EnemyInitialPosition[i];
            }
        }


        for (int i = 0; i < TurnManager.Instance.playable.Count; i++)
        {
            if (!TurnManager.Instance.playable[i].isMyTurn) //만약 내 턴이 아닌 플레이어블들이 있다면.
            {
                TurnManager.Instance.playable[i].skin[0].enabled = false;
                TurnManager.Instance.playable[i].skin[1].enabled = false; //그 턴에 한해 비화성화

            }
        }

    }

    public override void Update()
    {
        base.Update();

        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("BuffGive")
            && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            player.CastHealSkill();

            player.cureng += 30;

            //player.isMyTurn = false;
            //TurnManager.Instance.TurnEnd(); //로직을 다시 짜야된다.
            //TurnManager.Instance.target_simbol.SetActive(false);
            player.stateMachine.ChangeState(player.turnEndState); //여기서 끝내지 말고, 누가 힐을 받았는지 카메라 전환이 필요함.

        }


    }
}
