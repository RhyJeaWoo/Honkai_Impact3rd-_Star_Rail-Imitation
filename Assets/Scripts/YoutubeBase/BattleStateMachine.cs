using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,//���
        TAKEACTION,//�׼�
        PERFORMACTION//����

    }

    public PerformAction battleStates;

    public List<HandleTurn> performList = new List<HandleTurn>();
    public List<GameObject> HerosInGame = new List<GameObject>();
    public List<GameObject> EnemyInBattle = new List<GameObject>();


    public enum HeroGuI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE

    }

    public HeroGuI HeroInput;

    public List<GameObject> HerosTomanage = new List<GameObject>();
    private HandleTurn HeroChoice;

    public GameObject enemyButton;
    public Transform Spacer;


    // Start is called before the first frame update
    void Start()
    {
        battleStates = PerformAction.WAIT;
        EnemyInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HerosInGame.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        EnemyButton();
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleStates)
        {
            case (PerformAction.WAIT):
                if(performList.Count > 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }
                break;
            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(performList[0].Attacker);
                if (performList[0].Type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.HeroToAttack = performList[0].AttackersTarget;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }

                if (performList[0].Type == "Hero")
                {

                }

                battleStates = PerformAction.PERFORMACTION;

                break;
            case (PerformAction.PERFORMACTION):
                break;
        }
    }

    public void CollectActions(HandleTurn imput)
    {
        performList.Add(imput);
    }

    void EnemyButton()
    {
        foreach(GameObject enemy in EnemyInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine>();

            Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text>();
            buttonText.text = cur_enemy.enemy.name;


            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(Spacer, false);
        }
    }

}
