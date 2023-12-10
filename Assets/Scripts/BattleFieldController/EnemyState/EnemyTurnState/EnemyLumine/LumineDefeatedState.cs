using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumineDefeatedState : EnemyState
{
    private EnemyLumine lumine;
    public LumineDefeatedState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
    }

    //�� ���� �϶� ��̳װ� �޴� ���ط� ��� ���� �־����.(���� ���� ���ط� �����)
    public override void Enter()
    {
        base.Enter();
        lumine.currentSpeed = lumine.currentSpeed + (lumine.finalSpeed * 0.25f); //�̻��¿� ���Խ� ��̳��� ���ǵ尡 ������.

        SoundManager.instance.SFXPlay("", lumine.anims.VoiceClip[1]);

    }

    public override void Exit()
    {
        base.Exit();

        //�� ���¸� �����, �������� �޾ƾ��ϰ�, ���ÿ�, ���ε��� �ٽ� ȸ���ؾ���.
        lumine.curStrongGauge = lumine.MaxStringGauge;
        lumine.isWeakness = false;


        lumine.WeaknessIncreasedDamage = 1.0f;//���¿��� ����� ������, ���� �޴� ���ط� ��� �ٽ� ����.
        //�̺κ��� ���°� �ƴ϶� ���߿� �ٽ� �����Ҷ�, ���� ó���ؾ���.

        if(lumine.isWeaknessBurned)
        {
            lumine.isWeaknessBurned = false;
        }
        else if(lumine.isWeaknessElectrocuted)
        {
            lumine.isWeaknessElectrocuted = false;
        }
        else if(lumine.isWeaknessLaceration)
        {
            lumine.isWeaknessLaceration = false;
        }

        
    }

    public override void Update()
    {
        base.Update();

        if(!lumine.isMyTurn)
        {
            lumine.WeaknessIncreasedDamage = 1.1f; //�� ���� �ƴҵ��� �޴� ������ 10% ����
        }
        else if(lumine.isMyTurn) 
        {
            stateMachine.ChangeState(lumine.selectState); //�� ���¿��� ��̳��� �ӵ��� �׷��� ȸ����.
        }
    }
}
