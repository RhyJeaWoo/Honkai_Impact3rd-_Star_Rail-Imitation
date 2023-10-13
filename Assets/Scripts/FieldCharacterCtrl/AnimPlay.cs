using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlay : MonoBehaviour
{
    public GameObject atkEffect;
    public GameObject skillEffect;
    public GameObject ultimate;

   void Skill()
   {
        Instantiate(skillEffect, transform.position, transform.rotation);
   }

    
    void Atk()
    {
        Instantiate(atkEffect, transform.position, transform.rotation);
    }

    void Ultimate()
    {
        Instantiate(ultimate, transform.position, transform.rotation);
    }

}


