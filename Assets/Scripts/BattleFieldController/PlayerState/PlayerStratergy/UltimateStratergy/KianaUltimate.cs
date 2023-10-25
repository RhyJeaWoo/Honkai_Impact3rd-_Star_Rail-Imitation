using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KianaUltimate : PlayerUltimateStrategy
{
    float ultimate_Coefficient = 3.4f;
    public float ExecuteUltimate(PlayerController player)
    {
        if (Random.Range(0f, 100f) < player.curCrt)
        {
            player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * ultimate_Coefficient;

            return player.criticalDamage;
        }
        else

        {
            player.norAtkDamage = player.defaultDamage * ultimate_Coefficient;

            return player.norAtkDamage;
        }

    }
}
