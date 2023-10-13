using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HP_Subject : MonoBehaviour, Subject
{
    //등록되어있는 옵저버에 대한 관리
    private List<Observer> observers = new List<Observer>();

    private float player_HP = 0.0f;
    private float enemy_HP = 0.0f;

    public void Changed(float _player_hp, float _enemy_hp)
    {
        //변경된 정보를 갱신해 Notify 호출
        player_HP = _player_hp;
        enemy_HP = _enemy_hp;
        Notify();
    }


    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.Observer_Update(player_HP, enemy_HP);
        }
    }

    public void Register(Observer _observer)
    {
        observers.Add(_observer);
    }

    public void Remove(Observer _observer)
    {
        observers.Remove(_observer);
    }
}

