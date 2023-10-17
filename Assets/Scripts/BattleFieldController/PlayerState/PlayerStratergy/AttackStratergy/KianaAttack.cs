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

            //������ ���ĵ� ���⼭ ��������.

            player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * attackDamage;

            // critical hit!

            Debug.Log("Ű�Ƴ� ũ��Ƽ�� ��Ÿ �������� : " + player.criticalDamage);
        }
        else
        {
            player.norAtkDamage = player.defaultDamage * attackDamage;

            // normal hit
            Debug.Log("Ű�Ƴ� �� ũ��Ƽ�� ��Ÿ �������� : " + player.defaultDamage);
        }

    }

    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}
