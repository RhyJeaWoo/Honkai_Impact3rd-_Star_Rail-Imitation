using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIController : Entity
{
    [Header("�� ������Ʈ�� ������ ���� ���� �����̻�")]
    public bool isWeaknessBurned = false;//ȭ�� ���� - ��
    public bool isWeaknessElectrocuted = false;//���� ���� - ����
    public bool isWeaknessFreeze = false;//���� ���� - ����
    public bool isWeaknessLaceration = false;//���� ���� - ����
    public bool is_Weakness_Be_Quantized = false;//���� ���� - ����
    public bool is_Weakness_Be_In_bondage = false;//�ӹ� ���� - ���




    public bool isAttack = false;
    public float propertieDamage = 1f; //�Ӽ� ���� ���
    public float WeaknessIncreasedDamage = 1f; //���� �����϶�, �޴� ������ �������
    public float curStrongGauge = 1f;
    public float MaxStringGauge = 1f;

    public Image strongGauge;

    public bool isWeakness = false; //������ ���ĵ� �����ΰ�...?

    public List<property> properties = new List<property>(); //Entity ���� ����� ������Ƽ�� ���� ����Ʈ

    public EnemyStateMachine2 stateMachine { get; private set; }


    // �̺�Ʈ �ڵ鷯 �޼���
    // �ִϸ��̼����� ������ ���� �߰�
    // ���� �۵� ������ ������ ����. HandleLevelDealt() --> HandleDamageDealt() �̷��� ȣ�� �ϴ� ������ �������� ������ �޾ƾߵǴµ�, 
    // �װͿ� ���� ������ ���� �����̰�, ���ÿ� ó���ϸ�, ù �����Ϳ��� �����ϴ� ������ �߻��ؼ�, ȣ�� ������ �־���.
    public void HandleDamageDealt(float damage)//
    {
         SumDamage = damage * defenseCoefficient * propertieDamage * WeaknessIncreasedDamage;

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

    public void HandPropertyDealt(property equal) //���⼭ ��������Ʈ�� �Ű������� �ޱ� ������, ���⼭ ó���� �ؾߵɵ���.
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



        if (equal == property.fire)//�ҼӼ��ΰ�?
        {
            isFireDamaged = true;
            isIceDamaged = false;
            isThunderDamaged = false;
            isPhysicalDamaged = false;
            isQuantumDanaged = false;
            isImaginary = false;
        }
        else if (equal == property.thunder)//���� �Ӽ��ΰ�?
        {
            isFireDamaged = false;
            isIceDamaged = false;
            isThunderDamaged = true;
            isPhysicalDamaged = false;
            isQuantumDanaged = false;
            isImaginary = false;
        }
        else if (equal == property.quantum)//���� �Ӽ��ΰ�?
        {
            isFireDamaged = false;
            isIceDamaged = false;
            isThunderDamaged = false;
            isPhysicalDamaged = false;
            isQuantumDanaged = true;
            isImaginary = false;
        }
        else if (equal == property.physical)//���� �Ӽ��ΰ�?
        {
            isFireDamaged = false;
            isIceDamaged = false;
            isThunderDamaged = false;
            isPhysicalDamaged = true;
            isQuantumDanaged = false;
            isImaginary = false;
        }
        else if (equal == property.ice)//���� �Ӽ��ΰ�?
        {
            isFireDamaged = false;
            isIceDamaged = true;
            isThunderDamaged = false;
            isPhysicalDamaged = false;
            isQuantumDanaged = false;
            isImaginary = false;
        }
        else if (equal == property.imaginary)//��� �Ӽ��ΰ�?
        {
            isFireDamaged = false;
            isIceDamaged = false;
            isThunderDamaged = false;
            isPhysicalDamaged = false;
            isQuantumDanaged = false;
            isImaginary = true;
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

        if(curStrongGauge <= 0)
        {
            curStrongGauge = 0;
            isWeakness = true;
        }

        //Debug.Log("���� �ǽð� ����� ���" + defenseCoefficient);
    }

    protected override void Start()
    {
        base.Start();
    }
}
