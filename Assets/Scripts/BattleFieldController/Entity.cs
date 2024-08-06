using Cinemachine;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum property //강인도 시스템에 사용될 속성(캐릭터의 기본 속성과 적의 약점을 위해 사용될거임)
{
    fire, //불 
    thunder, //번개
    physical, //물리
    ice, //빙결
    quantum, //양자
    imaginary, //허수
    without_weakness//약점이 없는 상태(주로 몬스터가 사용)

    //위 상태들은 약점 격파시 특정 상태로 진입하기 위해
};

public class Entity : MonoBehaviour
{
    private TurnManager turnManager;


    public Image hp;


    
    public event Action<Entity> OnTurnSpeedChanged;

    //델리게이트
    public delegate void DamageDealtHandler(float damage); //데미지에 관한 델리게이트로 선언
    public event DamageDealtHandler OnDamageDealt;//델리게이트

    public delegate void LevelDealtHandler(float level); //레벨에 관한 델리게이트
    public event LevelDealtHandler OnLevelDealt;

    public delegate void PropertyDealtHandler(property equal);//속성에 관한 델리게이트(내 속성을 쏴서, 적 속성하고 일치하는지 판단할거임)
    public event PropertyDealtHandler OnPropertyDealt;

    public delegate void StrongGaugeHandler(float strongGauge);
    public event StrongGaugeHandler OnStrongGaugeDealt;

    public  property character_attributes;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public AnimPlay anims; //여기서 모순 발생.
     
    public SkinnedMeshRenderer[] skin;

    public CinemachineVirtualCamera[] vircam;

    public Vector3 toEnemyPos = Vector3.zero; //적의 위치

    public Vector3 toPlayerPos = Vector3.zero;//내 원래 위치

    public Vector3 TargetEnemyTranform { get; set; } // TargetEnemyTranform을 속성으로 추가

    public GameObject hudDamageText;

    public Transform hudPos;

    public GameObject turn_Ui_Image;

    public RectTransform turnUiImageRectTransform => turn_Ui_Image.GetComponent<RectTransform>(); // RectTransform 가져오기

    public TextMeshProUGUI curspdText;

    public Sprite CharSprite;





    [Header("플레이어의 스테이터스")]
    public string names;
    public float curLevel;//현재 레벨
    public float maxLevel;//최대 레벨

    public float maxhp;//최대 체력
    public float curhp;//현재 체력

    public float atk;//현재 공격력
    public float def;//현재 방어력

    public float cureng;//현재 에너지
    public float maxeng;//최대 에너지

    public float curCrt;//현재 치명타 수치
    public float criticalPower;//크리티컬 데미지 증가 배율 기본이 0.5임
    public float baseCrtPow = 0.5f;
    public float addCrtPow;

    public float baseSpeed; // 기초 속도x
    public float buffSpeed; // 버프로 증가할 수 있는 속도
    public float flatSpeed; // 장비로 받을 수 있는 속도
    public float currentSpeed; // 현재 속도

    public float finalSpeed;

    public float baseTurnSpeed; // 기초 행동 수치
    public float currentTurnSpeed; // 현재 행동 수치

    public int curSpeedText;



    [Header("이 오브젝트가 가지는 상태이상")]
    public bool isBurned = false;//화상 상태 - 불
    public bool isElectrocuted = false;//감전 상태 - 번개
    public bool isFreeze = false;//빙결 상태 - 빙결
    public bool isLaceration = false;//열상 상태 - 물리
    public bool is_Be_Quantized = false;//얽힘 상태 - 양자
    public bool is_Be_In_bondage = false;//속박 상태 - 허수
    public bool isStun = false;//기절 상태

    [Header("이 오브젝트가 받은 공격 속성의 종류")] 
    //이 bool값으로 내 상태가 어떤 상태에서 격파를 당했는지 체크하고 거기에 맞는상태로 넘어갈거임
    public bool isFireDamaged = false;
    public bool isIceDamaged = false;
    public bool isThunderDamaged = false;
    public bool isPhysicalDamaged = false;
    public bool isQuantumDamaged = false;
    public bool isImaginary = false;



    [Header("플레이어의 데미지 관련 정리")]
    public float defaultDamage;//크리티컬 판정 전으로 가하는 데미지

    public float norAtkDamage;

    public float criticalDamage;

    public float skillDamage;//스킬계수데미지

    public float increasedDamage;//가하는 피해량 증가 데미지

    public float SumDamage;//내가 받은 총 데미지의 합.

    public float defenseCoefficient;//방어 계수
    public float liciveOpponentLevel;//전달 받은 상대 레벨 계수
    public float ignoredDefense;//방어 무시 받는 계수

    //public float strongGaugePower = 0;

    [Header("플레이어의 힐 관련 정리")]
    public float sumHeal; //힐 총합 수치

   
   

    [Header("잡다한거")]

    public float time;//특정 스테이트에서 바로 빠져나가기 위한 쿨타임.

    public bool isDamaged=false;//데미지를 입었는가?

    public bool isMyTurn=false; // 내 턴인지를 체크하는 변수
  
    public bool isUltimate = false; //내가 궁극기 버튼을 눌렀는가에 대한 체크

    public bool IsReservingUltimate = false; //궁극기가 예약되었는가?

    public bool firstUltimate = false; //이 변수는 가장 먼저 첫번째로 도달한 애가 있는지를 체크하는 함수임.

    public bool canAct = true; //HP가 0이 되어서 활동할 수 있는지 체크

    //이게 True를 받을 경우 새로운 상태로 진입.

    //public bool StopTurn;
    // public bool isAtackOn = false; //내가 공격 준비 상태에서 공격할 준비가 되었는지 체크
    //public bool isSkillOn = false; //내가 스킬 준비 상태에서 스킬을 사용할 준비가 되었는지 체크
    //되었는지 체크하고 넘어가는걸로

    // Start 함수에서 초기화를 수행합니다.

    protected virtual void Start()
    {
        skin = GetComponentsInChildren<SkinnedMeshRenderer>();

        //curspdText = turn_Ui_Image.GetComponentInChildren<TextMeshProUGUI>();//여기서 문제 발생하는듯

        ObjectStatCal();


    }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update 함수에서 턴 및 행동 수치를 업데이트합니다.
    protected virtual void Update()
    {
        time -= Time.deltaTime;

        hp.fillAmount = curhp / maxhp;

        curSpeedText = Mathf.RoundToInt(currentTurnSpeed);

        if (curspdText.text != curSpeedText.ToString())
        {
            curspdText.text = curSpeedText.ToString();
            OnTurnSpeedChanged?.Invoke(this);
        }
        //curspdText.text = curSpeedText.ToString();
    }

    public void SetTurnManager(TurnManager manager)
    {
        turnManager = manager; 
    }

    // 속도를 업데이트 해서 UI턴 정렬을 이벤트로 보내는 함수
    public void UpdateSpeed(float newSpeed)
    {
        if (currentTurnSpeed != newSpeed)
        {
            Debug.Log($"Updating speed from {currentTurnSpeed} to {newSpeed} for {gameObject.name}");

            currentTurnSpeed = newSpeed;
            OnTurnSpeedChanged?.Invoke(this);
        }
    }

    // 속도를 업데이트 해서 남은 속도를 상시 갱신하는것을 이벤트하는 함수

    public void UpdateTurnSpeed(float newSpeed)
    {
        currentTurnSpeed = newSpeed;
        curspdText.text = currentTurnSpeed.ToString();
        turnManager.SortPanels();
    }


    public void ObjectStatCal() //초기 수치 계산
    {
        // 최종 속도 계산    
        finalSpeed = baseSpeed * (1 + buffSpeed / 100) + flatSpeed;
        // 기초 행동 수치 계산
        baseTurnSpeed = 10000 / finalSpeed;
        // 초기화 혹은 턴 시작 시 현재 속도와 행동 수치를 설정
        currentSpeed = finalSpeed;

        currentTurnSpeed = baseTurnSpeed;

        //curhp = maxhp;

        //크리티컬 증가량 계산

        criticalPower = baseCrtPow + addCrtPow;

        //크리티컬 적용전 데미지   (공격력 * 스킬 계수) * (1 + 피해 증가 배수) 
        defaultDamage = (atk) * (1 + increasedDamage);
    }

    /*구조체 관련 함수 테스트*/
    public PlayerStruct GetCharacterInfo()
    {
        return new PlayerStruct(this);
    }

    public void SetCharacterInfo(PlayerStruct info)
    {
        info.ApplyToEntity(this);
    }

    //여기까지가 구조체


 

    public void DamageDelegate(float damage)
    {
        OnDamageDealt?.Invoke(damage);
    }

    public void LevelDelegate() 
    {
        OnLevelDealt?.Invoke(curLevel);
    }

    public void PropertyDelegate()
    {
        OnPropertyDealt?.Invoke(character_attributes);
    }

    
    public void StrongGaugeDelegate(float strongGaugePower)
    {
        OnStrongGaugeDealt?.Invoke(strongGaugePower);
    }
    
    public void TakeDamageText(int damage)
    {
        float x = UnityEngine.Random.Range(-1, 2);

        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPos.position + new Vector3(x, 1f,0); // 표시될 위치
        hudText.GetComponent<DamageText>().damage = damage; // 데미지 전달
        //base.TakeDamage(damage);
    }
    
}
