using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<PlayerController> player;
    


    //만들려는 게임 스타레일 모작 
    //게임의 특징 매우 단순한 턴제 방식이지만, 턴의 선송권이 캐릭터가 가진 속도로 계산이 된다.
    //속도 공식 부터 적용 해야됨.
    //캐릭터가 몬스터를 찾아 공격하는건 두번째 문제임.

    public enum State
    {
        whoisTurn, playerTurn, enemyTurn, win, Lose // whoisTurn 에서
            //플레이어와 적의 속도를 계산해서 턴의 선공권을 가져올 것임.
    }

    public State state;
    public bool isLive;

    void Awake()
    {
        state= State.whoisTurn; //기본 상태임, 전투 진입 상태가 아님.
                                //BattleStart(); 적이 피격되거나 또는 플레이거가 피격시 전투 시작을 할것임. 
        player = new List<PlayerController>();
    }

    private void Update()
    {
        if(state == State.whoisTurn)
        {
            player.Clear();
            foreach( PlayerController controller in player ) 
            {
            
            }
        }
    }

    void BattleStart()
    {
        //전투 시작시 캐릭터의 턴중 효과 또는 이펙트나 애니메이션을 사용

        //플레이어나 적에게 턴을 넘길 것 속도에 따라서
        state = State.whoisTurn;
    }

    public void PlayerAttackButton()
    {
        //공격 스킬, 데미지 등 코드 또는 함수 호출
        //버튼이 계속 눌리는 거를 방지하기

        if (state != State.playerTurn)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("플레이어 공격 실행");
        //공격 스킬, 데미지 등 코드 또는 플레이어 행동 상태 적용

        //적이 죽었으면 전투 종료

        if(!isLive)
        {
            state = State.win;
            EndBattle();
            //적이 살았으면, 서로의 속도를 계산해서 턴 넘기기

        }
        else
        {
            state = State.whoisTurn;
        }
        
    }

    void EndBattle()
    {
        Debug.Log("전투 종료");
    }
}
