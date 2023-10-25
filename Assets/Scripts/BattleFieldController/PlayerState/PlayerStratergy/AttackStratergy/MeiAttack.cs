using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiAttack : PlayerAttackStrategy
{
    float attackDamage = 0.35f;
    public float ExcuteAttack(PlayerController player)
    {
        if (Random.Range(0f, 100f) < player.curCrt)
        {

            //������ ���ĵ� ���⼭ ��������.

            player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * attackDamage;

            // critical hit!

            //Debug.Log("���� ũ��Ƽ�� ��Ÿ �������� : " + player.criticalDamage);

            return player.criticalDamage;
        }
        else
        {
            player.norAtkDamage = player.defaultDamage * attackDamage;

            // normal hit
            //Debug.Log("���� �� ũ��Ƽ�� ��Ÿ �������� : " + player.norAtkDamage);

            return player.norAtkDamage;
        }

      

    }

    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}
