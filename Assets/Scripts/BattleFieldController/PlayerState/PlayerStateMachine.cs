using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState {get; private set; }
    public PlayerState prevState { get; private set; }//���� ���¸� �����ϱ� ���� �뵵;
    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        prevState = currentState;
        currentState = _newState;
        currentState.Enter();
    }
}
