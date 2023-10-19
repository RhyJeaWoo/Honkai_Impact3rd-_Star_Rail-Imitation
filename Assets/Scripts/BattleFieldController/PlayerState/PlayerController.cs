using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{

    #region Digine
    private PlayerSkillStrategy skillStrategy;
    private PlayerAttackStrategy attackStrategy;

    public Transform target; // �������� �־�� �ϴ� ���

    public delegate void DamageDealtHandler(float damage); //�������� ���� ��������Ʈ�� ����
    public event DamageDealtHandler OnDamageDealt;//��������Ʈ

    public delegate void LevelDealtHandler(float level); //������ ���� ��������Ʈ
    public event LevelDealtHandler OnLevelDealt;

    #endregion



    #region States
    public PlayerStateMachine stateMachine { get; private set; }//������Ʈ �ӽ�

    public PlayerIdleState idleState { get; private set; }//�� ���� �ƴ� ��� ������ ����

    public PlayerTurnGetState turnGetState { get; private set; }//idle���� ���� ��Ҵٴ� ��ȣ�� ���ϱ� ���� ����

    public PlayerAttackWaitState attackWaitState { get; private set; }//���� ��� ����

   
    public PlayerAttackState attackState { get; private set; }//���� ���� 

    public PlayerSkillState skillState { get; private set; }//��ų ���� ����
    public PlayerSkillWaitState skillWaitState { get; private set; }//���� �޾ƿ������� ���� ���⼭ ���� ���·� �Ѿ�� �״��� ����/

    public PlayerTargetToMoveState targetMoveState { get; private set; }//���� ����� ��ų�� Ÿ���� ���� ��ų�� ���.
    public PlayerComeBackState comeBackState { get; private set; }//���� ���� ���� ���� �̵����� ��� �ٽ� ȣ���� ����(�� �ڸ��� ������)

    public PlayerBuffGiveState giveBuffState { get; private set; }//���� ���� ����� ��ų�� �����迭�� ���.

    


    public PlayerUltimateState ultimateState { get; private set; } //���� ������ ����, � ���¿����� ����� ������.


    public PlayerWaitState waitState { get; private set; }//�ñر� ������ ��� ���·� ��� ����. ��� ���¿����� ���� ������.





    public PlayerDeadState deadState { get; private set; }//�÷��̾��� ü���� 0�� �Ǿ����� �����ϴ� ����

    public PlayerHitState hitState { get; private set; }//�ǰ� ������ ���� ����.


    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        turnGetState = new PlayerTurnGetState(this, stateMachine, "Idle"); //���� �������� Idle�� ������.


        attackWaitState = new PlayerAttackWaitState(this, stateMachine, "AttackWait"); //�� �Ͽ��� � �ൿ�� ���� �����ϴ� ������Ʈ

        skillWaitState = new PlayerSkillWaitState(this, stateMachine, "SkillWait"); //�� ���̸� �� ó�� ���۵Ǵ� ������Ʈ

       


        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        skillState = new PlayerSkillState(this, stateMachine, "Skill"); //�� �Ͽ��� ��ų�� �������� ��� �����ϴ� ������Ʈ


        waitState = new PlayerWaitState(this, stateMachine, "Wait");//�� ���� ���� ���� ���� ������ ����(�� ������Ʈ ���ķ� ����� ������) ,�ñر� �ߵ��� ����� ������� ���

        ultimateState = new PlayerUltimateState(this, stateMachine, "Ultimate"); //�ƹ� ���̳� ������ �� �����Ǵ� ������Ʈ

        comeBackState = new PlayerComeBackState(this, stateMachine, "ComeBack");

        idleState = new PlayerIdleState(this, stateMachine, "Idle"); //�� ���� �ƴ� ��� ����ϴ� ������Ʈ(��� �Ǵ� ����� ��ٸ��� ��)
       
        
        deadState = new PlayerDeadState(this, stateMachine, "Dead");//�� HP�� �� �޾��� ��� ������ ������Ʈ(� ���� �ߵ��� ����, ����� ���� ����)
        hitState = new PlayerHitState(this, stateMachine, "Hit");//���� ������ ���(Idle)���� �۵���.


        targetMoveState = new PlayerTargetToMoveState(this, stateMachine, "TargetToMove");
        giveBuffState = new PlayerBuffGiveState(this, stateMachine, "GiveBuff");

        

    }
    // ����� ��Ÿ���� �� ���� �����
    // 1 - (�� ���� / �� ���� + 200 + 10 * �� ����) �̴�. 


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



    // ��ų ���� ����
    // ��� 1 �����ȿ� ������ �ѹ��� ���� ��ø ������ �����ΰ�...?
    public void SetSkillStrategy(PlayerSkillStrategy strategy)
    {
        skillStrategy = strategy;
    }

    // ���� ���� ����
    public void SetAttackStrategy(PlayerAttackStrategy strategy)
    {
        attackStrategy = strategy;
    }



    // �÷��̾�� ĳ������ ��ų ����� ����� ����
    // ���̸� �����ϰ� �������� ����.
    public void ExecuteSkill(PlayerController player)
    {
        if (skillStrategy != null)
        {
        }
    }
    public void ExecuteAttack(PlayerController player)
    {
        if(attackStrategy != null) { }
    }

    //�ִϸ��̼����� ����
    public void SkillDamageEvent()
    {
        if (skillStrategy != null)
        {
            
             float skillDamage = skillStrategy.ExcuteSkill(this); //��������Ʈ�� �������� �޾ƿ� �������� ���� ������ ����
           
            // ������ �̺�Ʈ �߻�
            OnDamageDealt?.Invoke(skillDamage);


        }
    }

    public void AttackDamageEnvet()
    {
        if(attackStrategy != null)
        {
            float norAtkDamage = attackStrategy.ExcuteAttack(this); //��������Ʈ�� �������� �޾ƿ� �������� ���� ������ ����

            // ������ �̺�Ʈ �߻�
            OnDamageDealt?.Invoke(norAtkDamage);
           
        }
    }

    public void DeligateLevel()
    {
        OnLevelDealt?.Invoke(curLevel);
       
    }



    public void Heal(float healAmount)
    {
        if (curhp > 0)
        {
            curhp += healAmount;

            if (curhp > maxhp)
            {
                curhp = maxhp;
            }
        }
    }







    //////////////////////////////////////////////////////////////////////////////////////// ĳ���� ��ų ���� ������ ���� ��ũ��Ʈ /////////////////////////////////////////////////////////////////////////////////////////////////


}
