using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumineNorAtkState : EnemyState
{
    private EnemyLumine lumine;
    int x;


    public LumineNorAtkState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
    }

    public override void Enter()
    {
        base.Enter();

        x = Random.Range(0, 4);

        TurnManager.Instance.PlayerNum = x;

        lumine.isAttack = true;

        //누굴 공격할지 랜덤으로 바라봄
        if(x ==0)
        {
            lumine.transform.LookAt(TurnManager.Instance.playable[x].transform.position);
            //일단 공격할 플레이어 방향을 바라봄

            TurnManager.Instance.PlayerTransForm = TurnManager.Instance.playable[x].transform.position;
            //그리고 턴 매니저에 있는 벡터 변수애 공격할 플레이어 변수를 저장함.
            //저장된걸 AnimPlayer에서 그 좌표에 공격할 이펙트를 생성 시킴.
            //lumine.anims.Atk();
            
            
            //좋은 방법은 아님.
            TurnManager.Instance.playable[x].HandleLevelDealt(lumine.curLevel); //방어 계수 계산을 위해 레벨 직접 전달.
            
            //TurnManager.Instance.playable[x].isDamaged = true; //상대가 데미지를 입었다고 직접 전달.


            Debug.Log("루미네가 "+ TurnManager.Instance.playable[x].name  + " 를 공격");
        }
        else if(x == 1) 
        {
            lumine.transform.LookAt(TurnManager.Instance.playable[x].transform.position);

            TurnManager.Instance.PlayerTransForm = TurnManager.Instance.playable[x].transform.position;

            TurnManager.Instance.playable[x].HandleLevelDealt(lumine.curLevel);
            
            //TurnManager.Instance.playable[x].isDamaged = true;


            //lumine.anims.Atk();
            Debug.Log("루미네가 " + TurnManager.Instance.playable[x].name + " 를 공격");
        }
        else if(x==2)
        {
            lumine.transform.LookAt(TurnManager.Instance.playable[x].transform.position);

            TurnManager.Instance.PlayerTransForm = TurnManager.Instance.playable[x].transform.position;

            TurnManager.Instance.playable[x].HandleLevelDealt(lumine.curLevel);

            //TurnManager.Instance.playable[x].isDamaged = true;

            //lumine.anims.Atk();
            Debug.Log("루미네가 " + TurnManager.Instance.playable[x].name + " 를 공격");
        }
        else if (x == 3) 
        {
            lumine.transform.LookAt(TurnManager.Instance.playable[x].transform.position);

            TurnManager.Instance.PlayerTransForm = TurnManager.Instance.playable[x].transform.position;
           
            TurnManager.Instance.playable[x].HandleLevelDealt(lumine.curLevel);

            //TurnManager.Instance.playable[x].isDamaged = true;

            //lumine.anims.Atk();


            Debug.Log("루미네가 " + TurnManager.Instance.playable[x].name + " 를 공격");
        }

        //Debug.Log(lumine.enemyTr);


    }

    public override void Exit()
    {
        base.Exit();

        //회전을 원래대로 돌리는 코드가 = 없음.

        if (x == 0)
        {
            TurnManager.Instance.playable[x].isDamaged = false;
        }
        else if (x == 1)
        {
            TurnManager.Instance.playable[x].isDamaged = false;
        }
        else if (x == 2)
        {
            TurnManager.Instance.playable[x].isDamaged = false;
        }
        else if (x == 3)
        {
            TurnManager.Instance.playable[x].isDamaged = false;
        }

        lumine.transform.rotation = Quaternion.Euler(0,180,0);
        //패턴 후 원래 회전값으로 복원.

    }

    public override void Update()
    {
        base.Update();




        if (lumine.anim.GetCurrentAnimatorStateInfo(0).IsName("NorAtk") && lumine.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Debug.Log("루미네 NorAtk 이 조건문은 실행되었음");

            lumine.isMyTurn = false;
            TurnManager.Instance.TurnEnd();

            stateMachine.ChangeState(lumine.idleState);
        }
        else
        {
            Debug.Log("루미네 NorAtk 이 조건문은 실행되지않았음");
        }
          
     
    }
}
