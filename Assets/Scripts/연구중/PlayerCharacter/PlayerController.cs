using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    #region States
    public PlayerStateMachine stateMachine { get; private set; }//������Ʈ �ӽ�

    public PlayerSelectState selectState { get; private set; }//���� ����

    public PlayerSkillState skillState { get; private set; }//��ų ���� ����

    public PlayerUltimateState ultimateState { get; private set; } //���� ������ ����, � ���¿����� ����� ������.

    public PlayerTurnState turnState { get; private set; }//���� �޾ƿ������� ���� ���⼭ ���� ���·� �Ѿ�� �״��� ����/,��ų ������ ���� �� �� ����

    public PlayerIdleState idleState { get; private set; }//�� ���� �ƴ� ��� ������ ����

    public PlayerWaitState waitState { get; private set; }//���� ���°� �Ǳ� ���� ����

    public PlayerAttackState attackState { get; private set; }//���� ���� 

    public PlayerDeadState deadState { get; private set; }//�÷��̾��� ü���� 0�� �Ǿ����� �����ϴ� ����

    public PlayerHitState hitState { get; private set; }//�ǰ� ������ ���� ����.


    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        turnState = new PlayerTurnState(this, stateMachine, "Turn"); //�� ���̸� �� ó�� ���۵Ǵ� ������Ʈ
        waitState = new PlayerWaitState(this, stateMachine, "Wait");//�� ���� ���� ���� ���� ������ ����(�� ������Ʈ ���ķ� ����� ������)

        selectState = new PlayerSelectState(this, stateMachine,"Selcet"); //�� �Ͽ��� � �ൿ�� ���� �����ϴ� ������Ʈ

        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        skillState = new PlayerSkillState(this, stateMachine, "Skill"); //�� �Ͽ��� ��ų�� �������� ��� �����ϴ� ������Ʈ
        ultimateState = new PlayerUltimateState(this, stateMachine, "Ultimate"); //�ƹ� ���̳� ������ �� �����Ǵ� ������Ʈ

      

        idleState = new PlayerIdleState(this, stateMachine, "Idel"); //�� ���� �ƴ� ��� ����ϴ� ������Ʈ(��� ��)
       
        
        deadState = new PlayerDeadState(this, stateMachine, "Dead");//�� HP�� �� �޾��� ��� ������ ������Ʈ(� ���� �ߵ��� ����, ����� ���� ����)
        hitState = new PlayerHitState(this, stateMachine, "Hit");//���� ������ ���(Idle)���� �۵���.

    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    #endregion






}
