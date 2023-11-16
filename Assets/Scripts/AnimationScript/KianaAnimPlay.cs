using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KianaAnimPlay : AnimPlay
{
    public GameObject[] AddSkill;

    int countatk = 0; //기본 공격인데, 여긴 사용안함.

    int count = 1; //콤보 공격일 경우 사용

    public override void Atk()
    {
        //base.Atk();
    }

    public override void Damaged()
    {
       // base.Damaged();
    }

    public override void DestroyUltimate()
    {
      //  base.DestroyUltimate();
    }

    public override void Died()
    {
      //  base.Died();
    }

    public override void HitEffect()
    {
      //  base.HitEffect();
    }

    public override void Skill()
    {
     //  base.Skill();
    }

    public override void Ultimate()
    {
      //  base.Ultimate();
    }

    public override void UltimateWaitEffect()
    {
        //base.UltimateWaitEffect();
    }
}
