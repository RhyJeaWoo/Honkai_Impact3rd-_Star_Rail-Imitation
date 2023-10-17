using UnityEngine;


public interface PlayerSkillStrategy
{
    float ExcuteSkill(PlayerController player);
    void PlayAnimation(PlayerController player);
    void SkillToTarget(Transform target);
}



