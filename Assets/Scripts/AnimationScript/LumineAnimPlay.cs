using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumineAnimPlay : AnimPlay
{
    EnemyAIController controller;
    
    public override void HitEffect()
    {
        //base.HitEffect();
    }

    public override void Ultimate()
    {
        //base.Ultimate();
    }

    public override void UltimateWaitEffect()
    {
        //base.UltimateWaitEffect();
    }

    public override void Atk()
    {
        //base.Atk();

        GameObject atkeffect = Instantiate(atkEffect, TurnManager.Instance.PlayerTransForm, atkEffect.transform.rotation); //AI�� �����Ϸ��� ��ġ�� ����.

        Debug.Log("���� ����Ʈ�� �����Ǿ���.");




        Destroy(atkeffect, 3f);

    }

    public override void Skill()
    {
        //base.Skill();
    }
}

