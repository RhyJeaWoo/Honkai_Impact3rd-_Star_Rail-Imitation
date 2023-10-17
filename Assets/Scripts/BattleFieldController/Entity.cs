using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{

  
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public SkinnedMeshRenderer[] skin;

    public CinemachineVirtualCamera vircam;

    public Vector3 toEnemyPos = Vector3.zero; //적의 위치

    public Vector3 toPlayerPos = Vector3.zero;//내 원래 위치

    [Header("플레이어의 스테이터스")]
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




    [Header("플레이어의 데미지 관련 정리")]
    public float defaultDamage;//크리티컬 판정 전으로 가하는 데미지

    public float norAtkDamage;

    public float criticalDamage;

    public float skillDamage;//스킬계수데미지

    public float increasedDamage;//가하는 피해량 증가 데미지

    [Header("잡다한거")]

    public float time;//특정 스테이트에서 바로 빠져나가기 위한 쿨타임.

    public bool isMyTurn; // 내 턴인지
    public bool isAtackOn = false; //내가 공격 준비 상태에서 공격할 준비가 되었는지
    public bool isSkillOn = false; //내가 스킬 준비 상태에서 스킬을 사용할 준비가 되었는지 체크하고 넘어가는걸로

    public bool canAct = true; //HP가 0이 되어서 활동할 수 있는지 체크


    // Start 함수에서 초기화를 수행합니다.
    protected virtual void Start()
    {
        skin = GetComponentsInChildren<SkinnedMeshRenderer>();

        // 최종 속도 계산    
        float finalSpeed = baseSpeed * (1 + buffSpeed / 100) + flatSpeed;

        // 기초 행동 수치 계산
        baseTurnSpeed = 10000 / finalSpeed;

        // 초기화 혹은 턴 시작 시 현재 속도와 행동 수치를 설정
        currentSpeed = finalSpeed;
        currentTurnSpeed = baseTurnSpeed;

        curhp = maxhp;

        //크리티컬 적용전 데미지   (공격력 * 스킬 계수) * (1 + 피해 증가 배수) 

        defaultDamage = (atk ) * (1 + increasedDamage);

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


    /*


    // 다른 이벤트나 조건에 따라 턴을 시작할 때 호출합니다.
    public void StartTurn()
    {
        isMyTurn = true;
        Debug.Log(name + "'s turn started.");
    }

    // 현재 행동을 마친 후 호출합니다.
    public void EndTurn()
    {
        isMyTurn = false;
        currentTurnSpeed = baseTurnSpeed; // 기초 행동 수치로 초기화
        canAct = false;
        //Debug.Log(name + "'s turn ended.");
    }*/
}
