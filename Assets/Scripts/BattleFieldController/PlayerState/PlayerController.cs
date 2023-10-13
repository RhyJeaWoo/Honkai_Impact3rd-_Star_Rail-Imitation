using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    #region States
    public PlayerStateMachine stateMachine { get; private set; }//스테이트 머신


    public PlayerAttackWaitState attackWaitState { get; private set; }//선택 상태

    public PlayerIdleState idleState { get; private set; }//내 턴이 아닐 경우 유지할 상태

    public PlayerWaitState waitState { get; private set; }//선택 상태가 되기 전의 상태

    public PlayerAttackState attackState { get; private set; }//공격 상태 

    public PlayerSkillState skillState { get; private set; }//스킬 시전 상태

    public PlayerUltimateState ultimateState { get; private set; } //궁을 시전한 상태, 어떤 상태에서든 사용이 가능함.

    public PlayerSkillWaitState skillwaitState { get; private set; }//턴을 받아왔을때의 상태 여기서 선택 상태로 넘어가고 그다음 공격/
                                                                    /// <summary>
                                                                    /// ,스킬 시전을 결정 할 수 있음
                                                                    /// </summary>

  


    public PlayerDeadState deadState { get; private set; }//플레이어의 체력이 0이 되었을때 진입하는 상태

    public PlayerHitState hitState { get; private set; }//피격 당했을 때의 상태.


    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();


        attackWaitState = new PlayerAttackWaitState(this, stateMachine, "AttackWait"); //내 턴에서 어떤 행동을 할지 결정하는 스테이트

        skillwaitState = new PlayerSkillWaitState(this, stateMachine, "SkillWait"); //내 턴이면 맨 처음 시작되는 스테이트

       


        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        skillState = new PlayerSkillState(this, stateMachine, "Skill"); //내 턴에서 스킬을 선택했을 경우 결정하는 스테이트


        waitState = new PlayerWaitState(this, stateMachine, "Wait");//내 턴중 선택 상태 전에 유지할 상태(턴 스테이트 이후로 연결될 상태임) ,궁극기 발동시 대기할 모션으로 사용

        ultimateState = new PlayerUltimateState(this, stateMachine, "Ultimate"); //아무 턴이나 눌렀을 시 결정되는 스테이트

      

        idleState = new PlayerIdleState(this, stateMachine, "Idle"); //내 턴이 아닐 경우 대기하는 스테이트(상대 또는 계산을 기다리는 턴)
       
        
        deadState = new PlayerDeadState(this, stateMachine, "Dead");//내 HP가 다 달았을 경우 진입할 스테이트(어떤 때라도 발동됨 내턴, 상대턴 구분 없음)
        hitState = new PlayerHitState(this, stateMachine, "Hit");//공격 당했을 경우(Idle)에서 작동됨.

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    #endregion






}
