using UnityEngine;


public interface PlayerAttackStrategy
{
    float ExcuteAttack(PlayerController player);
    float ExcuteAtkStrongGaugePower(PlayerController player);
 
}

