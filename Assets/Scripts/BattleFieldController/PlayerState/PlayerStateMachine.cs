using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState {get; private set; }
    public PlayerState prevState { get; private set; }//이전 상태를 저장하기 위한 용도;
    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState) //상태를 바꾸는 곳
    {
        currentState.Exit();
        prevState = currentState; //이전 상태를 저장
        currentState = _newState; //그리고 현재 상태를 넣음.
        currentState.Enter();
    }
}
