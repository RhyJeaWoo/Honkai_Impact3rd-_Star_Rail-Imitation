using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum property //강인도 시스템에 사용될 속성(캐릭터의 기본 속성과 적의 약점을 위해 사용될거임)
{
    fire, //불 
    thunder, //번개
    physical, //물리
    ice, //빙결
    quantum, //양자
    imaginary, //허수
    without_weakness//약점이 없는 상태(주로 몬스터가 사용)

    //위 상태들은 약점 격파시 특정 상태로 진입하기 위해
};

public class Entity : MonoBehaviour
{
    public Image hp;


    //델리게이트

    public delegate void DamageDealtHandler(float damage); //데미지에 관한 델리게이트로 선언
    public event DamageDealtHandler OnDamageDealt;//델리게이트

    public delegate void LevelDealtHandler(float level); //레벨에 관한 델리게이트
    public event LevelDealtHandler OnLevelDealt;

    public delegate void PropertyDealtHandler(property equal);//속성에 관한 델리게이트(내 속성을 쏴서, 적 속성하고 일치하는지 판단할거임)
    public event PropertyDealtHandler OnPropertyDealt;

    public delegate void StrongGaugeHandler(float strongGauge);
    public event StrongGaugeHandler OnStrongGaugeDealt;

    public  property character_attributes;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public AnimPlay anims; //여기서 모순 발생.
     
    public SkinnedMeshRenderer[] skin;

    public CinemachineVirtualCamera[] vircam;

    public Vector3 toEnemyPos = Vector3.zero; //적의 위치

    public Vector3 toPlayerPos = Vector3.zero;//내 원래 위치

    public Vector3 TargetEnemyTranform { get; set; } // TargetEnemyTranform을 속성으로 추가

    public GameObject hudDamageText;

    public Transform hudPos;

   



    [Header("플레이어의 스테이터스")]
    public float curLevel;//현재 레벨
    public float maxLevel;//최대 레벨

    public float maxhp;//최대 체력
    public float curhp;//현재 체력

    public float atk;//현재 공격력
    public float def;//현재 방어력

    public float cureng;//현재 에너지
    public float maxeng;//최대 에너지

    public float curCrt;//현재 치명타 수치
    public float criticalPower;//크리티컬 데미지 증가 배율 

    public float baseSpeed; // 기초 속도x
    public float buffSpeed; // 버프로 증가할 수 있는 속도
    public float flatSpeed; // 장비로 받을 수 있는 속도
    public float currentSpeed; // 현재 속도

    public float baseTurnSpeed; // 기초 행동 수치
    public float currentTurnSpeed; // 현재 행동 수치

    

    /* 속성 예정 스타레일 식이 아닌 내 마개조 버전으로 생각중.
     * 
     * 약점이 반응하는 방식
     * 내 플레이어블 캐릭터가 턴을 잡았을때, 얘가 가진 속성에 대응되는 약점을 가진 오브젝트들이 다 빛남.
     * 
     * 물리 <-> (원소) 화염 빙결 번개
     * 
     * 기계 생물 물리 허수 양자
     * 
     * 이넘 타입으로 만들면 될듯
     * 
     * 
     * 4. 저항 계수(아 안만듬 ㅡㅡ) ㅅㅂ

        (1 - 저항성 + 저항관통)

        적은 속성에 따라 다른 저항성을 보유함



        약점 속성: 0%

        일반 속성: 20%

        같은 속성: 40%  ex) 얼음몹은 얼음 저항, 화염몹은 화염 저항

        저항관통은 제레의 베어가르기, 단항의 특성
     * 
     * 
     */


    /*
    * 여기서 플레이어가 가지는 속성을 정함
    * 물, 번개, 얼음 , 허수, 양자 ,물리 만 다루려고함.
    * 일단 키아나 불
    * 메이 번개 확정
    * 엘리시아는 양자
    * 듀란달은 허수로 생각중
    * 
    * 루미네는 약점 속성 불 번개 그리고 물리만 생각중
    * 
    * 슬라임이야 그냥 불 번개 양자로 넣으면 될듯 아이콘 표현은 음... 몰것네
    * 
    */
    [Header("이 오브젝트가 가지는 속성")]
   




    [Header("플레이어의 데미지 관련 정리")]
    public float defaultDamage;//크리티컬 판정 전으로 가하는 데미지

    public float norAtkDamage;

    public float criticalDamage;

    public float skillDamage;//스킬계수데미지

    public float increasedDamage;//가하는 피해량 증가 데미지

    public float SumDamage;//내가 받은 총 데미지의 합.

    public float defenseCoefficient;//방어 계수
    public float liciveOpponentLevel;//전달 받은 상대 레벨 계수
    public float ignoredDefense;//방어 무시 받는 계수

    //public float strongGaugePower = 0;

    [Header("플레이어의 힐 관련 정리")]
    public float sumHeal; //힐 총합 수치

   
   

    [Header("잡다한거")]

    public float time;//특정 스테이트에서 바로 빠져나가기 위한 쿨타임.

    public bool isDamaged=false;//데미지를 입었는가?

    public bool isMyTurn=false; // 내 턴인지를 체크하는 변수
  
    public bool isUltimate = false; //내가 궁극기 버튼을 눌렀는가에 대한 체크

    public bool IsReservingUltimate = false; //궁극기가 예약되었는가?

    public bool firstUltimate = false; //이 변수는 가장 먼저 첫번째로 도달한 애가 있는지를 체크하는 함수임.

    public bool canAct = true; //HP가 0이 되어서 활동할 수 있는지 체크

    public bool isWeakness = false; //약점이 격파된 상태인가...?

    //이게 True를 받을 경우 새로운 상태로 진입.


    //public bool StopTurn;
    // public bool isAtackOn = false; //내가 공격 준비 상태에서 공격할 준비가 되었는지 체크
    //public bool isSkillOn = false; //내가 스킬 준비 상태에서 스킬을 사용할 준비가 되었는지 체크
    //되었는지 체크하고 넘어가는걸로

    // Start 함수에서 초기화를 수행합니다.

  

    protected virtual void Start()
    {
        skin = GetComponentsInChildren<SkinnedMeshRenderer>();

        ObjectStatCal();

    }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update 함수에서 턴 및 행동 수치를 업데이트합니다.
    protected virtual void Update()
    {
        time -= Time.deltaTime;

        hp.fillAmount = curhp / maxhp;
    }

    public void ObjectStatCal() //초기 수치 계산
    {
        // 최종 속도 계산    
        float finalSpeed = baseSpeed * (1 + buffSpeed / 100) + flatSpeed;
        // 기초 행동 수치 계산
        baseTurnSpeed = 10000 / finalSpeed;
        // 초기화 혹은 턴 시작 시 현재 속도와 행동 수치를 설정
        currentSpeed = finalSpeed;
        currentTurnSpeed = baseTurnSpeed;
        curhp = maxhp;
        //크리티컬 적용전 데미지   (공격력 * 스킬 계수) * (1 + 피해 증가 배수) 

        defaultDamage = (atk) * (1 + increasedDamage);

       
    }

    public void DamageDelegate(float damage)
    {
        OnDamageDealt?.Invoke(damage);
    }

    public void LevelDelegate() 
    {
        OnLevelDealt?.Invoke(curLevel);
    }

    public void PropertyDelegate()
    {
        OnPropertyDealt?.Invoke(character_attributes);
    }

    
    public void StrongGaugeDelegate(float strongGaugePower)
    {
        OnStrongGaugeDealt?.Invoke(strongGaugePower);
    }
    




    public void TakeDamageText(int damage)
    {
        float x = Random.Range(-1, 2);

        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPos.position + new Vector3(x, 1f,0); // 표시될 위치
        hudText.GetComponent<DamageText>().damage = damage; // 데미지 전달
        //base.TakeDamage(damage);
    }
    
}
