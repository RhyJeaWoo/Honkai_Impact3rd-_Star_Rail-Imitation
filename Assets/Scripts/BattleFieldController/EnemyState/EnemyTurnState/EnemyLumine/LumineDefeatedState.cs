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

    //이 상태 일때 루미네가 받는 피해량 계수 증가 있어야함.(약점 전용 피해량 계수임)
    public override void Enter()
    {
        base.Enter();
        lumine.currentSpeed = lumine.currentSpeed + (lumine.finalSpeed * 0.25f); //이상태에 진입시 루미네의 스피드가 떨어짐.

        SoundManager.instance.SFXPlay("", lumine.anims.VoiceClip[1]);

    }

    public override void Exit()
    {
        base.Exit();

        //이 상태를 벗어날때, 데미지를 받아야하고, 동시에, 강인도를 다시 회복해야함.
        lumine.curStrongGauge = lumine.MaxStringGauge;
        lumine.isWeakness = false;


        lumine.WeaknessIncreasedDamage = 1.0f;//상태에서 벗어났기 때문에, 원래 받는 피해로 계수 다시 조정.
        //이부분은 상태가 아니라 나중에 다시 제작할때, 따로 처리해야함.

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
            lumine.WeaknessIncreasedDamage = 1.1f; //내 턴이 아닐동안 받는 데미지 10% 증가
        }
        else if(lumine.isMyTurn) 
        {
            stateMachine.ChangeState(lumine.selectState); //이 상태에서 루미네의 속도는 그래도 회복됨.
        }
    }
}
