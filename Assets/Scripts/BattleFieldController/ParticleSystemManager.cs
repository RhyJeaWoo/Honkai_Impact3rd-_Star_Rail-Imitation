using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public GameObject[] childGameObjects; // 자식 게임 오브젝트 배열
    public int ExitCount = 0; // 종료된 파티클 카운트

    private void Start()
    {
        // MultipleObjectsMake 스크립트에서 정의한 이벤트를 구독
        MultipleObjectsMake[] particleSystems = GetComponentsInChildren<MultipleObjectsMake>();
        foreach (MultipleObjectsMake particleSystem in particleSystems)
        {
            particleSystem.OnObjectsMakeComplete += HandleObjectsMakeComplete;
        }
    }

    public void HandleObjectsMakeComplete()
    {
        // 파티클 생성이 완료되었을 때 호출됨
        ExitCount++;
        if (ExitCount >= childGameObjects.Length)
        {
            Debug.Log("파티클이 모두 종료되었음");
            gameObject.SetActive(false);
            return;
          
        }
    }
}
