using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerController : Entity
{

    public List<PlayerController> playerList = new List<PlayerController>();
    //�� �������� �ٶ� �� �ִ� �÷��̾� ����Ʈ




    #region Design Patterns

    //����
    private PlayerSkillStrategy skillStrategy;
    private PlayerAttackStrategy attackStrategy;

    //��������Ʈ

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

    public PlayerWhereGiveBuffState whereGiveBuffState { get; private set; } //�ϴ� ���� �������� ���� �ٰ���
    public PlayerBuffGiveState buffgiveState { get; private set; }// ���� �������� ���� ����




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


        targetMoveState = new PlayerTargetToMoveState(this, stateMachine, "TargetToMove"); //��ġ�� �ִ� ���� ���� ������


        buffgiveState = new PlayerBuffGiveState(this, stateMachine, "BuffGive"); //����(��)�� �ִ� ����
        whereGiveBuffState = new PlayerWhereGiveBuffState(this, stateMachine, "WhereGiveBuff");



    }

    // ����� ��Ÿ���� �� ���� �����
    // 1 - (�� ���� / �� ���� + 200 + 10 * �� ����) �̴�. 


    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

        // TurnManager ��ũ��Ʈ�κ��� healTarget �迭�� �����ͼ� playerList ����Ʈ�� ����
        playerList.AddRange(TurnManager.Instance.healTarget.Select(transform => transform.GetComponent<PlayerController>()));

        ListSort();

     
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        //Debug.Log(playerList[0].name);
        //Debug.Log(playerList[1].name);
        //Debug.Log(playerList[2].name);
        //Debug.Log(healTarget); //null
    }
    #endregion


    public void ListSort()
    {
        List<PlayerController> sortedPlayers = new List<PlayerController>(playerList);

        //���ϴ� ������� ����
        sortedPlayers.Sort((player1, player2) => player1.transform.position.x.CompareTo(player2.transform.position.x));

        //���ĵ� ����Ʈ�� playerble ����Ʈ�� �Ҵ�
        playerList = sortedPlayers;
    }


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
        if (attackStrategy != null) { }
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
        if (attackStrategy != null)
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


    /* ========================================================������� ���賻�� ============================================ */




    public void Heal(float healAmount)
    {
        if (curhp > 0)
        {
            //TurnManager.Instance.healTarget
            curhp += healAmount;

            if (curhp > maxhp)
            {
                curhp = maxhp;
            }
        }
    }
    public void CastHealSkill()
    {
        if (playerList.Count > 0)
        {
            float heal = skillStrategy.ExcuteSkill(this);


          
                if (TurnManager.Instance.targetPlayerName == playerList[0].name)
                {
                    playerList[0].Heal(heal);
                    Debug.Log(playerList[0].name + "��(��) ���� " + heal + " ��ŭ �޾ҽ��ϴ�.");

                }
                else if (TurnManager.Instance.targetPlayerName == playerList[1].name)
                {
                    playerList[1].Heal(heal);
                Debug.Log(playerList[0].name + "��(��) ���� " + heal + " ��ŭ �޾ҽ��ϴ�.");
            }
                else if (TurnManager.Instance.targetPlayerName == playerList[2].name)
                {
                    playerList[2].Heal(heal);
                Debug.Log(playerList[0].name + "��(��) ���� " + heal + " ��ŭ �޾ҽ��ϴ�.");
            }
                else
                {
                    Debug.Log("���� ��ġ �ϴ°� ����.");
                }


        }
        else
        {
            Debug.LogWarning("Heal target is not set.");
        }
    }



}
