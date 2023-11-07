using UnityEngine;

//전략 쓰자, 메이는 메이대로 듀란달은 듀란달대로 전략 써야될듯 ㅅㅂ ㅡㅡ
//아니면 그냥 이거 스크립트 존나 만들면 되긴함. 전략이나 이거나, 비슷함.

public class AnimPlay : MonoBehaviour
{
    public GameObject atkEffect;
    public GameObject skillEffect;
    public GameObject ultimateEffect;
    public GameObject hitEffect;
    public GameObject ultimateWaitEffect;

    public GameObject BladeHitPos;

    //int countatk = 0;

    public virtual void Skill() //메이 전용 값임
    {
        GameObject skilleffect = Instantiate(skillEffect, BladeHitPos.transform.position, skillEffect.transform.rotation);

     
        Destroy(skilleffect, 3f);
    }


    public virtual void Atk() //일단 메이 전용 값으로 바꿀거임.
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
        Debug.Log("생성되었음");
        Destroy(ultimateWaiteffect, 6f);
    }

    public virtual void HitEffect()
    {
        GameObject hiteffect = Instantiate(hitEffect, BladeHitPos.transform.position, Quaternion.Euler(BladeHitPos.transform.position)); //이게 검 위치에서 생성 시킬거임. 
        Destroy(hiteffect, 1f);

    }

}


