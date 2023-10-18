using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLumine : EnemyAIController
{
    #region States


    public LumineTurnState turnState { get; private set; }//루미네가 idle에서 턴을 받았을떄 상태
    public LumineSelectState selectState { get; private set; } //루미네가 턴을 잡고 norAtk, strAtk1, strAtk2, ulitimateState 에서 공격을 고민하는 상태

    public LumineNorAtkState norAtkState { get; private set; } //루미네가 일반 공격 선택시

    public LumineStrAtkState strAtkState { get; private set; } //루미네가 패턴1 공격 선택시

    public LumineStrAtk2State strAtk2State { get; private set; } //루미네 패턴2 공격 선택시

    public LumineUlitimateState ulitimateState { get; private set; } //루미네 턴일때 루미네의 체력이 n% 낮을 경우 발동 나머지 패턴 전부 쓰지 않음.

    public LumineIdleState idleState { get; private set; } //루미네가 자기 턴이 오기 전까지 기다리는 상태

    public LumineHitState hitState { get; private set; }//루미네가 피격 당했을때 실행될 상태(이 상태는 어디서든지 발동 될 수 있음)
    //주로 idle에서 발동됨
    public LumineDeadState deadState { get; private set; }
    //루미네가 피격 당했고 죽었을때 실행될 상태(이 상태는 어디서든지 발동 될 수 있음)

    #endregion


    protected override void Awake()
    {
        base.Awake();

        turnState = new LumineTurnState(this, stateMachine, "Turn");
        selectState = new LumineSelectState(this, stateMachine, "Select",this);
        norAtkState = new LumineNorAtkState(this, stateMachine, "NorAtk", this);
        strAtkState = new LumineStrAtkState(this, stateMachine, "StrAtk", this);
        strAtk2State = new LumineStrAtk2State(this, stateMachine, "Str2Atk", this);
        ulitimateState = new LumineUlitimateState(this, stateMachine, "Ultimate", this);
        idleState = new LumineIdleState(this, stateMachine, "Idle",this);
        hitState = new LumineHitState(this, stateMachine, "Hit", this);
        deadState = new LumineDeadState(this, stateMachine, "Dead", this);
       
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

        SubscribeToPlayerDamageEvent();


    }

    protected override void Update()
    {
        base.Update();
    }
}
