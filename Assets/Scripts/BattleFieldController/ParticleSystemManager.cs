using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public GameObject[] childGameObjects; // �ڽ� ���� ������Ʈ �迭
    public int ExitCount = 0; // ����� ��ƼŬ ī��Ʈ

    private void Start()
    {
        // MultipleObjectsMake ��ũ��Ʈ���� ������ �̺�Ʈ�� ����
        MultipleObjectsMake[] particleSystems = GetComponentsInChildren<MultipleObjectsMake>();
        foreach (MultipleObjectsMake particleSystem in particleSystems)
        {
            particleSystem.OnObjectsMakeComplete += HandleObjectsMakeComplete;
        }
    }

    public void HandleObjectsMakeComplete()
    {
        // ��ƼŬ ������ �Ϸ�Ǿ��� �� ȣ���
        ExitCount++;
        if (ExitCount >= childGameObjects.Length)
        {
            Debug.Log("��ƼŬ�� ��� ����Ǿ���");
            gameObject.SetActive(false);
            return;
          
        }
    }
}
