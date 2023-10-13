using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : Entity
{
    public EnemyStateMachine2 stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine2();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

}
