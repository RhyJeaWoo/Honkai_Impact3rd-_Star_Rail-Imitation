using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIController : Entity
{
    [Header("이 오브젝트가 가지는 약점 격파 상태이상")]
    public bool isWeaknessBurned = false;//화상 상태 - 불
    public bool isWeaknessElectrocuted = false;//감전 상태 - 번개
    public bool isWeaknessFreeze = false;//빙결 상태 - 빙결
    public bool isWeaknessLaceration = false;//열상 상태 - 물리
    public bool is_Weakness_Be_Quantized = false;//얽힘 상태 - 양자
    public bool is_Weakness_Be_In_bondage = false;//속박 상태 - 허수




    public bool isAttack = false;
    public float propertieDamage = 1f; //속성 저항 계수
    public float WeaknessIncreasedDamage = 1f; //격파 상태일때, 받는 데미지 증가계수
    public float curStrongGauge = 1f;
    public float MaxStringGauge = 1f;

    public Image strongGauge;

    public bool isWeakness = false; //약점이 격파된 상태인가...?

    public List<property> properties = new List<property>(); //Entity 위에 선언된 프로퍼티에 넣을 리스트

    public EnemyStateMachine2 stateMachine { get; private set; }


    // 이벤트 핸들러 메서드
    // 애니메이션으로 보내게 설계 했고
    // 현재 작동 순서는 다음과 같음. HandleLevelDealt() --> HandleDamageDealt() 이렇게 호출 하는 이유는 공격자의 레벨을 받아야되는데, 
    // 그것에 대한 정보가 없기 때문이고, 동시에 처리하면, 첫 데이터에는 누락하는 현상이 발생해서, 호출 순서를 주었음.
    public void HandleDamageDealt(float damage)//
    {
         SumDamage = damage * defenseCoefficient * propertieDamage * WeaknessIncreasedDamage;

         curhp = curhp - SumDamage;

        TakeDamageText((int)SumDamage);

      // 이 메서드에서 데미지 값을 받아 처리
      // Debug.Log("데미지를 전달 받았습니다!" + damage);
      // Debug.Log("현재 방어율 계수 : " + defenseCoefficient);
      // Debug.Log("현재 레벨" + curLevel);
        
      //  Debug.Log("데미지를 받았습니다: " + SumDamage);
      //  Debug.Log("현재 hp : " + curhp);
 
    }


    public void HandleLevelDealt(float level) 
    {
        
        Debug.Log("전달 받은 Level은 " + level);

        liciveOpponentLevel = level;
    }

    public void HandPropertyDealt(property equal) //여기서 델리게이트를 매개변수로 받기 때문에, 여기서 처리를 해야될듯함.
    {
        Debug.Log("전달 받은 속성 : " + equal);

        for(int i = 0; i < properties.Count; ++i) 
        {
            if (properties[i] == equal)
            {
                propertieDamage = 1f;
                Debug.Log("속성이 일치함");

                break;
            }
            else
            {
                propertieDamage = 0.8f;
                Debug.Log("속성이 일치하지 않음");
            }
        }



        if (equal == property.fire)//불속성인가?
        {
            isFireDamaged = true;
            isIceDamaged = false;
            isThunderDamaged = false;
            isPhysicalDamaged = false;
            isQuantumDanaged = false;
            isImaginary = false;
        }
        else if (equal == property.thunder)//번개 속성인가?
        {
            isFireDamaged = false;
            isIceDamaged = false;
            isThunderDamaged = true;
            isPhysicalDamaged = false;
            isQuantumDanaged = false;
            isImaginary = false;
        }
        else if (equal == property.quantum)//양자 속성인가?
        {
            isFireDamaged = false;
            isIceDamaged = false;
            isThunderDamaged = false;
            isPhysicalDamaged = false;
            isQuantumDanaged = true;
            isImaginary = false;
        }
        else if (equal == property.physical)//물리 속성인가?
        {
            isFireDamaged = false;
            isIceDamaged = false;
            isThunderDamaged = false;
            isPhysicalDamaged = true;
            isQuantumDanaged = false;
            isImaginary = false;
        }
        else if (equal == property.ice)//빙결 속성인가?
        {
            isFireDamaged = false;
            isIceDamaged = true;
            isThunderDamaged = false;
            isPhysicalDamaged = false;
            isQuantumDanaged = false;
            isImaginary = false;
        }
        else if (equal == property.imaginary)//허수 속성인가?
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


    // 이벤트 구독 
    public void SubscribeToPlayerDamageEvent() //플레이어에게 전달 받을것을 구독하는거
    {
  
        PlayerController[] playerControllers = FindObjectsOfType<PlayerController>(); // 모든 플레이어 컨트롤러를 찾습니다.

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
        //최종 방어계수 = ((현재 레벨 * 100) + 100) / (((전달 받은 상대 레벨 * 10) +200 )  +((현재 레벨 * 10 ) + 200));
        defenseCoefficient = ((curLevel * 10) + 200) / (((liciveOpponentLevel * 10) + 200) + ((curLevel * 10) + 200));

        strongGauge.fillAmount = curStrongGauge / MaxStringGauge;

        if(curStrongGauge <= 0)
        {
            curStrongGauge = 0;
            isWeakness = true;
        }

        //Debug.Log("현재 실시간 방어율 계수" + defenseCoefficient);
    }

    protected override void Start()
    {
        base.Start();
    }
}
