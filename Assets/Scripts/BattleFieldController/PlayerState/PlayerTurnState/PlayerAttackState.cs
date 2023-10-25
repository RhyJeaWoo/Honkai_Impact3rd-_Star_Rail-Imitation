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

        player.ExecuteAttack(this.player);//전략 패턴 실행.
        //player.isAtackOn = true;
    }

    public override void Exit()
    {
        base.Exit();
        //player.isAtackOn = false;


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
