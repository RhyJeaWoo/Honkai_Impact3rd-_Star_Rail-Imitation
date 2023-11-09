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
        TurnManager.Instance.Enemy_target_simbol.SetActive(false); // Ȱ��ȭ
    
    }

    public override void Update()
    {
        base.Update();

        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("BuffGive")
            && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            player.CastHealSkill();

            player.cureng += 30;

            player.isMyTurn = false;
            TurnManager.Instance.TurnEnd(); //������ �ٽ� ¥�ߵȴ�.
            //TurnManager.Instance.target_simbol.SetActive(false);
            player.stateMachine.ChangeState(player.idleState);
        }


    }
}
