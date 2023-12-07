using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLumine : EnemyAIController
{
    /*
    public delegate void EnemyDamageDealtHandler(float damage); //���� ������ �������� ���� ��������Ʈ�� ����
    public event EnemyDamageDealtHandler OnDamageDealt;//��������Ʈ

    public delegate void EnemyLevelDealtHandler(float level); //�� ������ ���� ��������Ʈ
    public event EnemyLevelDealtHandler OnLevelDealt;*/
    //���⼭ ��̳� ���� ��ũ��Ʈ�� �����.

    #region States

    public LumineDefeatedState defeatedState {  get; private set; } //���� ���� ������ ���.

    public LumineTurnState turnState { get; private set; }//��̳װ� idle���� ���� �޾����� ����
    public LumineSelectState selectState { get; private set; } //��̳װ� ���� ��� norAtk, strAtk1, strAtk2, ulitimateState ���� ������ ����ϴ� ����

    public LumineNorAtkState norAtkState { get; private set; } //��̳װ� �Ϲ� ���� ���ý�

    public LumineStrAtkState strAtkState { get; private set; } //��̳װ� ����1 ���� ���ý�

    public LumineStrAtk2State str2AtkState { get; private set; } //��̳� ����2 ���� ���ý�

    public LumineUlitimateState ulitimateState { get; private set; } //��̳� ���϶� ��̳��� ü���� n% ���� ��� �ߵ� ������ ���� ���� ���� ����.

    public LumineIdleState idleState { get; private set; } //��̳װ� �ڱ� ���� ���� ������ ��ٸ��� ����

    public LumineHitState hitState { get; private set; } //��̳װ� �ǰ� �������� ����� ����(�� ���´� ��𼭵��� �ߵ� �� �� ����)
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
        str2AtkState = new LumineStrAtk2State(this, stateMachine, "Str2Atk", this);
        ulitimateState = new LumineUlitimateState(this, stateMachine, "Ultimate", this);
        idleState = new LumineIdleState(this, stateMachine, "Idle",this);
        hitState = new LumineHitState(this, stateMachine, "Hit", this);
        deadState = new LumineDeadState(this, stateMachine, "Dead", this);
        defeatedState = new LumineDefeatedState(this, stateMachine, "Defeat", this);
       
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

        MaxStringGauge = 450f;
        curStrongGauge = MaxStringGauge;

        SubscribeToPlayerDamageEvent();

        properties.Add(property.fire);//�� ������Ʈ�� �Ӽ� ���� ����
        properties.Add(property.physical);
        properties.Add(property.thunder);

    }

    protected override void Update()
    {
        base.Update();

    
    }


}
