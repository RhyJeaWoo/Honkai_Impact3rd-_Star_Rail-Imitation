using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurandalSkill : PlayerSkillStrategy
{
    int skillCount = 0;
    float durandalSkillDamage1 = 1.15f;
    float durandalSkillDamage2 = 1.2f;
    float durandalSkillDamage3 = 1.25f;
    float durandalSkillDamage4 = 1.25f;
    float durandalSkillDamage5 = 1.25f;
    float durandalSkillDamage6 = 1.5f;

    public float ExcuteSkill(PlayerController player)
    {

        if (skillCount == 0)
        {
            //여기서 공격 타입을 정할지 생각중.
            skillCount = 1;
            if (Random.Range(0f, 100f) < player.curCrt)
            {

                //데미지 공식도 여기서 생각하자.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * durandalSkillDamage1;

                // critical hit!

                Debug.Log("듀란달 크리티컬 스킬 데미지는 : " + player.criticalDamage);

                return player.criticalDamage;
            }
            else
            {
                player.norAtkDamage = player.defaultDamage * durandalSkillDamage1;

                // normal hit
                Debug.Log("듀란달 논 크리티컬 스킬 데미지는 : " + player.norAtkDamage);
                return player.norAtkDamage;
            }


        }
        else if (skillCount == 1)
        {
            //여기서 공격 타입을 정할지 생각중.
            skillCount = 2;
            if (Random.Range(0f, 100f) < player.curCrt)
            {

                //데미지 공식도 여기서 생각하자.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * durandalSkillDamage2;

                // critical hit!

                Debug.Log("듀란달 크리티컬 스킬 데미지는 : " + player.criticalDamage);
                return player.criticalDamage;
            }
            else
            {
                player.norAtkDamage = player.defaultDamage * durandalSkillDamage2;
                // normal hit
                Debug.Log("듀란달 논 크리티컬 스킬 데미지는 : " + player.norAtkDamage);
                return player.norAtkDamage;
            }
        }
        else if (skillCount == 2)
        {
            //여기서 공격 타입을 정할지 생각중.
            skillCount = 0;
            if (Random.Range(0f, 100f) < player.curCrt)
            {


                //데미지 공식도 여기서 생각하자.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * durandalSkillDamage3;

                // critical hit!

                Debug.Log("듀란달 크리티컬 스킬 데미지는 : " + player.criticalDamage);
                return player.criticalDamage;
            }
            else
            {


                player.norAtkDamage = player.defaultDamage * durandalSkillDamage3;
                // normal hit
                Debug.Log("듀란달 논 크리티컬 스킬 데미지는 : " + player.norAtkDamage);
                return player.norAtkDamage;
            }


        }
        else if (skillCount == 3)
        {
            //여기서 공격 타입을 정할지 생각중.
            
            if (Random.Range(0f, 100f) < player.curCrt)
            {


                //데미지 공식도 여기서 생각하자.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * durandalSkillDamage4;

                // critical hit!

                Debug.Log("듀란달 크리티컬 스킬 데미지는 : " + player.criticalDamage);
                return player.criticalDamage;
            }
            else
            {


                player.norAtkDamage = player.defaultDamage * durandalSkillDamage4;
                // normal hit
                Debug.Log("듀란달 논 크리티컬 스킬 데미지는 : " + player.norAtkDamage);
                return player.norAtkDamage;
            }


        }
        else if (skillCount == 4)
        {
            //여기서 공격 타입을 정할지 생각중.
          
            if (Random.Range(0f, 100f) < player.curCrt)
            {


                //데미지 공식도 여기서 생각하자.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * durandalSkillDamage5;

                // critical hit!

                Debug.Log("듀란달 크리티컬 스킬 데미지는 : " + player.criticalDamage);
                return player.criticalDamage;
            }
            else
            {


                player.norAtkDamage = player.defaultDamage * durandalSkillDamage5
                    ;
                // normal hit
                Debug.Log("듀란달 논 크리티컬 스킬 데미지는 : " + player.norAtkDamage);
                return player.norAtkDamage;
            }


        }
        else if (skillCount == 5)
        {
            //여기서 공격 타입을 정할지 생각중.
            skillCount = 0;
            if (Random.Range(0f, 100f) < player.curCrt)
            {


                //데미지 공식도 여기서 생각하자.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * durandalSkillDamage6;

                // critical hit!

                Debug.Log("듀란달 크리티컬 스킬 데미지는 : " + player.criticalDamage);
                return player.criticalDamage;
            }
            else
            {


                player.norAtkDamage = player.defaultDamage * durandalSkillDamage6
                    ;
                // normal hit
                Debug.Log("듀란달 논 크리티컬 스킬 데미지는 : " + player.norAtkDamage);
                return player.norAtkDamage;
            }


        }



        return skillCount;

    }

    public void PlayVoice(PlayerController player)
    {

    }
}
