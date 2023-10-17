using UnityEngine;


public interface PlayerAttackStrategy
{
    void ExcuteAttack(PlayerController player);
    void PlayAnimation(PlayerController player);
    void SkillToTarget(Transform target);
}

