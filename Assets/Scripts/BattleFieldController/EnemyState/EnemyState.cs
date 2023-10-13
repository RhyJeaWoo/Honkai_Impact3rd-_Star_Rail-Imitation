using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine2 stateMachine;
    protected EnemyAIController enemy;
    protected Rigidbody2D rb;


    private string animBoolName;


    protected bool triggerCalled;
    protected float stateTimer;

    public EnemyState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName)
    {
        this.enemy = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
    
        rb = enemy.rb;
        enemy.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }

    






}
