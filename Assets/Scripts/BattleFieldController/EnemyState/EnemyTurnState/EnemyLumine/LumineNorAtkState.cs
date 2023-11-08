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

        //���� �������� �������� �ٶ�
        if(x ==0)
        {
            lumine.transform.LookAt(TurnManager.Instance.playable[x].transform.position);
            //�ϴ� ������ �÷��̾� ������ �ٶ�

            TurnManager.Instance.PlayerTransForm = TurnManager.Instance.playable[x].transform.position;
            //�׸��� �� �Ŵ����� �ִ� ���� ������ ������ �÷��̾� ������ ������.
            //����Ȱ� AnimPlayer���� �� ��ǥ�� ������ ����Ʈ�� ���� ��Ŵ.
            //lumine.anims.Atk();

            Debug.Log("��̳װ� "+ TurnManager.Instance.playable[x].name  + " �� ����");
        }
        else if(x == 1) 
        {
            lumine.transform.LookAt(TurnManager.Instance.playable[x].transform.position);

            TurnManager.Instance.PlayerTransForm = TurnManager.Instance.playable[x].transform.position;



            //lumine.anims.Atk();
            Debug.Log("��̳װ� " + TurnManager.Instance.playable[x].name + " �� ����");
        }
        else if(x==2)
        {
            lumine.transform.LookAt(TurnManager.Instance.playable[x].transform.position);

            TurnManager.Instance.PlayerTransForm = TurnManager.Instance.playable[x].transform.position;



            //lumine.anims.Atk();
            Debug.Log("��̳װ� " + TurnManager.Instance.playable[x].name + " �� ����");
        }
        else if (x == 3) 
        {
            lumine.transform.LookAt(TurnManager.Instance.playable[x].transform.position);

            TurnManager.Instance.PlayerTransForm = TurnManager.Instance.playable[x].transform.position;



            //lumine.anims.Atk();


            Debug.Log("��̳װ� " + TurnManager.Instance.playable[x].name + " �� ����");


        }

        //Debug.Log(lumine.enemyTr);


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if (x == 0)
        {
           
        }
        else if (x == 1)
        {
            
        }
        else if (x == 2)
        {
           
        }
        else if (x == 3)
        {
            
        }


        if (lumine.anim.GetCurrentAnimatorStateInfo(0).IsName("NorAtk") && lumine.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Debug.Log("��̳� NorAtk �� ���ǹ��� ����Ǿ���");

            lumine.isMyTurn = false;
            TurnManager.Instance.TurnEnd();

            stateMachine.ChangeState(lumine.idleState);
        }
        else
        {
            Debug.Log("��̳� NorAtk �� ���ǹ��� ��������ʾ���");
        }
          
     
    }
}
