using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KianaAnimPlay : AnimPlay
{
    public GameObject[] AddSkill;

    int countatk = 0; //�⺻ �����ε�, ���� ������.

    int count = 1; //�޺� ������ ��� ���

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
