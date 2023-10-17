using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    // 다른 PlayerController 멤버 변수

    private PlayerSkillStrategy skillStrategy;
    private PlayerAttackStrategy attackStrategy;

    public Transform target; // 데미지를 주어야 하는 대상

    public delegate void DamageDealtHandler(float damage); //델리게이트로 선언
    public event DamageDealtHandler OnDamageDealt;//델리게이트




    #region States
    public PlayerStateMachine stateMachine { get; private set; }//스테이트 머신

    public PlayerIdleState idleState { get; private set; }//내 턴이 아닐 경우 유지할 상태

    public PlayerTurnGetState turnGetState { get; private set; }//idle에서 턴을 잡았다는 신호를 정하기 위한 상태

    public PlayerAttackWaitState attackWaitState { get; private set; }//공격 대기 상태

   
    public PlayerAttackState attackState { get; private set; }//공격 상태 

    public PlayerSkillState skillState { get; private set; }//스킬 시전 상태
    public PlayerSkillWaitState skillWaitState { get; private set; }//턴을 받아왔을때의 상태 여기서 선택 상태로 넘어가고 그다음 공격/

    public PlayerTargetToMoveSkillState targetMoveState { get; private set; }//내가 사용한 스킬이 타게팅 공격 스킬일 경우.
    public PlayerComeBackState comeBackState { get; private set; }//내가 만약 적을 향해 이동했을 경우 다시 호출할 상태(제 자리로 가야함)

    public PlayerBuffGiveState giveBuffState { get; private set; }//만약 내가 사용한 스킬이 버프계열일 경우.

    


    public PlayerUltimateState ultimateState { get; private set; } //궁을 시전한 상태, 어떤 상태에서든 사용이 가능함.


    public PlayerWaitState waitState { get; private set; }//궁극기 시전후 대기 상태로 사용 예정. 어떠한 상태에서든 개입 가능함.





    public PlayerDeadState deadState { get; private set; }//플레이어의 체력이 0이 되었을때 진입하는 상태

    public PlayerHitState hitState { get; private set; }//피격 당했을 때의 상태.


    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        turnGetState = new PlayerTurnGetState(this, stateMachine, "Idle"); //턴을 겟했지만 Idle로 쓸거임.


        attackWaitState = new PlayerAttackWaitState(this, stateMachine, "AttackWait"); //내 턴에서 어떤 행동을 할지 결정하는 스테이트

        skillWaitState = new PlayerSkillWaitState(this, stateMachine, "SkillWait"); //내 턴이면 맨 처음 시작되는 스테이트

       


        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        skillState = new PlayerSkillState(this, stateMachine, "Skill"); //내 턴에서 스킬을 선택했을 경우 결정하는 스테이트


        waitState = new PlayerWaitState(this, stateMachine, "Wait");//내 턴중 선택 상태 전에 유지할 상태(턴 스테이트 이후로 연결될 상태임) ,궁극기 발동시 대기할 모션으로 사용

        ultimateState = new PlayerUltimateState(this, stateMachine, "Ultimate"); //아무 턴이나 눌렀을 시 결정되는 스테이트

        comeBackState = new PlayerComeBackState(this, stateMachine, "ComeBack");

        idleState = new PlayerIdleState(this, stateMachine, "Idle"); //내 턴이 아닐 경우 대기하는 스테이트(상대 또는 계산을 기다리는 턴)
       
        
        deadState = new PlayerDeadState(this, stateMachine, "Dead");//내 HP가 다 달았을 경우 진입할 스테이트(어떤 때라도 발동됨 내턴, 상대턴 구분 없음)
        hitState = new PlayerHitState(this, stateMachine, "Hit");//공격 당했을 경우(Idle)에서 작동됨.


        targetMoveState = new PlayerTargetToMoveSkillState(this, stateMachine, "TargetToMove");
        giveBuffState = new PlayerBuffGiveState(this, stateMachine, "GiveBuff");

        

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



    // 스킬 전략 설정
    public void SetSkillStrategy(PlayerSkillStrategy strategy)
    {
        skillStrategy = strategy;
    }

    // 공격 전략 설정

    public void SetAttackStrategy(PlayerAttackStrategy strategy)
    {
        attackStrategy = strategy;
    }



    // 플레이어블 캐릭터의 스킬 실행시 연결될 전략
    public void ExecuteSkill(PlayerController player)
    {
        if (skillStrategy != null)
        {
            //skillStrategy.ExcuteSkill(player);

            // 스킬 중 특정 타이밍에 데미지를 주어야 할 때, target을 설정하고 애니메이션 재생
           // target = GetTarget(); // 데미지를 주어야 할 대상을 얻어옴
            //skillStrategy.PlayAnimation(this); // 애니메이션 재생
        }
    }

    public void ExecuteAttack(PlayerController player)
    {
        if(attackStrategy != null) { }
    }

    //애니메이션으로 실행
    public void SkillDamageEvent()
    {
        if (skillStrategy != null)
        {
            //skillStrategy.SkillToTarget(target);
             float skillDamage = skillStrategy.ExcuteSkill(this);


            // 데미지 이벤트 발생
            OnDamageDealt?.Invoke(skillDamage);

        }
    }

    public void AttackDamageEnvet()
    {
        if(attackStrategy != null)
        {
            attackStrategy.ExcuteAttack(this);
            //float attackDamage = 
            // 데미지 이벤트 발생
            //OnDamageDealt?.Invoke(attackDamage);
        }
    }

}
