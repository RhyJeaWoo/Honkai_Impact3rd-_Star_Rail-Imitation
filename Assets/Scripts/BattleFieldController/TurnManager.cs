using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayerSkillStrategy currentSkill; //전략 인터페이스 할당


    public GameObject Enemy_target_simbol;

    public GameObject Player_target_simbol;

    private static TurnManager instance = null;

    public int SkillStack = 3;

    public GameObject[] StackFullUi = null;
    public GameObject SkillStackCheckimg = null;

    public TextMeshProUGUI text;



    public string targetPlayerName;

    public string targetEnemyName;

    public int PlayerNum = 0;

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
    [SerializeField] public List<PlayerController> playerUltimate = new List<PlayerController>(); //이걸 그냥 먼저 들어온 애들을 순차적으로 저장할 예정임.

    //[SerializeField] public Queue<PlayerController> ultimateQueue = new Queue<PlayerController>(); //폐기예정


    [SerializeField] private int curEnemyIndex = 0;
    [SerializeField] private int curPlayerIndex = 0;

    [Header("궁극기 사용 순서를 나타내기 위해 사용하는 큐")]
    //public Queue<PlayerController> ultimateQueue = new Queue<PlayerController>();

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
        SkillStackCheck();
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




        if (playable[0].cureng == playable[0].maxeng && Input.GetKeyDown(KeyCode.Alpha1))
        {

            // if (Input.GetKeyDown(hotkey))
            //s {
            //if (!playable[i].isMyTurn && !playable[i].isUltimate)

            //ReserveUltimate(playable[0]);
            // 플레이어를 "궁극기 예약 중" 상태로 설정
            playable[0].isUltimate = true; //여기서 궁극기가 사용가능한가.
            playable[0].HandleUltimateReservations(); //플레이어에게 궁극기를 입력하였다고 알려줌
            playerUltimate.Add(playable[0]);
            playable[0].cureng = 0;

            Debug.Log(KeyCode.Alpha1 + "번 키가 눌렸음");
            //여기서 만약 예약만 한다고 치면 

            // }
        }
        else if (playable[1].cureng == playable[1].maxeng && Input.GetKeyDown(KeyCode.Alpha2))
        {

            //ReserveUltimate(playable[1]);
            // 플레이어를 "궁극기 예약 중" 상태로 설정
            playable[1].isUltimate = true; //여기서 궁극기가 사용가능한가.
            playable[1].HandleUltimateReservations();
            playerUltimate.Add(playable[1]);
            playable[1].cureng = 0;
            Debug.Log(KeyCode.Alpha2 + "번 키가 눌렸음");
        }
        else if (playable[2].cureng == playable[2].maxeng && Input.GetKeyDown(KeyCode.Alpha3))
        {

            //ReserveUltimate(playable[2]);
            // 플레이어를 "궁극기 예약 중" 상태로 설정
            playable[2].isUltimate = true; //여기서 궁극기가 사용가능한가.
            playable[2].HandleUltimateReservations();
            playerUltimate.Add(playable[2]);
            playable[2].cureng = 0;
            Debug.Log(KeyCode.Alpha3 + "번 키가 눌렸음");
        }

        else if (playable[3].cureng == playable[3].maxeng && Input.GetKeyDown(KeyCode.Alpha4))
        {

            playable[3].isUltimate = true; //여기서 궁극기가 사용가능한가.
            playable[3].HandleUltimateReservations();
            playerUltimate.Add(playable[3]);
            playable[3].cureng = 0;
            Debug.Log(KeyCode.Alpha4 + "번 키가 눌렸음");
        }

        //   if (ultimateQueue.Count == 0) //궁극기가 예약된 유저가 없을 경우 턴을 속행
        // {

        // 큐가 비어있을 때, 즉 예약된 궁극기가 없을 때에만 플레이어 턴 처리

        if (playerUltimate.Count == 0)
        {

            for (int i = 0; i < all_obj.Count; i++)
            {
                if (!all_obj[i].isUltimate)
                {
                    if (!StopTurn)
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
                        //all_obj[i].IsReservingUltimate = false;


                    }

                    Debug.Log("플레이어 턴 잡힘 " + all_obj[i].name);

                    all_obj[i].currentTurnSpeed = all_obj[i].baseTurnSpeed;
                }
            }
        }
        else
        {

        }

    }

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
    private void SkillStackCheck()
    {
        if (SkillStack <= StackFullUi.Length)
        {
            for (int i = 0; i < StackFullUi.Length; i++)
            {
                if (i < SkillStack)
                {
                    StackFullUi[i].SetActive(true);
                }
                /*else if (SkillStack == 0)
                {
                    StackFullUi[0].SetActive(false);
                }*/
                else
                {
                    StackFullUi[i].SetActive(false);
                }

            }

           
        }

        text.text = SkillStack.ToString();
    }


}


// 여기에서 궁극기 예약 및 큐에 추가 로직을 유지
/*
        if (ultimateQueue.Count > 0) //궁극기를 누른 player오브젝트를 큐에 저장되면, Count가 오름
        {

            isUltimateActivate = true; //궁극기를 누른 플레이어가 있는가?

            for (int i = 0; i < playable.Count; i++)
            {
                if (playable[i].isUltimate)
                {

                    //playable[i].IsReservingUltimate = false;
                }
            }
            //IsReservingUltimate가  playable[i]가 예약했다는 뜻 true로 바뀜
            //이때 여러명이 누를 수 있으니, playerControl이   public bool isUltimate 가 모두 false 일때 IsReservingUltimate가 false로 바뀌어야함.
        }
        else
        {
            isUltimateActivate = false;
        }*/

//그냥 큐 안쓰고 리스트로 해서, 리스트 첫번째 인지 보고 첫번쨰만 실행하고 종료되면 리스트 제거할거임 ㅡㅡ


/*========================================================== 실험중 ===================================================
private void ReserveUltimate(PlayerController player)
{

    // 예약된 궁극기를 대기열에 추가
    ultimateQueue.Enqueue(player);
    // 큐에 추가할 때 디버그 메시지 출력
    Debug.Log(player.name + "의 궁극기 예약이 큐에 추가되었음.");
    실험 로직


    if (!ultimateQueue.Contains(player))
    {
        // 예약된 궁극기를 대기열에 추가
        ultimateQueue.Enqueue(player);
        // 큐에 추가할 때 디버그 메시지 출력
        Debug.Log(player.name + "의 궁극기 예약이 큐에 추가되었음.");
    }
    else
    {
        // 이미 예약되었던 플레이어에 대한 처리 (예를 들어, 이미 예약되었다는 메시지 출력)
        Debug.Log(player.name + "은(는) 이미 궁극기를 예약했습니다.");
    }
}*/
