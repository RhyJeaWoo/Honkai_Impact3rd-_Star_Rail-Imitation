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

        GameObject atkeffect = Instantiate(atkEffect, BladeHitPos.transform.position, atkEffect.transform.rotation);


        Destroy(atkeffect, 3f);

    }

    public override void Skill()
    {
        //base.Skill();
    }
}

