using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BaseHero
{
    public string name; //캐릭터의 이름
    public float baseHp; //전체 기본 hp
    public float curHp;//현재 hp

    public float baseMp;//전체 기본 에너지
    public float curMp;//현재 에너지

    public int stamina;//스테미나 전투 단계를 계산할 변수로 추정
    public int intellect;//지력
    public int dexterity;//재치?
    public int agility;//민첩? 

    ////내가 만들 변수에서는 안쓸수도 있음 

}
