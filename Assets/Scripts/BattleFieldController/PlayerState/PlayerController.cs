using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerController : Entity
{


    public List<PlayerController> playerList = new List<PlayerController>(new PlayerController[4]);
    //���� ����� �ƴ� �׳� �ϸŴ����� ���� �޾ƿͼ� �װɷ� ó���ϴ°� ���ƺ���.


    //�� �������� �ٶ� �� �ִ� �÷��̾� ����Ʈ
    public PlayableDirector[] playableDirector; //������ ��ų�̳�, �ñر⸦ Ÿ�Ӷ��ο� ����.

    #region Design Patterns

    


    //����
    private PlayerSkillStrategy skillStrategy;
    private PlayerAttackStrategy attackStrategy;
    private PlayerUltimateStrategy ultimateStrategy;

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




    public PlayerIsMyUltimateTurnState isMyUltimateTurnState { get; private set; }

    public PlayerUltimateState ultimateState { get; private set; } //���� ������ ����, � ���¿����� ����� ������, ���⼭ �߻���

    public PlayerBeforeUltimateExcute beforeUltimateExcute { get; private set; } //���⼭ Ÿ�Ӷ��� ����


    public PlayerUltimateWaitState ultimateWaitState { get; private set; }//�ñر� ������ ��� ���·� ��� ����. ��� ���¿����� ���� ������.

    public PlayerUltimateEndState ultimateEndState { get; private set; }// �ñر� ������ ������ ����





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
        //////////////////////////////////////////////////

        isMyUltimateTurnState = new PlayerIsMyUltimateTurnState(this, stateMachine, "isMyUltimateTurn");

        ultimateWaitState = new PlayerUltimateWaitState(this, stateMachine, "UltimateWait");//�� ���� ���� ���� ���� ������ ����(�� ������Ʈ ���ķ� ����� ������) ,�ñر� �ߵ��� ����� ������� ���

        beforeUltimateExcute = new PlayerBeforeUltimateExcute(this, stateMachine, "BeforeUltimate"); //�� ����Ǳ� �ٷ� �� ���

        ultimateState = new PlayerUltimateState(this, stateMachine, "Ultimate"); //�ñر� ��� �ߵ�

        ultimateEndState = new PlayerUltimateEndState(this, stateMachine, "UltimateEnd"); //���⼭�� ī�޶� ���� ������� ������. �׸��� ����ɶ�, ���⼭ ó�� �Ұ���.

        /////////////////////////////////////////////////////

        comeBackState = new PlayerComeBackState(this, stateMachine, "ComeBack");

        idleState = new PlayerIdleState(this, stateMachine, "Idle"); //�� ���� �ƴ� ��� ����ϴ� ������Ʈ(��� �Ǵ� ����� ��ٸ��� ��)


        deadState = new PlayerDeadState(this, stateMachine, "Dead");//�� HP�� �� �޾��� ��� ������ ������Ʈ(� ���� �ߵ��� ����, ����� ���� ����)
        hitState = new PlayerHitState(this, stateMachine, "Hit");//���� ������ ���(Idle)���� �۵���.


        targetMoveState = new PlayerTargetToMoveState(this, stateMachine, "TargetToMove"); //��ġ�� �ִ� ���� ���� ������


        buffgiveState = new PlayerBuffGiveState(this, stateMachine, "BuffGive"); //����(��)�� �ִ� ����
        whereGiveBuffState = new PlayerWhereGiveBuffState(this, stateMachine, "WhereGiveBuff");

       

    }

    #endregion


    // ����� ��Ÿ���� �� ���� �����
    // 1 - (�� ���� / �� ���� + 200 + 10 * �� ����) �̴�. 


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);

        // TurnManager ��ũ��Ʈ�κ��� healTarget �迭�� �����ͼ� playerList ����Ʈ�� ����
        playerList.AddRange(TurnManager.Instance.healTarget.Select(transform => transform.GetComponent<PlayerController>()));
        //Debug.Log(TurnManager.Instance.healTarget.Length);
        
        ListSort(); //�ѹ� ���������� ���� ����. 

    
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        if(maxeng < cureng)
        {
            cureng = maxeng;
        }

       // Debug.Log(stateMachine.currentState);
      
    }
  

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

    public void SetUltimateStratergy(PlayerUltimateStrategy strategy)
    {
        ultimateStrategy = strategy;
    }



    // �÷��̾�� ĳ������ ��ų ����� ����� ����
    // ���̸� �����ϰ� �������� ����.
    /*
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
     

    public void ExecuteUltimate(PlayerController player)
    {
        if(skillStrategy != null) 
        {
            
        }
    }*/

    //�ִϸ��̼����� ���� , ���ڵ� �ٽ��ѹ� �ٵ��� �ɰ� ����, ������ �׳� �� ��������Ʈ�� ���� �ֵ����� �� ������ �ڵ尡 �Ǵµ�,
    //���� �����̸� �׳� ������ �Ǳ���. �ٵ�, �ϴ� �����̰ų�, ���� �����ؼ� �����°ſ� �־, �� �κ��� ���� ���� �ʳ� ����.
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

    public void UltimateDamageEvent()
    {
        if(skillStrategy != null)
        {
            float ultimateDamage = ultimateStrategy.ExecuteUltimate(this);

            //������ �̺�Ʈ �߻�
            OnDamageDealt?.Invoke(ultimateDamage);
        }
    }

    public void DeligateLevel()
    {
        OnLevelDealt?.Invoke(curLevel); //������ ���� �ؾ� ��� ����� ������.

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

  

    public void HandleUltimateReservations()
    {
        // �ñر� ���� ó�� ���� �߰�
        // ...
        IsReservingUltimate = true; //�� ĳ���Ͱ� ���� �Ǿ��°�?
        Debug.Log(name + "��(��) �ñر⸦ �����߽��ϴ�.");
    }

}
