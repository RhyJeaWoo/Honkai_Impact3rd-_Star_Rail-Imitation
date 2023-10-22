using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : Entity
{

    public EnemyStateMachine2 stateMachine { get; private set; }

    // 이벤트 핸들러 메서드
    // 애니메이션으로 보내게 설계 했고
    // 현재 작동 순서는 다음과 같음. HandleLevelDealt() --> HandleDamageDealt() 이렇게 호출 하는 이유는 공격자의 레벨을 받아야되는데, 
    // 그것에 대한 정보가 없기 때문이고, 동시에 처리하면, 첫 데이터에는 누락하는 현상이 발생해서, 호출 순서를 주었음.
    public void HandleDamageDealt(float damage)//
    {
         SumDamage = damage * defenseCoefficient;

         curhp = curhp - SumDamage;

        // 이 메서드에서 데미지 값을 받아 처리
        Debug.Log("데미지를 전달 받았습니다!" + damage);
        Debug.Log("현재 방어율 계수 : " + defenseCoefficient);
        Debug.Log("데미지를 받았습니다: " + SumDamage);
        Debug.Log("현재 hp : " + curhp);
 
    }
    public void HandleLevelDealt(float level) 
    {
        
        Debug.Log("전달 받은 Level은 " + level);

        liciveOpponentLevel = level;
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
                playerController.OnLevelDealt += HandleLevelDealt;
            }
        }
        else
        {
            Debug.LogWarning("플레이어 컨트롤러를 찾을 수 없습니다.");
        }

    }

    // 이벤트 구독 해제
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
        //최종 방어계수 = ((현재 레벨 * 100) + 100) / (((전달 받은 상대 레벨 * 10) +200 )  +((현재 레벨 * 10 ) + 200));
        defenseCoefficient = ((curLevel * 10) + 200) / (((liciveOpponentLevel * 10) + 200) + ((curLevel * 10) + 200));
    }

    protected override void Start()
    {
        base.Start();
    }

  

}
