using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerStruct
{
    public string names;
    public float curLevel;
    public float maxLevel;
    public float maxhp;
    public float curhp;
    public float atk;
    public float def;
    public float cureng;
    public float maxeng;
    public float curCrt;
    public float criticalPower;
    public float baseSpeed;
    public float buffSpeed;
    public float flatSpeed;
    public float currentSpeed;
    public float finalSpeed;
    public float baseTurnSpeed;
    public float currentTurnSpeed;
    public int curSpeedText;
    public bool isBurned;
    public bool isElectrocuted;
    public bool isFreeze;
    public bool isLaceration;
    public bool is_Be_Quantized;
    public bool is_Be_In_bondage;
    public bool isStun;
    public bool isFireDamaged;
    public bool isIceDamaged;
    public bool isThunderDamaged;
    public bool isPhysicalDamaged;
    public bool isQuantumDamaged;
    public bool isImaginary;

    public PlayerStruct(Entity entity)
    {
        names = entity.names;

        curLevel = entity.curLevel;
        maxLevel = entity.maxLevel;

        maxhp = entity.maxhp;
        curhp = entity.curhp;

        atk = entity.atk;

        def = entity.def;

        cureng = entity.cureng;
        maxeng = entity.maxeng;

        curCrt = entity.curCrt;

        criticalPower = entity.criticalPower;

        baseSpeed = entity.baseSpeed;

        buffSpeed = entity.buffSpeed;

        flatSpeed = entity.flatSpeed;

        currentSpeed = entity.currentSpeed;

        finalSpeed = entity.finalSpeed;

        baseTurnSpeed = entity.baseTurnSpeed;

        currentTurnSpeed = entity.currentTurnSpeed;

        curSpeedText = entity.curSpeedText;

        isBurned = entity.isBurned;

        isElectrocuted = entity.isElectrocuted;

        isFreeze = entity.isFreeze;

        isLaceration = entity.isLaceration;

        is_Be_Quantized = entity.is_Be_Quantized;

        is_Be_In_bondage = entity.is_Be_In_bondage;

        isStun = entity.isStun;

        isFireDamaged = entity.isFireDamaged;

        isIceDamaged = entity.isIceDamaged;

        isThunderDamaged = entity.isThunderDamaged;

        isPhysicalDamaged = entity.isPhysicalDamaged;

        isQuantumDamaged = entity.isQuantumDamaged;

        isImaginary = entity.isImaginary;
    }

    public void ApplyToEntity(Entity entity)
    {
        entity.curLevel = curLevel;
        entity.maxLevel = maxLevel;
        entity.maxhp = maxhp;
        entity.curhp = curhp;
        entity.atk = atk;
        entity.def = def;
        entity.cureng = cureng;
        entity.maxeng = maxeng;
        entity.curCrt = curCrt;
        entity.criticalPower = criticalPower;
        entity.baseSpeed = baseSpeed;
        entity.buffSpeed = buffSpeed;
        entity.flatSpeed = flatSpeed;
        entity.currentSpeed = currentSpeed;
        entity.finalSpeed = finalSpeed;
        entity.baseTurnSpeed = baseTurnSpeed;
        entity.currentTurnSpeed = currentTurnSpeed;
        entity.curSpeedText = curSpeedText;
        entity.isBurned = isBurned;
        entity.isElectrocuted = isElectrocuted;
        entity.isFreeze = isFreeze;
        entity.isLaceration = isLaceration;
        entity.is_Be_Quantized = is_Be_Quantized;
        entity.is_Be_In_bondage = is_Be_In_bondage;
        entity.isStun = isStun;
        entity.isFireDamaged = isFireDamaged;
        entity.isIceDamaged = isIceDamaged;
        entity.isThunderDamaged = isThunderDamaged;
        entity.isPhysicalDamaged = isPhysicalDamaged;
        entity.isQuantumDamaged = isQuantumDamaged;
        entity.isImaginary = isImaginary;
    }

}

