using UnityEngine;


public interface PlayerAttackStrategy
{
    float ExcuteAttack(PlayerController player);
    void PlayAnimation(PlayerController player);
    void SkillToTarget(Transform target);
}

