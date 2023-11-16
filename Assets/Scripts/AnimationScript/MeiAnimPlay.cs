using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiAnimPlay : AnimPlay
{
    int countatk = 0;

    int count = 1;

    private GameObject ultimateWaiteffect; //���������� �����ϱ� ����ٸ�, �� ������� ä��.

    public override void Skill() //���� ���� ����
    {
        GameObject skilleffect = Instantiate(skillEffect, BladeHitPos.transform.position, skillEffect.transform.rotation);


        // ȸ�� ������ ������ ��, Y���� �߽����� ȸ���ϵ��� ����
        //float rotationAngle = count * 45f; // ����: 45�� �������� ȸ��
        Quaternion rotation = Quaternion.Euler(180f, 180f, 45f);

        if (count == 1)
        {
            rotation = Quaternion.Euler(180f, 180f, 45f);

            // ��ų ����Ʈ�� ȸ�� ����
            skillEffect.transform.rotation = rotation;
            SoundManager.instance.SFXPlay("", VoiceClip[2]);
            count = 2;
        }
        else  if (count == 2)
        {
            // ȸ�� ������ ������ ��, Y���� �߽����� ȸ���ϵ��� ����
            //rotationAngle = count * 45f; // ����: 45�� �������� ȸ��
            rotation = Quaternion.Euler(0f, 0f, 45f);

            // ��ų ����Ʈ�� ȸ�� ����
            skillEffect.transform.rotation = rotation;
            count = 3;
            SoundManager.instance.SFXPlay("", VoiceClip[3]);
        }
        else if (count == 3)
        {
            // ȸ�� ������ ������ ��, Y���� �߽����� ȸ���ϵ��� ����
            // rotationAngle = count * 45f; // ����: 45�� �������� ȸ��
            rotation = Quaternion.Euler(180f, 180f, 45f);

            // ��ų ����Ʈ�� ȸ�� ����
            skillEffect.transform.rotation = rotation;

            SoundManager.instance.SFXPlay("", VoiceClip[4]);


            count = 1;
        }

        Destroy(skilleffect, 3f);
    }


    public override void Atk() //�ϴ� ���� ���� ������ �ٲܰ���.
    {

        // ȸ�� ������ ������ ��, Y���� �߽����� ȸ���ϵ��� ����
        //float rotationAngle = count * 45f; // ����: 45�� �������� ȸ��
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        if (countatk == 0)
        {
            rotation = Quaternion.Euler(0f, 0f, 45f);

            // ��ų ����Ʈ�� ȸ�� ����
            atkEffect.transform.rotation = rotation;
            SoundManager.instance.SFXPlay("", VoiceClip[0]);
            countatk = 1;
        }
        else
        {

            rotation = Quaternion.Euler(0f, 0f, -135f);

            // ��ų ����Ʈ�� ȸ�� ����
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
       // Debug.Log("�����Ǿ���");
       // Destroy(ultimateWaiteffect, 6f);
    }

    public override void HitEffect()
    {
        GameObject hiteffect = Instantiate(hitEffect, BladeHitPos.transform.position, Quaternion.Euler(BladeHitPos.transform.position)); //�̰� �� ��ġ���� ���� ��ų����. 
        Destroy(hiteffect, 1f);

    }

    public override void DestroyUltimate()
    {
        if (ultimateWaiteffect != null)
        {
            Destroy(ultimateWaiteffect);
            //Debug.Log("�ڽĿ� �ִ� �� �Լ��� ���� �Ǿ���");
        }
    }

    public override void Damaged()
    {
        SoundManager.instance.SFXPlay("", VoiceClip[5]); //�ǰ� ��������,
    }
    
    public override void Died()
    {
        SoundManager.instance.SFXPlay("", VoiceClip[6]);//�׾����� ȣ��
    }
   

}
