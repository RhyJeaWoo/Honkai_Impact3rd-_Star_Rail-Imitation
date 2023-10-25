using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayerSkillStrategy currentSkill; //전략 인터페이스 할당


    public GameObject Enemy_target_simbol;

    public GameObject Player_target_simbol;

    private static TurnManager instance = null;

    [SerializeField] private int SkillStack = 3;

   

    public string targetPlayerName;

    public string targetEnemyName;


    public bool StopTurn; //true면 턴 중지 이때 IsTurn이 true면 턴 인계로 false됨

    public bool IsTurn; //누군가가 턴 실행 중이다 true

    // 10/24일
    public bool isUltimateActivate; //궁극기가 실행 되는중이라면 true  -> (현재는 이 의미로 사용) 궁극기를 누른 플레이어가 있는가?

    public bool ultimateReserved = false;//궁극기가 예약 되었다면 true

 

    [Header("오브젝트 관련 리스트 및 요소들")]
    [SerializeField] private List<Entity> all_obj = new List<Entity>();
    //현제 게임내에 존재하는 오브젝트 전체의 리스트

    [SerializeField] public List<EnemyAIController> enemys = new List<EnemyAIController>();
    //적들만 구분하기 위해 다시 저장하는 리스트

    [SerializeField] public List<PlayerController> playable = new List<PlayerController>();
    //플레어블만 구분하기 위해 선언된 리스트


    [SerializeField] private int curEnemyIndex = 0;
    [SerializeField] private int curPlayerIndex = 0;

    [Header("궁극기 사용 순서를 나타내기 위해 사용하는 큐")]
    public Queue<PlayerController> ultimateQueue = new Queue<PlayerController>();

    //[Header("턴이 먼저 잡힌 순서를 나타내기 위해 사용하는 큐")]
    //public Queue<Entity> isturnQueue = new Queue<PlayerController>();
    


    public Vector3 TargetSimbolEnemyTr; //내가 지정한 적의 위치 앞에 이동하기 위해 저의 좌표를 저장할 벡터3 좌표 변수임
                                        //턴이 끝나면 초기화 시킬거임.

    public Vector3 EnemyTransForm { get; set; } //적의 위치를 저장하는 변수

    public Vector3 TargetSimbolPlTr;

    public Vector3 PlayerTransForm;




    public Transform[] healTarget; // 힐 대상을 저장하는 변수 , 사실상 플레이어들의 트랜스폼을 저장하는 변수임.

    Vector3 ResetTargetRot;


    private void Awake()
    {
        SingTone();

    }

    private void OnEnable()
    {
        StartCoroutine(ReSizeBox());
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
    public void SingTone()
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

        Enemy_target_simbol.SetActive(false);
    }


    void Start()
    {


        ListAddRange();

        ListSort();

        PlayerHealPos();

        SetStrategy();


        Enemy_target_simbol.transform.position = new Vector3(enemys[0].transform.position.x, Enemy_target_simbol.transform.position.y, Enemy_target_simbol.transform.position.z);

        ResetTargetRot = Enemy_target_simbol.transform.eulerAngles;
    }


    private void Update()
    {
        TargetMove();// 타겟을 정하고 그 타겟이 어디에 있는지 저장하려는 함수

        TurnTime();

        Debug.Log("ultimateQueue.Count: " + ultimateQueue.Count);
     
    }


    public void UltimateEnd()
    {
        isUltimateActivate = false;
    }

    public void ListAddRange()
    {
        all_obj.AddRange(FindObjectsOfType<Entity>());
        enemys.AddRange(FindObjectsOfType<EnemyAIController>());

        playable.AddRange(FindObjectsOfType<PlayerController>());
    }

    public void PlayerHealPos()
    {

        healTarget = new Transform[playable.Count];

        for (int i = 0; i < playable.Count; i++)
        {
            healTarget[i] = playable[i].transform;
        }


    }

    public void ListSort()
    {
        List<PlayerController> sortedPlayers = new List<PlayerController>(playable);

        //원하는 순서대로 정렬
        sortedPlayers.Sort((player1, player2) => player1.transform.position.x.CompareTo(player2.transform.position.x));

        //정렬된 리스트를 playerble 리스트에 할당
        playable = sortedPlayers;
    }

    public void SetStrategy()
    {
        //이 필드에 존재하는 플레이어들에게 전략 할당
        foreach (PlayerController player in playable)
        {
            PlayerSkillStrategy skillStrategy = null;
            PlayerAttackStrategy attackStrategy = null;
            PlayerUltimateStrategy ultimateStrategy = null;



            if (player.CompareTag("Mei"))
            {
                skillStrategy = new MeiSkill();
                attackStrategy = new MeiAttack();
                ultimateStrategy = new MeiUltimate();

            }
            else if (player.CompareTag("Kiana"))
            {
                skillStrategy = new KianaSkill();
                attackStrategy = new KianaAttack();
                ultimateStrategy = new KianaUltimate();

            }
            else if (player.CompareTag("Elysia"))
            {
                skillStrategy = new ElysiaSkill();
                attackStrategy = new ElysiaAttack();
                ultimateStrategy = new ElysiaUltimate();

            }
            else if (player.CompareTag("Durandal"))
            {
                skillStrategy = new DurandalSkill();
                attackStrategy = new DurandalAttack();
                ultimateStrategy = new DurandalUltimate();

            }

            if (skillStrategy != null)
            {
                player.SetSkillStrategy(skillStrategy);
                player.SetAttackStrategy(attackStrategy);
                player.SetUltimateStratergy(ultimateStrategy);

            }
        }

    }


    public void TurnTime()
    {
        // 여기에서 궁극기 예약 및 큐에 추가 로직을 유지
        for (int i = 0; i < playable.Count; i++)
        {
            if (ultimateQueue.Count > 0) //궁극기를 누른 player오브젝트를 큐에 저장되면, Count가 오름
            {
                isUltimateActivate = true; //궁극기를 누른 플레이어가 있는가?
                playable[i].HandleUltimateReservations();
                //IsReservingUltimate가  playable[i]가 예약했다는 뜻 true로 바뀜
                //이때 여러명이 누를 수 있으니, playerControl이   public bool isUltimate 가 모두 false 일때 IsReservingUltimate가 false로 바뀌어야함.
            }else
            {
                isUltimateActivate = false;
            }
               


                if (playable[i].cureng == playable[i].maxeng)
                {
                    KeyCode hotkey = KeyCode.Alpha1 + i;
                    if (Input.GetKeyDown(hotkey))
                    {
                        //if (!playable[i].isMyTurn && !playable[i].isUltimate)
                        
                            ReserveUltimate(playable[i]);
                            // 플레이어를 "궁극기 예약 중" 상태로 설정
                            playable[i].isUltimate = true; //여기서 궁극기가 사용가능한가.
                            Debug.Log((i + 1) + "번 키가 눌렸음");
                        //여기서 만약 예약만 한다고 치면 
                        
                    }
                }
          
        }


        if (ultimateQueue.Count == 0) //궁극기가 예약된 유저가 없을 경우 턴을 속행
        {
            
            // 큐가 비어있을 때, 즉 예약된 궁극기가 없을 때에만 플레이어 턴 처리
            for (int i = 0; i < all_obj.Count; i++)
            {
                if (!all_obj[i].isUltimate)
                {
                    if(!StopTurn)
                    all_obj[i].currentTurnSpeed -= 1f;
                }
                else
                {
                    Debug.Log("궁극기 상태이므로, 정지되었음.");
                }

                if (all_obj[i].currentTurnSpeed <= 0)
                {

                    all_obj[i].currentTurnSpeed = 0; //0보다 작아지면 0밑으로 안떨어지게

                    //일단 이대로
                    StopTurn = true; //매니저가 턴 종료한다고 선언
                    
                    all_obj[i].isMyTurn = true; //그리고 이 플레이어의 턴이 실행

                    // 턴이 잡힌 플레이어를 큐에 추가
                    //ReserveNextTurnPlayer(all_obj[i]);



                    // "궁극기 예약 중" 상태인 플레이어의 플래그 해제
                    if (all_obj[i].IsReservingUltimate)
                    {
                        all_obj[i].IsReservingUltimate = false;
                      
                     
                    }

                    Debug.Log("플레이어 턴 잡힘 " + all_obj[i].name);
                    all_obj[i].currentTurnSpeed = all_obj[i].baseTurnSpeed;
                }
            }
        }
    }


    /*========================================================== 실험중 ===================================================*/
    private void ReserveUltimate(PlayerController player)
    {
        // 예약된 궁극기를 대기열에 추가
        ultimateQueue.Enqueue(player);
        // 큐에 추가할 때 디버그 메시지 출력
        Debug.Log(player.name + "의 궁극기 예약이 큐에 추가되었음.");
    }

    private void ReserveNextTurnPlayer(Entity currentPlayer) //턴이 잡힌 플레이어를 큐에 추가
    {
        if (currentPlayer is PlayerController)
        {
            PlayerController nextPlayer = (PlayerController)currentPlayer;
            ultimateQueue.Enqueue(nextPlayer);
        }
    }


    /* 다시 실험
    public void TurnTime()
    {

        if (ultimateQueue.Count > 0)
        {
            isUltimateActivate = true;

            PlayerController nextPlayer = ultimateQueue.Peek(); // 큐에서 다음 플레이어를 가져옴
            if (!nextPlayer.isMyTurn)
            {
                // 다음 플레이어의 턴이 종료되었음

                nextPlayer.isUltimate = true;

                ultimateQueue.Dequeue(); // 큐에서 제거
                Debug.Log(nextPlayer.name + "의 궁극기가 발동 직전임2");
                // 궁극기 발동 로직 추가
            }
        }

        if (!isUltimateActivate)//궁극기 활성 상태가 아니라면
        {
            for (int i = 0; i < playable.Count; i++)
            {
                if (playable[i].cureng == playable[i].maxeng) //소유한 에너지가 같은가
                {
                    KeyCode hotkey = KeyCode.Alpha1 + i; // 플레이어 번호에 해당하는 키 계산
                    if (Input.GetKeyDown(hotkey))
                    {
                      
                            ReserveUltimate(playable[i]);
                            
                            if (!all_obj[i].isMyTurn)
                            {
                                isUltimateActivate = true;
                                //playable[i].isMyTurn = true;

                                playable[i].isUltimate = true;
                                Debug.Log((i + 1) + "번 키가 눌렸음");
                            }
                        
                    }
                }
            }

        }

        if (!StopTurn)// isUltimateActivate = true;
        {
            for (int i = 0; i < all_obj.Count; i++)
            {
              
                    all_obj[i].currentTurnSpeed -= 1f;
            

                if (all_obj[i].currentTurnSpeed <= 0)
                {
                    all_obj[i].currentTurnSpeed = 0;
                    
                    all_obj[i].isMyTurn = true;

                    StopTurn = true; //턴을 멈춰라
                    Debug.Log("플레이어 턴 잡힘 " + all_obj[i].name);
                    all_obj[i].currentTurnSpeed = all_obj[i].baseTurnSpeed;
                    break;
                }
            }
        }
    }*/

    public void TurnEnd()
    {
        StopTurn = false;
    }



    public void TargetMove()
    {


        if (Input.GetKeyDown(KeyCode.A))
        {
            curEnemyIndex--;
            curPlayerIndex++;


            Debug.Log("키 입력은 됬음");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            curEnemyIndex++;
            curPlayerIndex--;

            Debug.Log("키 입력은 됬음");

        }

        curEnemyIndex = Mathf.Clamp(curEnemyIndex, 0, enemys.Count - 1);
        curPlayerIndex = Mathf.Clamp(curPlayerIndex, 0, playable.Count - 1);

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            Enemy_target_simbol.transform.eulerAngles = ResetTargetRot;
        }



        ChangeEnemyTarget();
    }



    IEnumerator ReSizeBox()
    {
        bool isSmall = false;
        while (true)
        {
            if (!isSmall)
            {
                Enemy_target_simbol.transform.localScale -= new Vector3(0.03f, 0.03f, 0.03f);

                if (Enemy_target_simbol.transform.localScale.x <= 1f)
                {
                    isSmall = true;
                }

                yield return new WaitForSeconds(0.05f);

            }

            if (isSmall)
            {
                Enemy_target_simbol.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);

                if (Enemy_target_simbol.transform.localScale.x >= 1.5f)
                {
                    isSmall = false;
                }

                yield return new WaitForSeconds(0.01f);
            }

            Enemy_target_simbol.transform.Rotate(new Vector3(0, 0, 1f));

        }

    }

    public void ChangePlayerTarget()
    {
        Player_target_simbol.transform.position = new Vector3(playable[curPlayerIndex].transform.position.x, Enemy_target_simbol.transform.position.y, playable[curPlayerIndex].transform.position.z + 0.2f);
        targetPlayerName = playable[curPlayerIndex].name;


    }


    public void ChangeEnemyTarget()
    {
        //PlayerController playerController = FindObjectOfType<PlayerController>();
        Enemy_target_simbol.transform.position = new Vector3(enemys[curEnemyIndex].transform.position.x
            , Enemy_target_simbol.transform.position.y, Enemy_target_simbol.transform.position.z);

        TargetSimbolEnemyTr = Enemy_target_simbol.transform.position; // 공격할 타겟 심볼의  Vector 저장 값임(적 enemy의 위치 저장값이 아님)
        targetEnemyName = enemys[curEnemyIndex].name;
        EnemyTransForm = enemys[curEnemyIndex].transform.position; //이게 없어서 문제가 생겼던거임 너무 돌아갔었음.

    }

    public void SkillStackUse()
    {
        if (SkillStack != 0)
        {
            SkillStack--;
        }

        if (SkillStack < 0)
        {
            SkillStack = 0;

        }
    }

    public void SkillStackAdd()
    {
        SkillStack++;
        if (SkillStack > 5)
        {
            SkillStack = 5;
        }
    }


}

/*

   실험중

   //누구의 턴인지 알 수 없을때(누가 먼저 0 인지 알 수 없을 경우, 리스트 전체의 current값을 0이 될때까지 감소)
   public void TurnTime()
   {
       if (!isUltimateActivate) //궁극기 모드가 아닐경우
       {

           for (int i = 0; i < playable.Count; i++) // 일단 턴 매니저가 모든 플레이어 오브젝트를 감쌈
           {
               //먼저 키를 비교함
               if (playable[i].cureng == playable[i].maxeng)
               {
                   if (Input.GetKeyDown(KeyCode.Alpha1) && playable[0].cureng == playable[0].maxeng) //여기서 누른애 순서랑 오브젝트 순서가 같은지를 같이 비교를 해야됨
                       //일단 전략을 당장 사용할 겨를이 없으니 이렇게 사용합시다.
                   { 
                       isUltimateActivate = true;
                       StopTurn = true;

                       //궁극기가 눌렸다면
                       Debug.Log("1번 키가 눌렸음");
                       playable[0].isUltimate = true;
                   }
                   else if (Input.GetKeyDown(KeyCode.Alpha2) && playable[1].cureng == playable[1].maxeng)
                   {
                       isUltimateActivate = true;

                       StopTurn = true;//일단 턴 제어를 멈춤.

                       playable[1].isUltimate = true;

                       //궁극기가 눌렸다면
                       Debug.Log("2번 키가 눌렸음");
                   }
                   else if (Input.GetKeyDown(KeyCode.Alpha3) && playable[2].cureng == playable[2].maxeng)
                   {
                       isUltimateActivate = true;
                       StopTurn = true;

                       playable[2].isUltimate = true;

                       //궁극기가 눌렸다면
                       Debug.Log("3번 키가 눌렸음");
                   }
                   else if(Input.GetKeyDown(KeyCode.Alpha4) && playable[3].cureng == playable[3].maxeng)
                   {
                       isUltimateActivate = true;
                       StopTurn = true;

                       playable[3].isUltimate = true;

                       //궁극기가 눌렸다면
                       Debug.Log("4번 키가 눌렸음");
                   }
               } // 이 로직들에서 궁극기가 켜짐.
           }



           if (!StopTurn && !isUltimateActivate) //그러면 여기서 뒤에 문장이 false로 바뀜 
           {
               for (int i = 0; i < all_obj.Count; i++)
               {

                   all_obj[i].currentTurnSpeed -= 1f; // 결론적으로 여기에 제동이 걸림.

                   if (all_obj[i].currentTurnSpeed <= 0)
                   {
                       all_obj[i].currentTurnSpeed = 0;

                       //대충 턴 잡고 턴시작되는내용
                       all_obj[i].isMyTurn = true;

                       StopTurn = true;

                       Debug.Log("플레이어 턴 잡힘 " + all_obj[i].name);

                       all_obj[i].currentTurnSpeed = all_obj[i].baseTurnSpeed;

                       break;
                   }
               }
           }
       }

   }*/


// ...
/*
if (Input.GetKeyDown(KeyCode.Alpha1) && playable[0].cureng == playable[0].maxeng)
{
    ReserveUltimate(playable[0]);
}
else if (Input.GetKeyDown(KeyCode.Alpha2) && playable[1].cureng == playable[1].maxeng)
{
    ReserveUltimate(playable[1]);
}else if(Input.GetKeyDown(KeyCode.Alpha3) && playable[2].cureng == playable[2].maxeng)
{
    ReserveUltimate(playable[2]);
}else if(Input.GetKeyDown(KeyCode.Alpha4) && playable[3].cureng == playable[3].maxeng)
{
    ReserveUltimate(playable[3]);
}
// 동일한 방식으로 나머지 플레이어 처리
*/
// ...