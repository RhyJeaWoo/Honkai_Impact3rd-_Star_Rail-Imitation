using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.transform.LookAt(player.toEnemyPos);
        TurnManager.Instance.SkillStackAdd();
            
        //player.ExecuteAttack(this.player);//전략 패턴 실행.
        //player.isAtackOn = true;

     

    }

    public override void Exit()
    {
        base.Exit();
        //player.isAtackOn = false;

        /*
        for (int i = 0; i < TurnManager.Instance.enemys.Count && i < TurnManager.Instance.EnemyInitialPosition.Length; i++)
        {
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                TurnManager.Instance.enemys[i].transform.position = TurnManager.Instance.EnemyInitialPosition[i];
            }
        }*/


        player.vircam[1].transform.position = player.vircam[0].transform.position;

        player.vircam[1].MoveToTopOfPrioritySubqueue();


    }

    public override void Update()
    {
        base.Update();
        if (/*player.isAtackOn &&*/ player.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {

            player.cureng += 20;

         
            TurnManager.Instance.Enemy_target_simbol.SetActive(false);
            player.stateMachine.ChangeState(player.comeBackState);
         
        }
    }
}
