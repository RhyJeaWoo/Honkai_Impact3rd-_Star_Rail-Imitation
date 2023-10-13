using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Player_State_Observer : MonoBehaviour, Observer
    {

    [SerializeField] private Image HP_BAR = null;

    //★★★ 옵저버는 멤버 변수로 Subject를 가집니다.
    private HP_Subject HP_Subject = null;

    public void Init(HP_Subject _subject)
    {
        HP_Subject = _subject;
    }


    public void Observer_Update(float player_hp, float enemy_hp)
    {
        HP_BAR.fillAmount = player_hp;
    }
}

