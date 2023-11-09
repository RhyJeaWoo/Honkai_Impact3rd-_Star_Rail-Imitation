using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerUltimateState : PlayerState
{
   
    float time = 0;

    public PlayerUltimateState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        time = 0;

        player.cureng = 0;


        player.playableDirector[0].enabled = true;

        player.LevelDelegate();



    }

    public override void Exit()
    {
        base.Exit();
      
        player.cureng += 5;
      
        time = 0;



    }

    public override void Update()
    {
        base.Update();


        if (player.playableDirector[0].enabled)
        {
            time += Time.deltaTime;
        }


        //player.playableDirector.duration -= Time.deltaTime;




        if (time >= player.playableDirector[0].duration)
        {

            player.playableDirector[0].Stop();
            stateMachine.ChangeState(player.ultimateEndState);
        
        }
      
    }

}
