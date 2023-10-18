using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiSkill : PlayerSkillStrategy
{
    int skillCount = 0;
    float meiSkillDamage1 = 1.3f;
    float meiSkillDamage2 = 1.35f;
    float meiSkillDamage3 = 1.45f;

    public float ExcuteSkill(PlayerController player)
    {
       
        if (skillCount == 0) 
        {
            //여기서 공격 타입을 정할지 생각중.
            skillCount = 1;
            if (Random.Range(0f, 100f) < player.curCrt)
            {

                //데미지 공식도 여기서 생각하자.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * meiSkillDamage1;

                // critical hit!

                Debug.Log("메이 크리티컬 스킬 데미지는 : " + player.criticalDamage);

                return player.criticalDamage;
            }
            else
            {
                player.norAtkDamage = player.defaultDamage * meiSkillDamage1;

                // normal hit
                Debug.Log("메이 논 크리티컬 스킬 데미지는 : " + player.norAtkDamage);
                return player.norAtkDamage;
            }


        }
        else if(skillCount == 1) 
        {
            //여기서 공격 타입을 정할지 생각중.
            skillCount = 2;
            if (Random.Range(0f, 100f) < player.curCrt)
            {

                //데미지 공식도 여기서 생각하자.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * meiSkillDamage2;

                // critical hit!

                Debug.Log("메이 크리티컬 스킬 데미지는 : " + player.criticalDamage);
                return player.criticalDamage;
            }
            else
            {
                player.norAtkDamage = player.defaultDamage * meiSkillDamage2;
                // normal hit
                Debug.Log("메이 논 크리티컬 스킬 데미지는 : " + player.norAtkDamage);
                return player.norAtkDamage;
            }
        }
        else if(skillCount ==2)
        {
            //여기서 공격 타입을 정할지 생각중.
            skillCount = 0;
            if (Random.Range(0f, 100f) < player.curCrt)
            {
               

                //데미지 공식도 여기서 생각하자.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * meiSkillDamage3;

                // critical hit!

                Debug.Log("메이 크리티컬 스킬 데미지는 : " + player.criticalDamage);
                return player.criticalDamage;
            }
            else
            {

              
                player.norAtkDamage = player.defaultDamage * meiSkillDamage3;
                // normal hit
                Debug.Log("메이 논 크리티컬 스킬 데미지는 : " + player.norAtkDamage);
                return player.norAtkDamage;
            }

           
        }
      


     return skillCount;
      
    }



    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target) //Entity 가 아니라 클래스 Transform이었는데, 실험하려고 바꿈.
    {
     
    }
}
