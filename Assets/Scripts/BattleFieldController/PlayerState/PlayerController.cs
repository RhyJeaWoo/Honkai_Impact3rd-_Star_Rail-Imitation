using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    #region States
    public PlayerStateMachine stateMachine { get; private set; }//������Ʈ �ӽ�


    public PlayerAttackWaitState attackWaitState { get; private set; }//���� ����

    public PlayerIdleState idleState { get; private set; }//�� ���� �ƴ� ��� ������ ����

    public PlayerWaitState waitState { get; private set; }//���� ���°� �Ǳ� ���� ����

    public PlayerAttackState attackState { get; private set; }//���� ���� 

    public PlayerSkillState skillState { get; private set; }//��ų ���� ����

    public PlayerUltimateState ultimateState { get; private set; } //���� ������ ����, � ���¿����� ����� ������.

    public PlayerSkillWaitState skillwaitState { get; private set; }//���� �޾ƿ������� ���� ���⼭ ���� ���·� �Ѿ�� �״��� ����/
                                                                    /// <summary>
                                                                    /// ,��ų ������ ���� �� �� ����
                                                                    /// </summary>

  


    public PlayerDeadState deadState { get; private set; }//�÷��̾��� ü���� 0�� �Ǿ����� �����ϴ� ����

    public PlayerHitState hitState { get; private set; }//�ǰ� ������ ���� ����.


    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();


        attackWaitState = new PlayerAttackWaitState(this, stateMachine, "AttackWait"); //�� �Ͽ��� � �ൿ�� ���� �����ϴ� ������Ʈ

        skillwaitState = new PlayerSkillWaitState(this, stateMachine, "SkillWait"); //�� ���̸� �� ó�� ���۵Ǵ� ������Ʈ

       


        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        skillState = new PlayerSkillState(this, stateMachine, "Skill"); //�� �Ͽ��� ��ų�� �������� ��� �����ϴ� ������Ʈ


        waitState = new PlayerWaitState(this, stateMachine, "Wait");//�� ���� ���� ���� ���� ������ ����(�� ������Ʈ ���ķ� ����� ������) ,�ñر� �ߵ��� ����� ������� ���

        ultimateState = new PlayerUltimateState(this, stateMachine, "Ultimate"); //�ƹ� ���̳� ������ �� �����Ǵ� ������Ʈ

      

        idleState = new PlayerIdleState(this, stateMachine, "Idle"); //�� ���� �ƴ� ��� ����ϴ� ������Ʈ(��� �Ǵ� ����� ��ٸ��� ��)
       
        
        deadState = new PlayerDeadState(this, stateMachine, "Dead");//�� HP�� �� �޾��� ��� ������ ������Ʈ(� ���� �ߵ��� ����, ����� ���� ����)
        hitState = new PlayerHitState(this, stateMachine, "Hit");//���� ������ ���(Idle)���� �۵���.

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
