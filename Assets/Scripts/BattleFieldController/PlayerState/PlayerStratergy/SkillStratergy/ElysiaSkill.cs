using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElysiaSkill : PlayerSkillStrategy
{
    public float ExcuteSkill(PlayerController player)
    {
        Debug.Log("�����þ� ��ų�� �����.");
        return 0;
    }
    
    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}
