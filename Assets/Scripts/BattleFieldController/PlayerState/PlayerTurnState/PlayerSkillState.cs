using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerState
{


    float time = 0;

    public PlayerSkillState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        TurnManager.Instance.SkillStackUse();
        time = 0;
        if (player.CompareTag("Durandal"))
        {
            player.playableDirector[1].enabled = true;
        }
        else
        {
            base.Enter();
        }

       
        //player.toEnemyPos = TurnManager.Instance.TargetEnemyTranform;

        // 타겟 방향으로 회전함
        player.transform.LookAt(player.toEnemyPos);

        //player.ExecuteSkill(this.player);//전략 패턴 실행.
        //player.isSkillOn = true;
        
    }

    public override void Exit()
    {
        base.Exit();
        //player.isSkillOn = false;

        if(player.CompareTag("Durandal"))
        {
            player.playableDirector[1].enabled = false;
        }

      

        player.vircam[1].transform.position = player.vircam[0].transform.position;

        player.vircam[1].MoveToTopOfPrioritySubqueue();



    }

    public override void Update()
    {
        if (player.CompareTag("Durandal"))
        {
            if (player.playableDirector[1].enabled)
            {
                time += Time.deltaTime;
            }


            //player.playableDirector.duration -= Time.deltaTime;

            if (time >= player.playableDirector[1].duration)
            {

                player.playableDirector[1].Stop();
           
                stateMachine.ChangeState(player.comeBackState);

            }
        }
        else
        {
            base.Update();
            if (/*player.isSkillOn && */player.anim.GetCurrentAnimatorStateInfo(0).IsName("Skill") && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {

                player.cureng += 30;

                //player.isMyTurn = false;
                player.stateMachine.ChangeState(player.comeBackState);

            }

        }

        
    }
}
