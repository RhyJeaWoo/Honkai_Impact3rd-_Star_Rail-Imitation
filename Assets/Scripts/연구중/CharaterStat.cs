using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterStat : MonoBehaviour
{
    //캐릭터들의 스텟을 정리한 것임.

    [Header("CharaterDefaultStat")]
    //여기서 캐릭터 들 플레이어 적 관게 없이 상속을 통해 사용할 것임.
    //기본 6개의 스텟만 상속함
    public int hp;//캐릭터 hp
    public int Atk;//캐릭터 공격력
    public int def;//캐릭터 방어력
    public int AtkSpeed;//공격 속도
    public int Critical;//치명타 확률
    public int CriticalPower;//치명타 피해량 증가

    //계수를 여기서 설정할 것인가?에 대해 여기서는 하지 않을듯.
    //캐릭터마다 따로 사용해야되기 떄문. 대신 상속 받아서
    // 그 캐릭터들 마다 따로 구현할 것임.

    #region Components

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }



    #endregion

    protected virtual void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }



}
