using System.Collections;
using UnityEngine;

public class ParticleExit : MonoBehaviour
{
    // If true, deactivate the object instead of destroying it
    public bool OnlyDeactivate;
    public bool OnParticleDamage = false;

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
                    {
                        OnParticleDamage = false;
                        GameObject.Destroy(this.gameObject);
                        break;
                    }
                }
                else
                {
                    if (!OnParticleDamage)
                    {
                        for (int j = 0; j < TurnManager.Instance.playable.Count; j++)
                        {
                            //TurnManager.Instance.playable[j].
                            if (TurnManager.Instance.PlayerNum == j)
                            {
                                TurnManager.Instance.playable[j].isDamaged = true;
                                break;
                            }

                        }
                        yield return new WaitForSeconds(1f);
                        OnParticleDamage = true;
                    }
                }
            }
        }
    }
}
