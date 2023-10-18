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
            //���⼭ ���� Ÿ���� ������ ������.
            skillCount = 1;
            if (Random.Range(0f, 100f) < player.curCrt)
            {

                //������ ���ĵ� ���⼭ ��������.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * meiSkillDamage1;

                // critical hit!

                Debug.Log("���� ũ��Ƽ�� ��ų �������� : " + player.criticalDamage);

                return player.criticalDamage;
            }
            else
            {
                player.norAtkDamage = player.defaultDamage * meiSkillDamage1;

                // normal hit
                Debug.Log("���� �� ũ��Ƽ�� ��ų �������� : " + player.norAtkDamage);
                return player.norAtkDamage;
            }


        }
        else if(skillCount == 1) 
        {
            //���⼭ ���� Ÿ���� ������ ������.
            skillCount = 2;
            if (Random.Range(0f, 100f) < player.curCrt)
            {

                //������ ���ĵ� ���⼭ ��������.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * meiSkillDamage2;

                // critical hit!

                Debug.Log("���� ũ��Ƽ�� ��ų �������� : " + player.criticalDamage);
                return player.criticalDamage;
            }
            else
            {
                player.norAtkDamage = player.defaultDamage * meiSkillDamage2;
                // normal hit
                Debug.Log("���� �� ũ��Ƽ�� ��ų �������� : " + player.norAtkDamage);
                return player.norAtkDamage;
            }
        }
        else if(skillCount ==2)
        {
            //���⼭ ���� Ÿ���� ������ ������.
            skillCount = 0;
            if (Random.Range(0f, 100f) < player.curCrt)
            {
               

                //������ ���ĵ� ���⼭ ��������.

                player.criticalDamage = player.defaultDamage * (1 + player.criticalPower) * meiSkillDamage3;

                // critical hit!

                Debug.Log("���� ũ��Ƽ�� ��ų �������� : " + player.criticalDamage);
                return player.criticalDamage;
            }
            else
            {

              
                player.norAtkDamage = player.defaultDamage * meiSkillDamage3;
                // normal hit
                Debug.Log("���� �� ũ��Ƽ�� ��ų �������� : " + player.norAtkDamage);
                return player.norAtkDamage;
            }

           
        }
      


     return skillCount;
      
    }



    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target) //Entity �� �ƴ϶� Ŭ���� Transform�̾��µ�, �����Ϸ��� �ٲ�.
    {
     
    }
}
