using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlay : MonoBehaviour
{
    public GameObject atkEffect;
    public GameObject skillEffect;
    public GameObject ultimate;

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

    void Ultimate()
    {
        GameObject ultimateEffect = Instantiate(ultimate, transform.position, transform.rotation);
        Destroy(ultimateEffect, 3f);
    }

    void UltimateWaitEffect()
    {
        GameObject ultimateWaiteffect = Instantiate(ultimate, transform.position, transform.rotation);
        Destroy(ultimateWaiteffect, 3f);
    }

}


