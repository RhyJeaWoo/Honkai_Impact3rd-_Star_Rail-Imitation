using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeforeUltimateExcute : PlayerState //여기서 애니메이션 체크
{
    public PlayerBeforeUltimateExcute(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        if (player.CompareTag("Kiana") || player.CompareTag("Elysia") || player.CompareTag("Durandal")) 
        { 
           
        }
        else
        {
            base.Enter();
        }
        Debug.Log(player.anim.GetCurrentAnimatorStateInfo(0));
    }

    public override void Exit()
    {
        if (player.CompareTag("Kiana") || player.CompareTag("Elysia") || player.CompareTag("Durandal"))
        {

        }
        else
          base.Exit();
    }

    public override void Update()
    {
        //base.Update();

        if (player.CompareTag("Kiana") || player.CompareTag("Elysia") || player.CompareTag("Durandal"))
        {
            stateMachine.ChangeState(player.ultimateState);
        }
        else
        {

            if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("BeforeUltimate") && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                stateMachine.ChangeState(player.ultimateState);
            }
        }


    }
}
