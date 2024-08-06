using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController player;
    protected Rigidbody2D rb;

    private string animBoolName;


    protected float stateTimer;
    protected bool triggerCalled;


    public PlayerState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }


    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
  
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;   


      

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
      
    }
}
