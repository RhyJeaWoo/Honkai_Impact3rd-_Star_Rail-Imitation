using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy 
{

    public string name;//�� ��ü �̸�

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

    public float baseHp; //��ü �⺻ hp
    public float curHp;//���� hp

    public float baseMp;//��ü �⺻ ������
    public float curMp;//���� ������

    public float baseATK;
    public float curATK;
    public float baseDef;
    public float curDef;



}

