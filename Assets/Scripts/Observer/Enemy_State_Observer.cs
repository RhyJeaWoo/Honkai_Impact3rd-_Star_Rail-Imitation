using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_State_Observer : MonoBehaviour, Observer
{

    [SerializeField] private Image HP_BAR = null;

    //�ڡڡ� �������� ��� ������ Subject�� �����ϴ�.
    private HP_Subject HP_Subject = null;

    public void Init(HP_Subject _subject)
    {
        HP_Subject = _subject;
    }


    public void Observer_Update(float player_hp, float enemy_hp)
    {
        HP_BAR.fillAmount = enemy_hp;
    }
}
