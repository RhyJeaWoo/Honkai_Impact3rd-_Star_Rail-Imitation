using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public BaseEnemy enemy;

    public enum TurnState //�� ���¸� ���������� ����
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for the ProgressBar

    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;

    //���� ������Ʈ
    private Vector3 startposition;
    //timeforaction stuff
    private bool actionStarted = false;
    public GameObject HeroToAttack;
    private float animSpeed;


    void Start()
    {
        currentState = TurnState.PROCESSING;
       // BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startposition = transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):

                break;
            case (TurnState.CHOOSEACTION):
                ChoosesAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):

                break;
         
            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());

                break;
            case (TurnState.DEAD):

                break;
        }
    }

    void UpgaradeProgressBar() //ui ���α׷����� �ε�, ���� �Ⱦ�����, ���� ���� �������°� �ൿ �ӵ� ����ؼ� �����;ߵ�.
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;

        if (cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }

    }

    void ChoosesAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = enemy.name;
        myAttack.Type = "Enemy";
        myAttack.AttacksGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.HerosInGame[Random.Range(0, BSM.HerosInGame.Count)];
        BSM.CollectActions(myAttack);
    }

    private IEnumerator TimeForAction()
    {
        if(actionStarted)
        {
            yield break; //actionStarted �ɸ��� �ٷ� ����
        }

        actionStarted = true;

        //�� ������Ʈ�� �ִϸ��̼�(����)�� �����ϴ� �κ�
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 1.5f,
            HeroToAttack.transform.position.y, 
            HeroToAttack.transform.position.z
            );
        while(MoveTowardsEnemy(heroPosition))
        {
            yield return null;
        }



        //wait abit

        yield return new WaitForSeconds(0.5f);

        //������ ��



        //animation back to startposition

        Vector3 firstPosition = startposition;
        while (MoveTowardsStart(firstPosition)) { yield return null; }

        //remove this performer from the list in BSM
        BSM.performList.RemoveAt(0);

        // reset BSM -> Wait
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
        //end corutine
        actionStarted = false;
        //reset this enemy state
        cur_cooldown = 0f;
        currentState = TurnState.PROCESSING;
        
    }


    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
}
