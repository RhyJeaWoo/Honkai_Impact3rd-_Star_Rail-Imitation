using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectsMake : _ObjectsMakeBase
{
    private GameObject[] m_objArray;//게임 오브젝트 배열로 변경

    public event Action OnObjectsMakeComplete;//델리게이트
    public GameObject m_obj;


    public float m_startDelay;
    public int m_makeCount;
    public float m_makeDelay;
    public Vector3 m_randomPos;
    public Vector3 m_randomRot;
    public Vector3 m_randomScale;
    public bool isObjectAttachToParent = true;

    float m_Time;
    float m_Time2;
    float m_delayTime;
    float m_count;
    float m_scalefactor;


    void Start()
    {
        m_Time = m_Time2 = Time.time;
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor; //transform.parent.localScale.x; 

        ParticleSystemManager particleSystem = GetComponentInParent<ParticleSystemManager>();

        if(particleSystem != null )
        {
            OnObjectsMakeComplete += particleSystem.HandleObjectsMakeComplete;
        }
    }


    void Update()
    {
        if (Time.time > m_Time + m_startDelay)
        {
            if (Time.time > m_Time2 + m_makeDelay && m_count < m_makeCount)
            {
                Vector3 m_pos = transform.position + GetRandomVector(m_randomPos)* m_scalefactor; 
                Quaternion m_rot = transform.rotation * Quaternion.Euler(GetRandomVector(m_randomRot));
                
                m_objArray = new GameObject[m_makeObjs.Length];//배열 초기화

                for (int i = 0; i < m_makeObjs.Length; i++)
                {
                    m_obj = Instantiate(m_makeObjs[i], m_pos, m_rot);
                    Vector3 m_scale = (m_makeObjs[i].transform.localScale + GetRandomVector2(m_randomScale));
                    if(isObjectAttachToParent)
                        m_obj.transform.parent = this.transform;
                    m_obj.transform.localScale = m_scale;

                    m_objArray[i] = m_obj;//게임 오브젝트 배열에 할당
                }

                m_Time2 = Time.time;
                m_count++;

            }

            if(m_count >= m_makeCount)
            {
                for(int i=0; i< m_objArray.Length; i++) 
                {
                    var particleSystem = m_objArray[i].GetComponentsInChildren<ParticleSystem>();
                    bool allSystemsDead = true;

                    foreach(var ps in particleSystem)
                    {
                        if(ps.IsAlive())
                        {
                            allSystemsDead = false;
                            break;
                        }
                    }

                    if(allSystemsDead) 
                    {

                        OnObjectsMakeComplete?.Invoke();

                    }
                
                }
            }


        }
    }
}
