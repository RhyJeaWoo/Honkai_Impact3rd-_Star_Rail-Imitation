using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElysiaSkill : PlayerSkillStrategy
{
    public float elysiaHeal = 200;  
    public float ExcuteSkill(PlayerController player)
    {   //�����þƴ� ���� ��ų�� �ƴ�, �׷��� ������ ��������Ʈ�� �������� �����ؼ��� �ȵ�.



        player.sumHeal = (player.defaultDamage / 40) + elysiaHeal;

        Debug.Log("�����þ��� ��ų ȸ������" + player.sumHeal);



        return elysiaHeal;

       
   
    }
    
    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}
