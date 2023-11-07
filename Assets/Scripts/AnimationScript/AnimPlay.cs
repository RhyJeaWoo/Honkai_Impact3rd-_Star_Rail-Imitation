using UnityEngine;

//���� ����, ���̴� ���̴�� ������� ����޴�� ���� ��ߵɵ� ���� �Ѥ�
//�ƴϸ� �׳� �̰� ��ũ��Ʈ ���� ����� �Ǳ���. �����̳� �̰ų�, �����.

public class AnimPlay : MonoBehaviour
{
    public GameObject atkEffect;
    public GameObject skillEffect;
    public GameObject ultimateEffect;
    public GameObject hitEffect;
    public GameObject ultimateWaitEffect;

    public GameObject BladeHitPos;

    //int countatk = 0;

    public virtual void Skill() //���� ���� ����
    {
        GameObject skilleffect = Instantiate(skillEffect, BladeHitPos.transform.position, skillEffect.transform.rotation);

     
        Destroy(skilleffect, 3f);
    }


    public virtual void Atk() //�ϴ� ���� ���� ������ �ٲܰ���.
    {
        GameObject atkeffect = Instantiate(atkEffect, BladeHitPos.transform.position, atkEffect.transform.rotation);

        Destroy(atkeffect, 3f);
    }

    public virtual void Ultimate()
    {
        GameObject ultimateeffect = Instantiate(ultimateEffect, transform.position, transform.rotation);
        // Destroy(ultimateeffect, 3f);
    }

    public virtual void UltimateWaitEffect()
    {
        GameObject ultimateWaiteffect = Instantiate(ultimateWaitEffect, transform.position, ultimateWaitEffect.transform.rotation);
        Debug.Log("�����Ǿ���");
        Destroy(ultimateWaiteffect, 6f);
    }

    public virtual void HitEffect()
    {
        GameObject hiteffect = Instantiate(hitEffect, BladeHitPos.transform.position, Quaternion.Euler(BladeHitPos.transform.position)); //�̰� �� ��ġ���� ���� ��ų����. 
        Destroy(hiteffect, 1f);

    }

}


