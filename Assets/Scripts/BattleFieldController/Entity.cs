using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //델리게이트

    public delegate void DamageDealtHandler(float damage); //데미지에 관한 델리게이트로 선언
    public event DamageDealtHandler OnDamageDealt;//델리게이트

    public delegate void LevelDealtHandler(float level); //레벨에 관한 델리게이트
    public event LevelDealtHandler OnLevelDealt;

    

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public AnimPlay anims;
     
    public SkinnedMeshRenderer[] skin;

    public CinemachineVirtualCamera[] vircam;

    public Vector3 toEnemyPos = Vector3.zero; //적의 위치

    public Vector3 toPlayerPos = Vector3.zero;//내 원래 위치

    public Vector3 TargetEnemyTranform { get; set; } // TargetEnemyTranform을 속성으로 추가

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

    [Header("이 오브젝트가 가지는 속성")]
    public string s = "s";


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

    [Header("플레이어의 힐 관련 정리")]
    public float sumHeal; //힐 총합 수치

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
     */
   

    [Header("잡다한거")]

    public float time;//특정 스테이트에서 바로 빠져나가기 위한 쿨타임.

    public bool isMyTurn; // 내 턴인지를 체크하는 변수
  
    public bool isUltimate = false; //내가 궁극기 버튼을 눌렀는가에 대한 체크

    public bool IsReservingUltimate = false; //궁극기가 예약되었는가?

    public bool firstUltimate = false; //이 변수는 가장 먼저 첫번째로 도달한 애가 있는지를 체크하는 함수임.

    public bool canAct = true; //HP가 0이 되어서 활동할 수 있는지 체크


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

    protected void DamageDelegate(float damage)
    {
        OnDamageDealt?.Invoke(damage);
    }

    protected void LevelDelegate() 
    {
        OnLevelDealt?.Invoke(curLevel);
    }

}
