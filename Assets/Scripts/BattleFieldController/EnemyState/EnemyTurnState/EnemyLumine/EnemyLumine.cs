using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLumine : EnemyAIController
{

    public delegate void EnemyDamageDealtHandler(float damage); //���� ������ �������� ���� ��������Ʈ�� ����
    public event EnemyDamageDealtHandler OnDamageDealt;//��������Ʈ

    public delegate void EnemyLevelDealtHandler(float level); //�� ������ ���� ��������Ʈ
    public event EnemyLevelDealtHandler OnLevelDealt;
    //���⼭ ��̳� ���� ��ũ��Ʈ�� �����.

    #region States


    public LumineTurnState turnState { get; private set; }//��̳װ� idle���� ���� �޾����� ����
    public LumineSelectState selectState { get; private set; } //��̳װ� ���� ��� norAtk, strAtk1, strAtk2, ulitimateState ���� ������ ����ϴ� ����

    public LumineNorAtkState norAtkState { get; private set; } //��̳װ� �Ϲ� ���� ���ý�

    public LumineStrAtkState strAtkState { get; private set; } //��̳װ� ����1 ���� ���ý�

    public LumineStrAtk2State strAtk2State { get; private set; } //��̳� ����2 ���� ���ý�

    public LumineUlitimateState ulitimateState { get; private set; } //��̳� ���϶� ��̳��� ü���� n% ���� ��� �ߵ� ������ ���� ���� ���� ����.

    public LumineIdleState idleState { get; private set; } //��̳װ� �ڱ� ���� ���� ������ ��ٸ��� ����

    public LumineHitState hitState { get; private set; }//��̳װ� �ǰ� �������� ����� ����(�� ���´� ��𼭵��� �ߵ� �� �� ����)
    //�ַ� idle���� �ߵ���
    public LumineDeadState deadState { get; private set; }
    //��̳װ� �ǰ� ���߰� �׾����� ����� ����(�� ���´� ��𼭵��� �ߵ� �� �� ����)

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

    public void deliveryNorDamage()
    {
        //���⼭ �� ������Ʈ�� ������ ���¿� ���� �ٸ� �������� ���� ���� �ؾߵ�.

        //float lumineDamage = ��̳װ� ����� ������

        //OnDamageDealt?.Invoke(damage); �������� ������.
    }

    public void deliveryLevel()
    {
        OnLevelDealt?.Invoke(curLevel);
    }

}
