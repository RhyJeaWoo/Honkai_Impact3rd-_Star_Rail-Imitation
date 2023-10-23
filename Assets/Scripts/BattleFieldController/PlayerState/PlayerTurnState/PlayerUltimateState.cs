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
  
        player.cureng = 0;

        player.playableDirector.enabled = true;

        player.DeligateLevel();



    }

    public override void Exit()
    {
        base.Exit();
      
        player.cureng += 5;
        player.playableDirector.enabled = false;
        time = 0;



    }

    public override void Update()
    {
        base.Update();


        if (player.playableDirector.enabled)
        {
            time += Time.deltaTime;
        }

        if (time >= player.playableDirector.duration)
        {
            stateMachine.ChangeState(player.ultimateEndState);
        
        }
      
    }

}
