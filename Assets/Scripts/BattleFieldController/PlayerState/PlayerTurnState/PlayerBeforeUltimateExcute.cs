using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeforeUltimateExcute : PlayerState //���⼭ �ñر� ������ Space 
{
    public PlayerBeforeUltimateExcute(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        //base.Enter();���⼭ Ÿ�Ӷ��� �����ϰ� �� Ÿ�Ӷ����� ���ᰡ �ȴٸ� ����� ������
        //Ÿ�� ���� ���� ī�޶� ��ġ �ϰ� �� �̷� ������ ����...
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
