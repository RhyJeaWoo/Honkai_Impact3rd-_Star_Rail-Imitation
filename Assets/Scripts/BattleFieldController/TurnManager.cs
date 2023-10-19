using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayerSkillStrategy currentSkill; //전략 인터페이스 할당
    private PlayerAttackStrategy currentAttack;


    private Transform healTarget; // 힐 대상을 저장하는 변수

    public GameObject target_simbol;

    private static TurnManager instance = null;

    [SerializeField] private int SkillStack = 3;

    public bool StopTurn;


    //private Transform healTarget; // 힐 대상을 저장하는 변수


    //bool isPlayerTurn; 쓰이지도 안흔ㄴ거 일단 보류



    [SerializeField] private List<Entity> all_obj = new List<Entity>();
    //현제 게임내에 존재하는 오브젝트 전체의 리스트

    [SerializeField] public List<EnemyAIController> enemys = new List<EnemyAIController>();
    //적들만 구분하기 위해 다시 저장하는 리스트

    [SerializeField] public List<PlayerController> playable = new List<PlayerController>();
    //플레어블만 구분하기 위해 선언된 리스트

    [SerializeField] private int curIndex = 0;


    public Vector3 TargetEnemyTranform; //내가 지정한 적의 위치 앞에 이동하기 위해 저의 좌표를 저장할 벡터3 좌표 변수임
                                 //턴이 끝나면 초기화 시킬거임.



  //  public Vector3 PlayerTranfrom; //내가 공겨한 이후 내 원래 위치로 다시 돌아기 위한 벡터3 좌표임.
                            //공격 시전 전에 내 위치를 먼저 저장하고 적 위치앞으로 이동한 다음 공격이 끝나면
                            //내 자리로 다시 돌아오게 그리고 이 값을 초기화 해서 돌려 쓸거임.   <--- 써봤는데, 존나 나쁜 방법임. Entity에서 직접 쓰자 뭔가 꼬임.
                            
    

    Vector3 ResetTargetRot;


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

       target_simbol.SetActive(false);
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



    void Start()
    {
        all_obj.AddRange(FindObjectsOfType<Entity>());
        enemys.AddRange(FindObjectsOfType<EnemyAIController>());

        playable.AddRange(FindObjectsOfType<PlayerController>());

        target_simbol.transform.position = new Vector3(enemys[0].transform.position.x,target_simbol.transform.position.y, target_simbol.transform.position.z);

        ResetTargetRot = target_simbol.transform.eulerAngles;


        //이 필드에 존재하는 플레이어들에게 전략 할당
        foreach (PlayerController player in playable)
        {
            PlayerSkillStrategy skillStrategy = null;
            PlayerAttackStrategy attackStrategy = null;

           

            if (player.CompareTag("Mei"))
            {
                skillStrategy = new MeiSkill();
                attackStrategy = new MeiAttack();
                
            }
            else if (player.CompareTag("Kiana"))
            {
                skillStrategy = new KianaSkill();
                attackStrategy = new KianaAttack();
               
            }
            else if (player.CompareTag("Elysia"))
            {
                skillStrategy = new ElysiaSkill();
                attackStrategy = new ElysiaAttack();
               
            }
            else if (player.CompareTag("Durandal"))
            {
                skillStrategy = new DurandalSkill();
                attackStrategy = new DurandalAttack();

            }

            if (skillStrategy != null)
            {
                player.SetSkillStrategy(skillStrategy);
                player.SetAttackStrategy(attackStrategy);
            }
        }


        //enemys.AddRange(FindObjectsOfType<BaseEnemy>());

        //AllObjectList();
        //AllEnemyList();
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

        TurnTime(); //현재 행동 수치가 누가 먼저 0으로 도달하는지 체크하기 위한 함수

        TargetMove();// 타겟을 정하고 그 타겟이 어디에 있는지 저장하려는 함수



    }
    public void PlayerSkill()
    {
        // 현재 플레이어의 스킬 실행
        PlayerController currentPlayer = playable[curIndex];
        currentPlayer.ExecuteSkill(currentPlayer);
    }

    public void PlayerAttack() 
    {
        // 현재 플레이어의 공격 실행
        PlayerController currentPlayer = playable[curIndex];
        currentPlayer.ExecuteAttack(currentPlayer);
    
    }

   

    //누구의 턴인지 알 수 없을때(누가 먼저 0 인지 알 수 없을 경우, 리스트 전체의 current값을 0이 될때까지 감소)
    public void TurnTime()
    {
        if (!StopTurn)
        {
            for (int i = 0; i < all_obj.Count; i++)
            {

                all_obj[i].currentTurnSpeed -= 1f;

                if (all_obj[i].currentTurnSpeed <= 0)
                {
                    all_obj[i].currentTurnSpeed = 0;

                    //대충 턴 잡고 턴시작되는내용
                    all_obj[i].isMyTurn = true;
                    //적 오브젝트도 여기서 처리하고 
                    //ai로 처리는 상태에서 구현


                    StopTurn = true;

                

                    Debug.Log("플레이어 턴 잡힘 " + all_obj[i].name);

                    //WhoisTurn(all_obj[i].gameObject);

                    all_obj[i].currentTurnSpeed = all_obj[i].baseTurnSpeed;


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


    public void TargetMove()
    {


        if (Input.GetKeyDown(KeyCode.A))
        {
            curIndex--;
          

            Debug.Log("키 입력은 됬음");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            curIndex++;
          
            Debug.Log("키 입력은 됬음");

        }

        /*
        if (curIndex < 0)
        {
            curIndex = 0;



        }
        else if (curIndex == enemys.Count)
        {
            curIndex = enemys.Count - 1;


        }*/

        // curIndex 범위 검사
        curIndex = Mathf.Clamp(curIndex, 0, enemys.Count - 1);

        if ( Input.GetKeyUp(KeyCode.D)|| Input.GetKeyUp(KeyCode.A))
        {
            target_simbol.transform.eulerAngles = ResetTargetRot;
        }


        // 이제는 ChangeTarget 함수를 호출하지 않고 선택된 적 캐릭터의 Transform을 PlayerController의 target 변수에 할당합니다
        if (curIndex >= 0 && curIndex < enemys.Count)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                if(playerController.CompareTag("Elysia"))
                {
                    playerController = playable[curIndex];
                    target_simbol.transform.position = playerController.transform.position;
                }
                else
                {
                    var enemyController = enemys[curIndex];
                    target_simbol.transform.position = playerController.transform.position;

                    playerController.target = enemyController.transform; // 수정된 부분
                    TargetEnemyTranform = enemyController.transform.position; // 수정된 부

                }

                //playerController.target = enemys[curIndex].transform;

                //TargetEnemyTranform = enemys[curIndex].transform.position;

                //playerController.target = playerController.transform;
                //TargetEnemyTranform = playerController.transform.position;

            }
        }
        //ChangeTarget();
    }

    public void SetHealTarget(Transform target)
    {
        healTarget = target;
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




    //일단 사용 보류
    public void ChangeTarget()
    {
        target_simbol.transform.position = new Vector3(enemys[curIndex].transform.position.x+0.0f, target_simbol.transform.position.y, target_simbol.transform.position.z);

        TargetEnemyTranform = target_simbol.transform.position; //공격할 적 위치 저장
        //여기에다가 플레이어 위치도 같이 저장할지 고민중.

        //Debug.Log("ChangeTarget은 실행 되었음.");
    }



}
