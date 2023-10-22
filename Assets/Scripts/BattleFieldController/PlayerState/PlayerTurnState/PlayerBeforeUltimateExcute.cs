using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeforeUltimateExcute : PlayerState //여기서 궁극기 시전후 Space 
{
    public PlayerBeforeUltimateExcute(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        //base.Enter();여기서 타임라인 실행하고 그 타임라인이 종료가 된다면 모션이 나가고
        //타임 라인 전용 카메라 배치 하고 뭐 이런 식으로 ㅇㅇ...
    }

    public override void Exit()
    {
        //base.Exit();
    }

    public override void Update()
    {
        //base.Update();
    }
}
