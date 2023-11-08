using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController player;

    protected Rigidbody2D rb;

   // protected float xInput;
   // protected float yInput;
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
        //rb = player.rb;
        //triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;   


        //  player.anim.SetFloat("yVelocity", rb.velocity.y);
       // Debug.Log("현재 이 플레이어 이름 " + player.gameObject.name);
       // Debug.Log("지금 현재 상태" + stateMachine.currentState);

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
      
    }
}
