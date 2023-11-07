using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExit : MonoBehaviour
{
    // If true, deactivate the object instead of destroying it
    public bool OnlyDeactivate;

    ParticleSystem[] ps;

    void OnEnable()
    {
        StartCoroutine("CheckIfAlive");
    }

    IEnumerator CheckIfAlive()
    {
        ps = GetComponentsInChildren<ParticleSystem>();

        while (true && ps != null)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < ps.Length; i++)
            {
                if (!ps[i].IsAlive(true))
                {
                    if (OnlyDeactivate)
                    {

                        this.gameObject.SetActive(false);
                    }
                    else
                        GameObject.Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
