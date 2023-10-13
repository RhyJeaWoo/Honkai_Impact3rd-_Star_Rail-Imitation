using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
   
    public float baseSpeed;//기초속도 이 캐릭터가 가질 속도

    public float buffspeed;//버프로 증가 할 수 있는 속도

    public float flatSpeed; //이 캐릭터가 장비로 받을 수 있는 속도

    private float finalSpeed; //최종적으로 오르는 속도

    private float TurnSpeed;//기초 행동 수치 (환산 후 저장할 변수)

    private float TimeTurn;

    public bool ismyTurn;//내 턴일 경우 (정확히는 플레이어가 총괄하는 내턴 일 경우) 외부에서 내 턴을 내외에서 전부 지정 해 줄 변수
                           //게임 매니저가 관여하거나, 내가 직접 끄거나

    public bool isTurnOn; //

    protected bool notmyturn;//상대턴일경우를 받아와서 내 턴하고 겹치는지 체크해줄 변수

    // finalSpeed = baseSpeed * (1 + buffspeed/100 ) + flatSpeed;
    // 최종 속도 = 기초 속도 * 버프로 증가하는 속도 + 장비로 증가하는 깡 속도



    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion

    protected virtual void Awake()
    {

        finalSpeed = baseSpeed * (1 + (buffspeed / 100 + buffspeed % 100) + finalSpeed);
        TurnSpeed = 10000/finalSpeed + 10000%finalSpeed;

        TimeTurn = TurnSpeed;


    }


    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

       
    }

    //상대 턴이거나 내 다른 오브젝트의 턴일 경우 행동 수치가 감소하지 않음.


    protected virtual void Update()
    {

        if(ismyTurn)

        
        if (ismyTurn && !notmyturn) //내턴이 On인지, 그리고 다른 턴들이 off인지 체크
        {
            isTurnOn = true; 

            TimeTurn -= 1;
            if (TimeTurn < 0)
            {
                TimeTurn = TurnSpeed;
                 
            }

        }

            Debug.Log(TurnSpeed);
    }



    
}
