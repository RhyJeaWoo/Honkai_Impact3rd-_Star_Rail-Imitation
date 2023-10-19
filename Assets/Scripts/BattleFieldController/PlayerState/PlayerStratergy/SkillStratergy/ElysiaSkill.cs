using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElysiaSkill : PlayerSkillStrategy
{
    public float ExcuteSkill(PlayerController player)
    {   //엘리시아는 공격 스킬이 아님, 그렇기 때문에 델리게이트로 데미지를 전달해서는 안됨.

        

        Debug.Log("엘리시아 스킬이 실행됨, 엘리시아는 스킬이 회복임.");
   
        return 0;
    }
    
    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}
