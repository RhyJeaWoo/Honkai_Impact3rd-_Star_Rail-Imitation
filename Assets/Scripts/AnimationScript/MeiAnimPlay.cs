using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiAnimPlay : AnimPlay
{
    int countatk = 0;

    int count = 1;

    private GameObject ultimateWaiteffect; //직관적으로 삭제하기 힘들다면, 이 방법으로 채택.

    public override void Skill() //메이 전용 값임
    {
        GameObject skilleffect = Instantiate(skillEffect, BladeHitPos.transform.position, skillEffect.transform.rotation);


        // 회전 각도를 설정할 때, Y축을 중심으로 회전하도록 설정
        //float rotationAngle = count * 45f; // 예시: 45도 간격으로 회전
        Quaternion rotation = Quaternion.Euler(180f, 180f, 45f);

        if (count == 1)
        {
            rotation = Quaternion.Euler(180f, 180f, 45f);

            // 스킬 이펙트에 회전 적용
            skillEffect.transform.rotation = rotation;
            SoundManager.instance.SFXPlay("", VoiceClip[2]);
            count = 2;
        }
        else  if (count == 2)
        {
            // 회전 각도를 설정할 때, Y축을 중심으로 회전하도록 설정
            //rotationAngle = count * 45f; // 예시: 45도 간격으로 회전
            rotation = Quaternion.Euler(0f, 0f, 45f);

            // 스킬 이펙트에 회전 적용
            skillEffect.transform.rotation = rotation;
            count = 3;
            SoundManager.instance.SFXPlay("", VoiceClip[3]);
        }
        else if (count == 3)
        {
            // 회전 각도를 설정할 때, Y축을 중심으로 회전하도록 설정
            // rotationAngle = count * 45f; // 예시: 45도 간격으로 회전
            rotation = Quaternion.Euler(180f, 180f, 45f);

            // 스킬 이펙트에 회전 적용
            skillEffect.transform.rotation = rotation;

            SoundManager.instance.SFXPlay("", VoiceClip[4]);


            count = 1;
        }

        Destroy(skilleffect, 3f);
    }


    public override void Atk() //일단 메이 전용 값으로 바꿀거임.
    {

        // 회전 각도를 설정할 때, Y축을 중심으로 회전하도록 설정
        //float rotationAngle = count * 45f; // 예시: 45도 간격으로 회전
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        if (countatk == 0)
        {
            rotation = Quaternion.Euler(0f, 0f, 45f);

            // 스킬 이펙트에 회전 적용
            atkEffect.transform.rotation = rotation;
            SoundManager.instance.SFXPlay("", VoiceClip[0]);
            countatk = 1;
        }
        else
        {

            rotation = Quaternion.Euler(0f, 0f, -135f);

            // 스킬 이펙트에 회전 적용
            atkEffect.transform.rotation = rotation;
            SoundManager.instance.SFXPlay("", VoiceClip[1]);
            countatk = 0;
        }

        GameObject atkeffect = Instantiate(atkEffect, BladeHitPos.transform.position, atkEffect.transform.rotation);

        Destroy(atkeffect, 3f);
    }

    public override void Ultimate()
    {
        /*GameObject UltimateEffect =*/ Instantiate(ultimateEffect, transform.position, transform.rotation);
        // Destroy(ultimateeffect, 3f);
    }

    public override void UltimateWaitEffect()
    {
        ultimateWaiteffect = Instantiate(ultimateWaitEffect, transform.position, ultimateWaitEffect.transform.rotation);
       // Debug.Log("생성되었음");
       // Destroy(ultimateWaiteffect, 6f);
    }

    public override void HitEffect()
    {
        GameObject hiteffect = Instantiate(hitEffect, BladeHitPos.transform.position, Quaternion.Euler(BladeHitPos.transform.position)); //이게 검 위치에서 생성 시킬거임. 
        Destroy(hiteffect, 1f);

    }

    public override void DestroyUltimate()
    {
        if (ultimateWaiteffect != null)
        {
            Destroy(ultimateWaiteffect);
            //Debug.Log("자식에 있는 이 함수가 실행 되었음");
        }
    }

    public override void Damaged()
    {
        SoundManager.instance.SFXPlay("", VoiceClip[5]); //피격 당했을때,
    }
    
    public override void Died()
    {
        SoundManager.instance.SFXPlay("", VoiceClip[6]);//죽었을때 호출
    }
   

}
