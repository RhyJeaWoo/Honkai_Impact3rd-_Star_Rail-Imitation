using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElysiaSkill : PlayerSkillStrategy
{
    public float ExcuteSkill(PlayerController player)
    {   //�����þƴ� ���� ��ų�� �ƴ�, �׷��� ������ ��������Ʈ�� �������� �����ؼ��� �ȵ�.

        

        Debug.Log("�����þ� ��ų�� �����, �����þƴ� ��ų�� ȸ����.");
   
        return 0;
    }
    
    public void PlayAnimation(PlayerController player)
    { }

    public void SkillToTarget(Transform target)
    { }
}
