using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{

    private BattleStateMachine BSM;
    public BaseHero hero;

    public enum TurnState //턴 상태를 열거형으로 정의
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for the ProgressBar

    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image ProgressBar;



    void Start()
    {
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PROCESSING;
    }

    
    void Update()
    {
        switch(currentState)
        {
            case (TurnState.PROCESSING):
                
                break;

            case (TurnState.ADDTOLIST):
                BSM.HerosTomanage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;
            case (TurnState.WAITING):
                //idleState

                break;
            case (TurnState.SELECTING):

                break;
            case (TurnState.ACTION):

                break;
            case (TurnState.DEAD):

                break;
        }
    }

    void UpgaradeProgressBar() //ui 프로그래스바 인데, 나는 안쓸거임, 나는 턴을 가져오는걸 행동 속도 계산해서 가져와야됨.
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float cacl_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(cacl_cooldown, 0 ,1), 
            ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);

        if(cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }



    }

}
