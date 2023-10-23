using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlay : MonoBehaviour
{
    public GameObject atkEffect;
    public GameObject skillEffect;
    public GameObject ultimateEffect;
    public GameObject hitEffect;
    public GameObject ultimateWaitEffect;

   void Skill()
   {
      GameObject skilleffect = Instantiate(skillEffect, transform.position, transform.rotation);
      Destroy(skilleffect, 3f);
   }

    
    void Atk()
    {
        GameObject atkeffect = Instantiate(atkEffect, transform.position, transform.rotation);
        Destroy(atkeffect, 3f);
    }

    public void Ultimate()
    {
        GameObject ultimateeffect = Instantiate(ultimateEffect, transform.position, transform.rotation);
       // Destroy(ultimateeffect, 3f);
    }

    public void UltimateWaitEffect()
    {
        GameObject ultimateWaiteffect = Instantiate(ultimateWaitEffect, transform.position, Quaternion.identity);
        Destroy(ultimateWaiteffect, 3f);
    }

    void HitEffect()
    {
        GameObject hiteffect = Instantiate(hitEffect, transform.position + new Vector3(0,0,1.5f), Quaternion.identity);
        Destroy(hiteffect, 1f);

    }

}


