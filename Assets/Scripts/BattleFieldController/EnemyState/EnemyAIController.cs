using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : Entity
{
    public EnemyStateMachine2 stateMachine { get; private set; }

    // �̺�Ʈ �ڵ鷯 �޼���
    public void HandleDamageDealt(float damage)
    {
        // �� �޼��忡�� ������ ���� �޾� ó��
        Debug.Log("�������� �޾ҽ��ϴ�: " + damage);
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
            }s
        }
        else
        {
            Debug.LogWarning("�÷��̾� ��Ʈ�ѷ��� ã�� �� �����ϴ�.");
        }

        /*if (playerController != null)
        {
            playerController.OnDamageDealt += HandleDamageDealt;
        }
        else
        {
            Debug.LogWarning("�÷��̾� ��Ʈ�ѷ��� ã�� �� �����ϴ�.");
        }*/
    }

    // �̺�Ʈ ���� ����
    public void UnsubscribeFromPlayerDamageEvent()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.OnDamageDealt -= HandleDamageDealt;
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
    }

}
