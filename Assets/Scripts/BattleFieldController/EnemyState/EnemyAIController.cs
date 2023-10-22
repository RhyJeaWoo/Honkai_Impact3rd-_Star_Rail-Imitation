using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : Entity
{

    public EnemyStateMachine2 stateMachine { get; private set; }

    // �̺�Ʈ �ڵ鷯 �޼���
    // �ִϸ��̼����� ������ ���� �߰�
    // ���� �۵� ������ ������ ����. HandleLevelDealt() --> HandleDamageDealt() �̷��� ȣ�� �ϴ� ������ �������� ������ �޾ƾߵǴµ�, 
    // �װͿ� ���� ������ ���� �����̰�, ���ÿ� ó���ϸ�, ù �����Ϳ��� �����ϴ� ������ �߻��ؼ�, ȣ�� ������ �־���.
    public void HandleDamageDealt(float damage)//
    {
         SumDamage = damage * defenseCoefficient;

         curhp = curhp - SumDamage;

        // �� �޼��忡�� ������ ���� �޾� ó��
        Debug.Log("�������� ���� �޾ҽ��ϴ�!" + damage);
        Debug.Log("���� ����� ��� : " + defenseCoefficient);
        Debug.Log("�������� �޾ҽ��ϴ�: " + SumDamage);
        Debug.Log("���� hp : " + curhp);
 
    }
    public void HandleLevelDealt(float level) 
    {
        
        Debug.Log("���� ���� Level�� " + level);

        liciveOpponentLevel = level;
    }


    // �̺�Ʈ ����
    public void SubscribeToPlayerDamageEvent()
    {
      //  PlayerController[] playerController = FindObjectsOfType<PlayerController>(); // Ȥ�� �ٸ� ������� �÷��̾� ��Ʈ�ѷ��� ã���ϴ�.

        PlayerController[] playerControllers = FindObjectsOfType<PlayerController>(); // ��� �÷��̾� ��Ʈ�ѷ��� ã���ϴ�.

        if (playerControllers != null)
        {
            foreach (PlayerController playerController in playerControllers)
            {
                playerController.OnDamageDealt += HandleDamageDealt;
                playerController.OnLevelDealt += HandleLevelDealt;
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
    }

    protected override void Start()
    {
        base.Start();
    }

  

}
