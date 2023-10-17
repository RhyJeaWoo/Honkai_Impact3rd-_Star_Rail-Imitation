using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KianaSkill : PlayerSkillStrategy
{
    int skillCount = 0;
    float meiSkillDamage1 = 1.2f;
    float meiSkillDamage2 = 1.3f;
    float meiSkillDamage3 = 1.5f;

    public float ExcuteSkill(PlayerController player)
    {
        if (skillCount == 0)
        {
            //���⼭ ���� Ÿ���� ������ ������.
         
            if (Random.Range(0f, 100f) < player.curCrt)
            {
                skillCount = 1;
                //������ ���ĵ� ���⼭ ��������.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * meiSkillDamage1;

                // critical hit!

                Debug.Log("Ű�Ƴ� ũ��Ƽ�� ��ų �������� : " + player.criticalDamage);

                return player.criticalDamage;
            }
            else
            {
                skillCount = 1;
                player.norAtkDamage = player.defaultDamage * meiSkillDamage1;

                // normal hit
                Debug.Log("Ű�Ƴ� �� ũ��Ƽ�� ��ų �������� : " + player.norAtkDamage);

                return player.norAtkDamage;
            }


        }
        else if (skillCount == 1)
        {
            //���⼭ ���� Ÿ���� ������ ������.
          
            if (Random.Range(0f, 100f) < player.curCrt)
            {
                skillCount = 2;
                //������ ���ĵ� ���⼭ ��������.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * meiSkillDamage2;

                // critical hit!

                Debug.Log("Ű�Ƴ� ũ��Ƽ�� ��ų �������� : " + player.criticalDamage);

                return player.criticalDamage;
            }
            else
            {
                skillCount = 2;
                player.norAtkDamage = player.defaultDamage * meiSkillDamage2;
                // normal hit
                Debug.Log("Ű�Ƴ� �� ũ��Ƽ�� ��ų �������� : " + player.norAtkDamage);

                return player.norAtkDamage;
            }
        }
        else if (skillCount == 2)
        {
            //���⼭ ���� Ÿ���� ������ ������.

            if (Random.Range(0f, 100f) < player.curCrt)
            {

                //������ ���ĵ� ���⼭ ��������.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * meiSkillDamage3;

                // critical hit!

                Debug.Log("Ű�Ƴ� ũ��Ƽ�� ��ų �������� : " + player.criticalDamage);

                skillCount = 0;

                return player.criticalDamage;
            }
            else
            {
                player.norAtkDamage = player.defaultDamage * meiSkillDamage3;
                // normal hit
                Debug.Log("Ű�Ƴ� �� ũ��Ƽ�� ��ų �������� : " + player.norAtkDamage);

                skillCount = 0;
                return player.norAtkDamage;
            }

         

        }

       
      
        return skillCount;


    }
    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}
