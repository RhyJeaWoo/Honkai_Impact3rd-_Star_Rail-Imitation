using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KianaAttack : PlayerAttackStrategy
{
    float attackDamage = 0.7f;
    public void ExcuteAttack(PlayerController player)
    {
        if (Random.Range(0f, 100f) < player.curCrt)
        {

            //데미지 공식도 여기서 생각하자.

            player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * attackDamage;

            // critical hit!

            Debug.Log("키아나 크리티컬 평타 데미지는 : " + player.criticalDamage);
        }
        else
        {
            player.norAtkDamage = player.defaultDamage * attackDamage;

            // normal hit
            Debug.Log("키아나 논 크리티컬 평타 데미지는 : " + player.defaultDamage);
        }

    }

    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}
