using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumineHitState : EnemyState
{
    private EnemyLumine lumine;
    public LumineHitState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
    }

    public override void Enter()
    {
        base.Enter();

        SoundManager.instance.SFXPlay("", lumine.anims.VoiceClip[0]);
    }

    public override void Exit()
    {
        base.Exit();
        lumine.isDamaged = false;
    }

    public override void Update()
    {
        base.Update();

        if (lumine.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && lumine.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {

            stateMachine.ChangeState(lumine.idleState);
        }
    }
}
