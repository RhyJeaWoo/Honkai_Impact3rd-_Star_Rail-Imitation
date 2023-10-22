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
            // 모든 자식 게임 오브젝트가 비활성화될 때 필요한 동작을 수행
            // 이때를 이제 상태에 전달해서 데미지를 주면 될거 같음.
            // 예를 들어, 상태 패턴으로 전환하는 등의 작업
        }
    }
}
