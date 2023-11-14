using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiUltimate : PlayerUltimateStrategy
{
    float ultimate_Coefficient = 4f; //�ñر� ���
    public float ExecuteUltimate(PlayerController player)
    {
        if (Random.Range(0f, 100f) < player.curCrt)
        {
            player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * ultimate_Coefficient;

            Debug.Log("���� �õ����� : " + player.criticalDamage);

            return player.criticalDamage;
        }
        else

        {
            player.norAtkDamage = player.defaultDamage * ultimate_Coefficient;

            return player.norAtkDamage;
        }
          
    }
}
