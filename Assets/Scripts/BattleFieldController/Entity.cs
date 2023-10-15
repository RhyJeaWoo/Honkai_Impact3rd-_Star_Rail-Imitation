using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public GameObject ObjPos;//각 오브젝트가 가질 좌표

    public CinemachineVirtualCamera vircam;

    public Vector3 virCamPos; //카메라 좌표 원점
    public Vector3 moveCamPos;//카메라가 이동할 좌표

    public Quaternion virCamRot;//카메라 회전 좌표값
    //여따가 카메라 좌표 원점 저장시켜놓고.
    //그리고 플레어의 턴일때 마다 걔의 좌표로 이동 시킬거임.

    public float maxhp;//최대 체력
    public float curhp;//현재 체력

    public float atk;//현재 공격력
    public float def;//현재 방어력

    public float cureng;//현재 에너지
    public float maxeng;//최대 에너지

    public float time;//특정 스테이트에서 바로 빠져나가기 위한 쿨타임.


    public float baseSpeed; // 기초 속도
    public float buffSpeed; // 버프로 증가할 수 있는 속도
    public float flatSpeed; // 장비로 받을 수 있는 속도
    public float currentSpeed; // 현재 속도

    public float baseTurnSpeed; // 기초 행동 수치
    public float currentTurnSpeed; // 현재 행동 수치

    public bool isMyTurn; // 내 턴인지

    public bool canAct = true; //HP가 0이 되어서 활동할 수 있는지 체크


    // Start 함수에서 초기화를 수행합니다.
    protected virtual void Start()
    {
        // 최종 속도 계산
        float finalSpeed = baseSpeed * (1 + buffSpeed / 100) + flatSpeed;

        // 기초 행동 수치 계산
        baseTurnSpeed = 10000 / finalSpeed;

        // 초기화 혹은 턴 시작 시 현재 속도와 행동 수치를 설정
        currentSpeed = finalSpeed;
        currentTurnSpeed = baseTurnSpeed;

        curhp = maxhp;

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
