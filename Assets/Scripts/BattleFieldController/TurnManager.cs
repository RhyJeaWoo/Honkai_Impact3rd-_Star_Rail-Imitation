using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance = null;
    bool StopTurn;
    bool isPlayerTurn;

    [SerializeField] private List<Entity> players = new List<Entity>();
    private int currentPlayerIndex = 0;



    private void Awake()
    {
        if (null == instance)
        {

            instance = this;


            DontDestroyOnLoad(this.gameObject);
        }
        else
        {

            Destroy(this.gameObject);
        }
    }


    public static TurnManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }



    void Start()
    {
        players.AddRange(FindObjectsOfType<Entity>());

        StartNextTurn();
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(players.Count);

        // 게임 플레이 로직을 구현
        //   EndPlayerTurn(players[currentPlayerIndex]);
        //   그렇다면 이거를 버튼 키로 조작해서 사용하는 방법을 써야될거 같음.

        //라고는 뜨는데, 행동 수치와 상관없이 돌아가는 중.

        // 턴 관리

        TurnTime();

    }


    public void PlayerAttack()
    {
        //EndPlayerTurn(players[currentPlayerIndex]);   
    }

    public void PlayerSkill()
    {
        //EndPlayerTurn(players[currentPlayerIndex]);
    }

    public void WhoisTurnEnd(GameObject obj)
    {
        //먼저 여기서 플레이어의 턴인지 아니면, 플레이어가 아닌지에 대해서부터 구별이 필요함.


        if (obj.GetComponent<PlayerController>() != null)
        {

            //
            //여기서 턴 종료가 되었다면, 다시 stop을 굴린다.
        }
        else
        {

        }


    }

    //누구의 턴인지 알 수 없을때(누가 먼저 0 인지 알 수 없을 경우, 리스트 전체의 current값을 0이 될때까지 감소)
    public void TurnTime()
    {
        if (!StopTurn)
        {
            for (int i = 0; i < players.Count; i++)
            {

                players[i].currentTurnSpeed -= 1f;

                if (players[i].currentTurnSpeed <= 0)
                {
                    players[i].currentTurnSpeed = 0;

                    //대충 턴 잡고 턴시작되는내용
                    players[i].isMyTurn = true;
                    //적 오브젝트도 여기서 처리하고 
                    //ai로 처리


                    StopTurn = true;

                    //  WhoisTurnEnd(players[i].gameObject);

                    Debug.Log("플레이어 턴 잡힘 " + players[i].name);

                    players[i].currentTurnSpeed = players[i].baseTurnSpeed;


                    //players[i].EndTurn();
                    //StopTurn = false;
                    //players.Remove(players[i]);
                    // players.Add(players[i]);
                    break;
                }
            }
        }
    }

    public void TurnEnd()
    {
        StopTurn = false;
    }


    //이코드를 변경 --> 일단 먼저 턴을 잡은  메이 를 엔드 시켜야됨.
    public void EndPlayerTurn(Entity player)
    {
        player.EndTurn();

        StopTurn = false;
        // 턴을 끝낸 플레이어를 리스트에서 제거
        players.Remove(player);

        // 리스트의 맨 뒤로 다시 추가 (순환)
        players.Add(player);

        StartNextTurn();
    }



    private void StartNextTurn()
    {

        //CanAct가 true이고 가장 먼저 currentTurnSpeed가 0이 되는 플레이어를 찾아 턴을 시작
        Entity nextPlayer = FindPlayerWithLowestCurrentTurnSpeed();

        if (nextPlayer != null)
        {
            //여기서 로직 작성.


            currentPlayerIndex = players.IndexOf(nextPlayer);
            nextPlayer.StartTurn();
            Debug.Log(nextPlayer.name + " has the next turn.");
        }
    }

    private Entity FindPlayerWithLowestCurrentTurnSpeed()
    {
        Entity nextPlayer = null;
        // float lowestTurnSpeed = float.MaxValue;

        // Debug.Log(lowestTurnSpeed);

        foreach (Entity player in players)
        {
            if (player.canAct /* player.currentTurnSpeed < lowestTurnSpeed*/)
            {
                /* lowestTurnSpeed = player.currentTurnSpeed;*/
                nextPlayer = player;
                Debug.Log("실행 되는중");
            }
        }

        return nextPlayer;
    }

}
