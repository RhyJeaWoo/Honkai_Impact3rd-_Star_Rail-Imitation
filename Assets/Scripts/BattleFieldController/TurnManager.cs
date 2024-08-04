using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public PlayerSkillStrategy currentSkill; //전략 인터페이스 할당


    public GameObject Enemy_target_simbol; //적 타겟 심볼

    public GameObject Player_target_simbol;//아군 타겟 심볼

    public RectTransform ui_Panel;

    public TurnUIPanelSorter ui_PanelSorter;//TurnUIPanelSorter 클래스 패널에서 상호작용하는 함수들이 내장

    public Camera uiCamera;


    private static TurnManager instance = null; //싱글턴으로 쓰려고 정적변수로 선언

    public int SkillStack = 3; //스킬 스텍

    public GameObject[] StackFullUi = null;
    public GameObject SkillStackCheckimg = null;

    public TextMeshProUGUI text;

    public CinemachineVirtualCamera startCam;//게임시작 카메라 초기 위치

    private StrategyAssigner strategyAssigner;


    public string targetPlayerName;

    public string targetEnemyName;

    public int PlayerNum = 0;

    public bool StopTurn;//true면 턴 중지 이때 IsTurn이 true면 턴 인계로 false됨

    public bool IsTurn;//누군가가 턴 실행 중이다 true

    // 10/24일
    public bool isUltimateActivate; //궁극기가 실행 되는중이라면 true  -> (현재는 이 의미로 사용) 궁극기를 누른 플레이어가 있는가?

    public bool ultimateReserved = false;//궁극기가 예약 되었다면 true

    // 현재 턴을 가진 플레이어를 추적하는 변수
    private PlayerController currentTurnPlayer;


    [SerializeField] private bool isInputEnabled = true; //입력을 막으려고 쓰는 함수

    [SerializeField] private Button[] characterButtons;

    [SerializeField] private InfomationPenel characterPanel; // 패널 인스턴스 직접 할당



    [Header("오브젝트 관련 리스트 및 요소들")]
    [SerializeField] public List<Entity> all_obj = new List<Entity>();
    //현제 게임내에 존재하는 오브젝트 전체의 리스트

    [SerializeField] public List<EnemyAIController> enemys = new List<EnemyAIController>();
    //적들만 구분하기 위해 다시 저장하는 리스트

    [SerializeField] public List<PlayerController> playable = new List<PlayerController>();
    //플레어블만 구분하기 위해 선언된 리스트
    [SerializeField] public List<PlayerController> playerUltimate = new List<PlayerController>(); 
    //이걸 그냥 먼저 들어온 애들을 순차적으로 저장할 예정임.

    [SerializeField] private int curEnemyIndex = 0;
    [SerializeField] private int curPlayerIndex = 0;

    [Header("")]
  

    public Vector3 TargetSimbolEnemyTr; //내가 지정한 적의 위치 앞에 이동하기 위해 저의 좌표를 저장할 벡터3 좌표 변수임
                                        //턴이 끝나면 초기화 시킬거임.

    public Vector3 EnemyTransForm { get; set; } //적의 위치를 저장하는 변수

    public Vector3 TargetSimbolPlTr;

    public Vector3 PlayerTransForm;

    public Transform[] healTarget; // 힐 대상을 저장하는 변수 , 사실상 플레이어들의 트랜스폼을 저장하는 변수임.

    public Transform EnemyTarget; //적의 위치와 그것을 추적할 카메라 트랜스폼

    Vector3 ResetTargetRot;

    public Vector3[] EnemyInitialPosition = null;


    private void Awake()
    {
        SingleTone();

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

    public void SingleTone()
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


    void Start()
    {
        Enemy_target_simbol.SetActive(false);

        startCam.MoveToTopOfPrioritySubqueue();//시작 초기 화면

        ListAddRange();

        ListSort();

        PlayerHealPos();

        //SetStrategy();

        EnemyInitialPos(); //게임 시작시 적들의 초기 위치를 저장(다시 되돌아 가기 위함임)

        TurnUiSet();//패널에 게임에 참여한 오브젝트의 턴 슬롯을 추가

        Enemy_target_simbol.transform.position = new Vector3(enemys[0].transform.position.x, Enemy_target_simbol.transform.position.y, Enemy_target_simbol.transform.position.z);

        ResetTargetRot = Enemy_target_simbol.transform.eulerAngles;

        for (int i = 0; i < playable.Count; i++)
        {
            playable[i].skin[0].enabled = false;
            playable[i].skin[1].enabled = false;
        }

        //전략 패턴 할당

        strategyAssigner = new StrategyAssigner();

        foreach (PlayerController player in playable)
        {
            strategyAssigner.AssignStrategies(player);
        }


        if (characterPanel == null)
        {
            Debug.LogError("Character panel is not assigned in the inspector.");
        }




    }

   

    private void Update()
    {
        if (!isInputEnabled)
        {
            // 입력이 비활성화된 상태에서는 아무 것도 처리하지 않음
            return;
        }

        TargetMove();// 타겟을 정하고 그 타겟이 어디에 있는지 저장하려는 함수

        TurnTime();

        SkillStackCheck();

        InfoEnter();




    }

    private void FixedUpdate()
    {
        //TraceCamPos(); //적의 위치를 카메라가 계속 저장하기 위해 두는 변수임, 일단 테스트 코드임
        //TurnUiSearchSort();
    }




    private void EnemyInitialPos() //적 전투 시 기존 위치 저장
    {
        for (int i = 0; i < enemys.Count && i < EnemyInitialPosition.Length; i++)
        {
            EnemyInitialPosition[i] = enemys[i].transform.position;
        }
    }

    public void SetInputEnabled(bool isEnabled)
    {
        isInputEnabled = isEnabled;
    }

   

    private void InfoEnter()
    {
        if (playable == null || playable.Count == 0)
        {
            Debug.LogWarning("Playables list is either null or empty.");
            return;
        }

        if (Input.GetKeyDown(KeyCode.C)) // C 키를 눌렀을 때
        {
            ShowCharacterInfo(); // 캐릭터 정보 표시
        }
    }

    private void ShowCharacterInfo()
    {
        if (characterPanel != null)
        {
            characterPanel.OpenUIPanel(playable); // 패널 열기 및 버튼 업데이트
        }
        else
        {
            Debug.LogError("InfomationPenel instance not assigned in the inspector.");
        }
    }

    private void ShowEnemyInfo()
    {
        // 적 정보 패널을 여는 로직을 여기에 추가하세요
    }




    public void UltimateEnd()
    {
        isUltimateActivate = false;
    }

    public void ListAddRange() //턴 매니저가 모든 오브젝트들을 리스트에 넣는 과정을 메소드로 제작한것
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

    public void TraceCamPos()
    {

        // EnemyTarget 변수가 초기화되어 있지 않다면 초기화
        if (EnemyTarget == null)
        {
            EnemyTarget = enemys[1].transform;
        } else
        {
            EnemyTarget = enemys[0].transform;
        }

        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i].transform != null && enemys[i].name == targetEnemyName)
            {
                EnemyTarget = enemys[i].transform;
            }
        }
    }

    //플레어블과 오브젝트들을 위치에 따라서 정렬 시키기 위해 사용한 메소드
    public void ListSort()
    {
        List<PlayerController> sortedPlayers = new List<PlayerController>(playable);

        List<EnemyAIController> sortEnemy = new List<EnemyAIController>(enemys);

        sortEnemy.Sort((enemy1, enemy2) => enemy1.transform.position.x.CompareTo(enemy2.transform.position.x));

        enemys = sortEnemy;

        //원하는 순서대로 정렬
        sortedPlayers.Sort((player1, player2) => player1.transform.position.x.CompareTo(player2.transform.position.x));

        //정렬된 리스트를 playerble 리스트에 할당
        playable = sortedPlayers;
    }

    public void TurnTime()
    {
        
        UltimateUse();

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

                    //all_obj[i].currentTurnSpeed = all_obj[i].baseTurnSpeed; //이부분을 따로 해야됨.
                    //상태 패턴에서 넘기는게 맞아보임.
                }
            }
        }
        else
        {

        }

    }

    private void UltimateUse()
    {
        if (playable[0].cureng == playable[0].maxeng && Input.GetKeyDown(KeyCode.Alpha1))
        {


            // 플레이어를 "궁극기 예약 중" 상태로 설정
            playable[0].isUltimate = true; //여기서 궁극기가 사용가능한가.
            playable[0].HandleUltimateReservations(); //플레이어에게 궁극기를 입력하였다고 알려줌
            playerUltimate.Add(playable[0]);
            //playable[0].cureng = 0; //여기서 0으로 처리하면 안됨.

            Debug.Log(KeyCode.Alpha1 + "번 키가 눌렸음");
            //여기서 만약 예약만 한다고 치면 

            // }
        }
        else if (playable[1].cureng == playable[1].maxeng && Input.GetKeyDown(KeyCode.Alpha2))
        {


            // 플레이어를 "궁극기 예약 중" 상태로 설정
            playable[1].isUltimate = true; //여기서 궁극기가 사용가능한가.
            playable[1].HandleUltimateReservations();
            playerUltimate.Add(playable[1]);
            //playable[1].cureng = 0;
            Debug.Log(KeyCode.Alpha2 + "번 키가 눌렸음");
        }
        else if (playable[2].cureng == playable[2].maxeng && Input.GetKeyDown(KeyCode.Alpha3))
        {


            // 플레이어를 "궁극기 예약 중" 상태로 설정
            playable[2].isUltimate = true; //여기서 궁극기가 사용가능한가.
            playable[2].HandleUltimateReservations();
            playerUltimate.Add(playable[2]);
            //playable[2].cureng = 0;
            Debug.Log(KeyCode.Alpha3 + "번 키가 눌렸음");
        }

        else if (playable[3].cureng == playable[3].maxeng && Input.GetKeyDown(KeyCode.Alpha4))
        {

            playable[3].isUltimate = true; //여기서 궁극기가 사용가능한가.
            playable[3].HandleUltimateReservations();
            playerUltimate.Add(playable[3]);
            //playable[3].cureng = 0;
            Debug.Log(KeyCode.Alpha4 + "번 키가 눌렸음");
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
    


    public void TurnUiSet()
    {
        if (ui_PanelSorter == null)
        {
            Debug.LogError("TurnUIPanelSorter is not assigned!");
            return;
        }

        foreach (var entity in all_obj)
        {
            // UI 이미지를 패널에 인스턴스화
            GameObject uiImage = Instantiate(entity.turn_Ui_Image, ui_Panel.transform);

            // 인스턴스화된 UI 이미지의 TextMeshProUGUI 컴포넌트를 Entity의 curspdText에 할당
            TextMeshProUGUI uiText = uiImage.GetComponentInChildren<TextMeshProUGUI>();
            entity.curspdText = uiText;

            if (uiText != null)
            {
                entity.curspdText = uiText;
               //Debug.Log($"Assigned TextMeshProUGUI to entity: {entity.name}");
            }
            else
            {
                //Debug.LogError($"TextMeshProUGUI component not found in {uiImage.name}");
            }
            // 인스턴스화된 UI 이미지를 Entity에 할당
            entity.turn_Ui_Image = uiImage;

            // Entity에 TurnManager를 설정
            entity.SetTurnManager(this);

            // 턴 속도 변경 이벤트 구독
            entity.OnTurnSpeedChanged += OnTurnSpeedChanged;
        }
        // UI 패널 초기 위치 설정 및 정렬 호출
        ui_PanelSorter.SortPanels(all_obj);
    }

    // Entity의 TurnSpeed 변경 시 호출될 메서드
    private void OnTurnSpeedChanged(Entity entity)
    {
        ui_PanelSorter.SortPanels(all_obj);
    }

    public void SortPanels()
    {
        // 패널 정렬 메서드를 호출
        ui_PanelSorter.SortPanels(all_obj);
    }


    


}


