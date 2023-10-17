using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurandalAttack : PlayerAttackStrategy
{
    float attackDamage = 0.7f;
    public void ExcuteAttack(PlayerController player)
    {
        if (Random.Range(0f, 100f) < player.curCrt)
        {

            //������ ���ĵ� ���⼭ ��������.

            player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * attackDamage;

            // critical hit!

            Debug.Log("���� ũ��Ƽ�� ��ų �������� : " + player.criticalDamage);
        }
        else
        {
            player.norAtkDamage = player.defaultDamage * attackDamage;

            // normal hit
            Debug.Log("���� �� ũ��Ƽ�� ��ų �������� : " + player.defaultDamage);
        }

    }

    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}

