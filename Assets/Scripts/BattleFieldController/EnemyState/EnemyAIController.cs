using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIController : Entity
{
    public bool isAttack = false;
    public float propertieDamage = 1f; //�Ӽ� ���� ���
    public float curStrongGauge = 1f;
    public float MaxStringGauge = 1f;

    public Image strongGauge;

    public List<property> properties = new List<property>(); //Entity ���� ����� ������Ƽ�� ���� ����Ʈ

    public EnemyStateMachine2 stateMachine { get; private set; }


    // �̺�Ʈ �ڵ鷯 �޼���
    // �ִϸ��̼����� ������ ���� �߰�
    // ���� �۵� ������ ������ ����. HandleLevelDealt() --> HandleDamageDealt() �̷��� ȣ�� �ϴ� ������ �������� ������ �޾ƾߵǴµ�, 
    // �װͿ� ���� ������ ���� �����̰�, ���ÿ� ó���ϸ�, ù �����Ϳ��� �����ϴ� ������ �߻��ؼ�, ȣ�� ������ �־���.
    public void HandleDamageDealt(float damage)//
    {
         SumDamage = damage * defenseCoefficient * propertieDamage;

         curhp = curhp - SumDamage;

        TakeDamageText((int)SumDamage);

      // �� �޼��忡�� ������ ���� �޾� ó��
      // Debug.Log("�������� ���� �޾ҽ��ϴ�!" + damage);
      // Debug.Log("���� ����� ��� : " + defenseCoefficient);
      // Debug.Log("���� ����" + curLevel);
        
      //  Debug.Log("�������� �޾ҽ��ϴ�: " + SumDamage);
      //  Debug.Log("���� hp : " + curhp);
 
    }


    public void HandleLevelDealt(float level) 
    {
        
        Debug.Log("���� ���� Level�� " + level);

        liciveOpponentLevel = level;
    }

    public void HandPropertyDealt(property equal)
    {
        Debug.Log("���� ���� �Ӽ� : " + equal);

        for(int i = 0; i < properties.Count; ++i) 
        {
            if (properties[i] == equal)
            {
                propertieDamage = 1f;
                Debug.Log("�Ӽ��� ��ġ��");
                break;
            }
            else
            {
                propertieDamage = 0.8f;
                Debug.Log("�Ӽ��� ��ġ���� ����");
            }
        }

    }

    public void HandStrongGaugeDealt(float strongGauge)
    {
        curStrongGauge -= strongGauge;
    }


    // �̺�Ʈ ���� 
    public void SubscribeToPlayerDamageEvent() //�÷��̾�� ���� �������� �����ϴ°�
    {
  
        PlayerController[] playerControllers = FindObjectsOfType<PlayerController>(); // ��� �÷��̾� ��Ʈ�ѷ��� ã���ϴ�.

        if (playerControllers != null)
        {
            foreach (PlayerController playerController in playerControllers)
            {
                playerController.OnDamageDealt += HandleDamageDealt;
                playerController.OnLevelDealt += HandleLevelDealt;
                playerController.OnPropertyDealt += HandPropertyDealt;
                playerController.OnStrongGaugeDealt += HandStrongGaugeDealt;
            }
        }
        else
        {
            Debug.LogWarning("�÷��̾� ��Ʈ�ѷ��� ã�� �� �����ϴ�.");
        }

    }

    // �̺�Ʈ ���� ����
    public void UnsubscribeFromPlayerDamageEvent()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.OnDamageDealt -= HandleDamageDealt;
            playerController.OnLevelDealt -= HandleLevelDealt;
            playerController.OnPropertyDealt -= HandPropertyDealt;
            playerController.OnStrongGaugeDealt -= HandStrongGaugeDealt;
        }


    }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine2();

    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        //���� ����� = ((���� ���� * 100) + 100) / (((���� ���� ��� ���� * 10) +200 )  +((���� ���� * 10 ) + 200));
        defenseCoefficient = ((curLevel * 10) + 200) / (((liciveOpponentLevel * 10) + 200) + ((curLevel * 10) + 200));

        strongGauge.fillAmount = curStrongGauge / MaxStringGauge;

        //Debug.Log("���� �ǽð� ����� ���" + defenseCoefficient);
    }

    protected override void Start()
    {
        base.Start();
    }
}
