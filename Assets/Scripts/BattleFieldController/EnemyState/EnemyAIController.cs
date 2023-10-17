using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : Entity
{
    public EnemyStateMachine2 stateMachine { get; private set; }

    // 이벤트 핸들러 메서드
    public void HandleDamageDealt(float damage)
    {
        // 이 메서드에서 데미지 값을 받아 처리
        Debug.Log("데미지를 받았습니다: " + damage);
    }

    // 이벤트 구독
    public void SubscribeToPlayerDamageEvent()
    {
      //  PlayerController[] playerController = FindObjectsOfType<PlayerController>(); // 혹은 다른 방식으로 플레이어 컨트롤러를 찾습니다.

        PlayerController[] playerControllers = FindObjectsOfType<PlayerController>(); // 모든 플레이어 컨트롤러를 찾습니다.

        if (playerControllers != null)
        {
            foreach (PlayerController playerController in playerControllers)
            {
                playerController.OnDamageDealt += HandleDamageDealt;
            }s
        }
        else
        {
            Debug.LogWarning("플레이어 컨트롤러를 찾을 수 없습니다.");
        }

        /*if (playerController != null)
        {
            playerController.OnDamageDealt += HandleDamageDealt;
        }
        else
        {
            Debug.LogWarning("플레이어 컨트롤러를 찾을 수 없습니다.");
        }*/
    }

    // 이벤트 구독 해제
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
