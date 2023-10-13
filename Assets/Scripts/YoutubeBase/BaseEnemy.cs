using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy 
{

    public string name;//적 개체 이름

    public enum Type
    {
        GRASS,
        FIRE,
        WATER,
        ELECTRIC

    }

    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        SUPERRARE
    }

    public Type EnemyType;
    public Rarity rarity;

    public float baseHp; //전체 기본 hp
    public float curHp;//현재 hp

    public float baseMp;//전체 기본 에너지
    public float curMp;//현재 에너지

    public float baseATK;
    public float curATK;
    public float baseDef;
    public float curDef;



}

