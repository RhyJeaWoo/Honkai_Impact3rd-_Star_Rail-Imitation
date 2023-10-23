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
        base.Enter();//
        Debug.Log(player.anim.GetCurrentAnimatorStateInfo(0));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        //base.Update();
        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("BeforeUltimate") && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            stateMachine.ChangeState(player.ultimateState);
        }
    }
}
