using System;
using UnityEngine;
using UnityEngine.UI;

//턴을 여기서 받을거임.

public class Turn : MonoBehaviour
{

    //서브젝트 연결
    [SerializeField] private HP_Subject HP_Subject = null;

    //옵저버 연결
    [SerializeField] Player_State_Observer player_State_Observer = null;
    [SerializeField] Enemy_State_Observer enemy_State_Observer = null;

    //결과에 대한 출력 텍스트
    [SerializeField] Text battle_result = null;

    //각 변수에 대한 기본 설정
    private float start_player_hp = 10.0f;
    private float start_enemy_hp = 10.0f;

    private float cur_player_hp = 0.0f;
    private float cur_enemy_hp = 0.0f;

    private void Start()
    {
        //옵저버 생성
        player_State_Observer.Init(HP_Subject);
        enemy_State_Observer.Init(HP_Subject);

        start_player_hp = 10.0f;
        start_enemy_hp = 10.0f;

        cur_player_hp = start_player_hp;
        cur_enemy_hp = start_enemy_hp;

        //옵저버 등록
        HP_Subject.Register(player_State_Observer);
        HP_Subject.Register(enemy_State_Observer);

        //옵저버 초기화
        HP_Subject.Changed(cur_player_hp, cur_enemy_hp);
    }

    //버튼 기능 연출 (턴이 올때만 사용할 수 있게 할것임)
    public void OnNextButtonEnter()
    {
        if (cur_player_hp <= 0.0f || cur_enemy_hp <= 0.0f)
        {
            return;
        }

        //공격 판정
        int attack = UnityEngine.Random.Range(0, 2); // 0 or 1

        switch (attack)
        {
            case 0:
                battle_result.text = "플레이어의 공격!";
                cur_enemy_hp--;
                break;
            case 1:
                battle_result.text = "적의 공격!";
                cur_player_hp--;
                break;
        }

        //현재 체력/최대 체력만큼 수치 변화
        HP_Subject.Changed(cur_player_hp / start_player_hp, cur_enemy_hp / start_enemy_hp);
        Debug.Log($"플레이어의 체력 : {cur_player_hp}/ {start_player_hp}  적의 체력 : {cur_enemy_hp} / {start_enemy_hp}");


        //승리 판정

        if (cur_player_hp <= 0f)
        {
            battle_result.text = "적의 승리!";
        }
        else if (cur_enemy_hp <= 0f)
        {
            battle_result.text = "플레이어의 승리!";
        }
    }


}
