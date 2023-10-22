using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayerSkillStrategy currentSkill; //전략 인터페이스 할당
    private PlayerAttackStrategy currentAttack;

    public GameObject target_simbol;

    private static TurnManager instance = null;

    [SerializeField] private int SkillStack = 3;

    public bool StopTurn;

    public string targetPlayerName;

    public string targetEnemyName;

    public bool isUltimateActivate;


    [Header("오브젝트 관련 리스트 및 요소들")]
    [SerializeField] private List<Entity> all_obj = new List<Entity>();
    //현제 게임내에 존재하는 오브젝트 전체의 리스트

    [SerializeField] public List<EnemyAIController> enemys = new List<EnemyAIController>();
    //적들만 구분하기 위해 다시 저장하는 리스트

    [SerializeField] public List<PlayerController> playable = new List<PlayerController>();
    //플레어블만 구분하기 위해 선언된 리스트

    [SerializeField] private int curEnemyIndex = 0;
    [SerializeField] private int curPlayerIndex = 0;




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

        target_simbol.SetActive(false);
    }


    void Start()
    {


        ListAddRange();

        ListSort();

        PlayerHealPos();

        SetStrategy();


        target_simbol.transform.position = new Vector3(enemys[0].transform.position.x, target_simbol.transform.position.y, target_simbol.transform.position.z);

        ResetTargetRot = target_simbol.transform.eulerAngles;
    }


    private void Update()
    {
        TurnTime(); //현재 행동 수치가 누가 먼저 0으로 도달하는지 체크하기 위한 함수

        TargetMove();// 타겟을 정하고 그 타겟이 어디에 있는지 저장하려는 함수



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

    /*
    public void EnemyPos()
    {
        enemys = new Transform[enemys.Count];
    }*/

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


    /*
    public void PlayerSkill()
    {
        // 현재 플레이어의 스킬 실행
        PlayerController currentPlayer = playable[curEnemyIndex];
        currentPlayer.ExecuteSkill(currentPlayer);
    }

    public void PlayerAttack()
    {
        // 현재 플레이어의 공격 실행
        PlayerController currentPlayer = playable[curEnemyIndex];
        currentPlayer.ExecuteAttack(currentPlayer);

    }*/

    //누구의 턴인지 알 수 없을때(누가 먼저 0 인지 알 수 없을 경우, 리스트 전체의 current값을 0이 될때까지 감소)
    public void TurnTime()
    {
        if (!isUltimateActivate)
        {

         

            for (int i = 0; i < playable.Count; i++)
            {
                //먼저 키를 비교함
                if (playable[i].cureng == playable[i].maxeng)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1) && playable[0].cureng == playable[0].maxeng) //여기서 누른애 순서랑 오브젝트 순서가 같은지를 같이 비교를 해야됨
                    {
                        isUltimateActivate = true;
                        StopTurn = true;

                        //궁극기가 눌렸다면
                        Debug.Log("키가 눌렸음");
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2) && playable[1].cureng == playable[1].maxeng)
                    {

                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3) && playable[2].cureng == playable[2].maxeng)
                    {

                    }
                    else if(Input.GetKeyDown(KeyCode.Alpha4) && playable[3].cureng == playable[3].maxeng)
                    {

                    }
                }
            }



            if (!StopTurn && !isUltimateActivate)
            {
                for (int i = 0; i < all_obj.Count; i++)
                {

                    all_obj[i].currentTurnSpeed -= 1f;

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
            target_simbol.transform.eulerAngles = ResetTargetRot;
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
                target_simbol.transform.localScale -= new Vector3(0.03f, 0.03f, 0.03f);

                if (target_simbol.transform.localScale.x <= 1f)
                {
                    isSmall = true;
                }

                yield return new WaitForSeconds(0.05f);

            }

            if (isSmall)
            {
                target_simbol.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);

                if (target_simbol.transform.localScale.x >= 1.5f)
                {
                    isSmall = false;
                }

                yield return new WaitForSeconds(0.01f);
            }

            target_simbol.transform.Rotate(new Vector3(0, 0, 1f));

        }

    }

    public void ChangePlayerTarget()
    {
        target_simbol.transform.position = new Vector3(playable[curPlayerIndex].transform.position.x, target_simbol.transform.position.y, playable[curPlayerIndex].transform.position.z + 0.2f);
        targetPlayerName = playable[curPlayerIndex].name;


    }


    public void ChangeEnemyTarget()
    {
        //PlayerController playerController = FindObjectOfType<PlayerController>();
        target_simbol.transform.position = new Vector3(enemys[curEnemyIndex].transform.position.x
            , target_simbol.transform.position.y, target_simbol.transform.position.z);

        TargetSimbolEnemyTr = target_simbol.transform.position; // 공격할 타겟 심볼의  Vector 저장 값임(적 enemy의 위치 저장값이 아님)
        targetEnemyName = enemys[curEnemyIndex].name;
        EnemyTransForm = enemys[curEnemyIndex].transform.position;

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