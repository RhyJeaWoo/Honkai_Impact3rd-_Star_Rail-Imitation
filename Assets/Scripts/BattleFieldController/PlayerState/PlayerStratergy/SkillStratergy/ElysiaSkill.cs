using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElysiaSkill : PlayerSkillStrategy
{
    public float elysiaHeal = 200;  
    public float ExcuteSkill(PlayerController player)
    {   //엘리시아는 공격 스킬이 아님, 그렇기 때문에 델리게이트로 데미지를 전달해서는 안됨.



        player.sumHeal = (player.defaultDamage / 40) + elysiaHeal;

        Debug.Log("엘리시아의 스킬 회복량은" + player.sumHeal);



        return elysiaHeal;

       
   
    }
    
    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}
