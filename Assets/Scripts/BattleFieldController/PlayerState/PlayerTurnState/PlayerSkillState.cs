using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerState
{
    
    public PlayerSkillState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //player.toEnemyPos = TurnManager.Instance.TargetEnemyTranform;

        // 타겟 방향으로 회전함
        player.transform.LookAt(player.toEnemyPos);

        player.ExecuteSkill(this.player);//전략 패턴 실행.
        //player.isSkillOn = true;
        
    }

    public override void Exit()
    {
        base.Exit();
        //player.isSkillOn = false;
    }

    public override void Update()
    {
        base.Update();
        if (/*player.isSkillOn && */player.anim.GetCurrentAnimatorStateInfo(0).IsName("Skill") && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
           
            player.cureng += 30;

            player.isMyTurn = false;
            TurnManager.Instance.TurnEnd();
            TurnManager.Instance.target_simbol.SetActive(false);
            player.stateMachine.ChangeState(player.comeBackState);
           
        }
    }
}
