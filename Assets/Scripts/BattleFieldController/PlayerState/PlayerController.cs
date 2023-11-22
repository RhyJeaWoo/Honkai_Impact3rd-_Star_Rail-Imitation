using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerController : Entity
{

    public Image eng;

    public Color engColorA;

    public List<PlayerController> playerList = new List<PlayerController>(new PlayerController[4]);
    //좋은 방법이 아님 그냥 턴매니저의 값을 받아와서 그걸로 처리하는게 좋아보임.
    // 삭제 예정, 관련된 모든 리스트 삭제하고, 힐 관련 리스트는 여기서가 아닌, 턴 매니저에서 받아와서 사용할 것.



    //내 시점에서 바라볼 수 있는 플레이어 리스트
    public PlayableDirector[] playableDirector; //각자의 스킬이나, 궁극기를 타임라인에 삽입.


    /* 오디오 클립은 그냥 쓰면 뒤지게 햇갈릴 것이니, 용도에 맞게 정의를 해서 쓰자.
     * 1. 턴을 잡았을 경우[0] 에 할당
     * 2. 스킬이나 공격 전환 중에는 사운드 할당 X
     * 3. 공격 & 스킬 시전 중에 사용 O 공격[1], 스킬[2] 
     * -> 여기서 공격과 스킬이 시전하는 사운드에 수에 따라
     * 적재되는 클립의 수가 다를거니 적절하게 쓸것
     * 
     * 4. 궁극기 시전 중에도 사운드를 사용할 거임 궁극기 대기[3]
     * 5. 히트 당할때 사운드
     * 순서만 지킬것
     * 
     * 
     * 
     */
    public AudioClip[] PlayerVoice; //여기서 타임 라인을 제외한 나머지 사운드들을 사용할 것임.
    
    //이거랑 별개로, 무기 전환 효과를 다른 클립으로 관리 해야될듯?


    #region Design Patterns

    


    //전략
    private PlayerSkillStrategy skillStrategy;
    private PlayerAttackStrategy attackStrategy;
    private PlayerUltimateStrategy ultimateStrategy;

   



    #endregion



    #region States
    public PlayerStateMachine stateMachine { get; private set; }//스테이트 머신

    public PlayerIdleState idleState { get; private set; }//내 턴이 아닐 경우 유지할 상태

    public PlayerTurnGetState turnGetState { get; private set; }//idle에서 턴을 잡았다는 신호를 정하기 위한 상태

    public PlayerAttackWaitState attackWaitState { get; private set; }//공격 대기 상태


    public PlayerAttackState attackState { get; private set; }//공격 상태 

    public PlayerSkillState skillState { get; private set; }//스킬 시전 상태
    public PlayerSkillWaitState skillWaitState { get; private set; }//턴을 받아왔을때의 상태 여기서 선택 상태로 넘어가고 그다음 공격/

    public PlayerTargetToMoveState targetMoveState { get; private set; }//내가 사용한 스킬이 타게팅 공격 스킬일 경우.
    public PlayerComeBackState comeBackState { get; private set; }//내가 만약 적을 향해 이동했을 경우 다시 호출할 상태(제 자리로 가야함)

    public PlayerWhereGiveBuffState whereGiveBuffState { get; private set; } //일단 내가 누구한테 힐을 줄건지
    public PlayerBuffGiveState buffgiveState { get; private set; }// 내가 고른애한테 버프 시전




    public PlayerIsMyUltimateTurnState isMyUltimateTurnState { get; private set; }

    public PlayerUltimateState ultimateState { get; private set; } //궁을 시전한 상태, 어떤 상태에서든 사용이 가능함, 여기서 발사함

    public PlayerBeforeUltimateExcute beforeUltimateExcute { get; private set; } //여기서 타임라인 굴림


    public PlayerUltimateWaitState ultimateWaitState { get; private set; }//궁극기 시전후 대기 상태로 사용 예정. 어떠한 상태에서든 개입 가능함.

    public PlayerUltimateEndState ultimateEndState { get; private set; }// 궁극기 종료후 데미지 정산

    public PlayerTurnEndState turnEndState { get; private set; }//턴이 완전히 종료되고 정리되는 상태 -> 모든 상태에서 반드시 거쳐가는 상태





    public PlayerDeadState deadState { get; private set; }//플레이어의 체력이 0이 되었을때 진입하는 상태

    public PlayerHitState hitState { get; private set; }//피격 당했을 때의 상태.


    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        turnGetState = new PlayerTurnGetState(this, stateMachine, "Idle"); //턴을 겟했지만 Idle로 쓸거임.


        attackWaitState = new PlayerAttackWaitState(this, stateMachine, "AttackWait"); //내 턴에서 어떤 행동을 할지 결정하는 스테이트

        skillWaitState = new PlayerSkillWaitState(this, stateMachine, "SkillWait"); //내 턴이면 맨 처음 시작되는 스테이트




        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        skillState = new PlayerSkillState(this, stateMachine, "Skill"); //내 턴에서 스킬을 선택했을 경우 결정하는 스테이트
        //////////////////////////////////////////////////

        isMyUltimateTurnState = new PlayerIsMyUltimateTurnState(this, stateMachine, "isMyUltimateTurn");

        ultimateWaitState = new PlayerUltimateWaitState(this, stateMachine, "UltimateWait");//내 턴중 선택 상태 전에 유지할 상태(턴 스테이트 이후로 연결될 상태임) ,궁극기 발동시 대기할 모션으로 사용

        beforeUltimateExcute = new PlayerBeforeUltimateExcute(this, stateMachine, "BeforeUltimate"); //궁 실행되기 바로 전 모션

        ultimateState = new PlayerUltimateState(this, stateMachine, "Ultimate"); //궁극기 모션 발동

        ultimateEndState = new PlayerUltimateEndState(this, stateMachine, "UltimateEnd"); //여기서는 카메라나 연출 모션으로 뺄거임. 그리고 종료될때, 여기서 처리 할거임.

        /////////////////////////////////////////////////////

        comeBackState = new PlayerComeBackState(this, stateMachine, "ComeBack");

        idleState = new PlayerIdleState(this, stateMachine, "Idle"); //내 턴이 아닐 경우 대기하는 스테이트(상대 또는 계산을 기다리는 턴)


        deadState = new PlayerDeadState(this, stateMachine, "Dead");//내 HP가 다 달았을 경우 진입할 스테이트(어떤 때라도 발동됨 내턴, 상대턴 구분 없음)
        hitState = new PlayerHitState(this, stateMachine, "Hit");//공격 당했을 경우(Idle)에서 작동됨.


        targetMoveState = new PlayerTargetToMoveState(this, stateMachine, "TargetToMove"); //위치에 있는 적을 향해 움직임


        buffgiveState = new PlayerBuffGiveState(this, stateMachine, "BuffGive"); //버프(힐)을 주는 상태
        whereGiveBuffState = new PlayerWhereGiveBuffState(this, stateMachine, "WhereGiveBuff");

        turnEndState = new PlayerTurnEndState(this, stateMachine, "TurnEnd");

    }

    #endregion


    // 참고로 스타레일 내 방어력 계수는
    // 1 - (내 방어력 / 내 방어력 + 200 + 10 * 적 레벨) 이다. 


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);

        // TurnManager 스크립트로부터 healTarget 배열을 가져와서 playerList 리스트에 저장
        playerList.AddRange(TurnManager.Instance.healTarget.Select(transform => transform.GetComponent<PlayerController>()));
        //Debug.Log(TurnManager.Instance.healTarget.Length);
        
        ListSort(); //한번 인위적으로 정렬 해줌. 

        engColorA = eng.color;

    
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        if(maxeng < cureng)
        {
            cureng = maxeng;
        }

        eng.fillAmount = cureng / maxeng;

        //플레이어 방어력 계수
        //최종 방어계수 = ((현재 레벨 * 100) + 100) / (((전달 받은 상대 레벨 * 10) +200 )  +((현재 레벨 * 10 ) + 200 + 내 현재 방어력));
        defenseCoefficient = ((curLevel * 10) + 200) / (((liciveOpponentLevel * 10) + 200) + ((curLevel * 10) + 200) + def);

        // Debug.Log(stateMachine.currentState);
        //  2200 / 900 + 1000

    }
  

    public void ListSort()
    {
        List<PlayerController> sortedPlayers = new List<PlayerController>(playerList);

        //원하는 순서대로 정렬
        sortedPlayers.Sort((player1, player2) => player1.transform.position.x.CompareTo(player2.transform.position.x));

        //정렬된 리스트를 playerble 리스트에 할당
        playerList = sortedPlayers;
    }


    // 스킬 전략 설정
    // 고민 1 전략안에 전략을 한번더 쓰는 중첩 전략을 쓸것인가...?
    public void SetSkillStrategy(PlayerSkillStrategy strategy)
    {
        skillStrategy = strategy;
    }

    // 공격 전략 설정
    public void SetAttackStrategy(PlayerAttackStrategy strategy)
    {
        attackStrategy = strategy;
    }

    public void SetUltimateStratergy(PlayerUltimateStrategy strategy)
    {
        ultimateStrategy = strategy;
    }



    // 플레이어블 캐릭터의 스킬 실행시 연결될 전략
    // 무늬만 존재하고 사용되지는 않음.
    /*
    public void ExecuteSkill(PlayerController player)
    {
        if (skillStrategy != null)
        {

        }
    }
    public void ExecuteAttack(PlayerController player)
    {
        if (attackStrategy != null) { }
    }
     

    public void ExecuteUltimate(PlayerController player)
    {
        if(skillStrategy != null) 
        {
            
        }
    }*/

    //애니메이션으로 실행 , 이코드 다시한번 다듬어야 될거 같음, 지금은 그냥 이 델리게이트를 가진 애들한테 다 보내는 코드가 되는데,
    //다중 공격이면 그냥 보내면 되긴함. 근데, 일단 단일이거나, 내가 지정해서 때리는거에 있어서, 그 부분이 들어가야 되지 않나 싶음.
    public void SkillDamageEvent() //광역 공격임. 
    {
        if (skillStrategy != null)
        {

            float skillDamage = skillStrategy.ExcuteSkill(this); //전략에서 받아온 정보값을 지역 변수에 저장

            // 데미지 이벤트 발생
            //OnDamageDealt?.Invoke(skillDamage);
            DamageDelegate(skillDamage); //델리게이트에 저장된 지역 변수 값을 전달 하는 원리


        }
    }

    public void SetOnlyOneDmageEvent() //eks
    {
        if(skillStrategy != null)
        {
            float skillDamage = skillStrategy.ExcuteSkill(this);//
            for (int i = 0; i < TurnManager.Instance.enemys.Count; i++)
            {
                if (TurnManager.Instance.targetEnemyName == TurnManager.Instance.enemys[i].name)
                {
                    TurnManager.Instance.enemys[i].HandleDamageDealt(skillDamage);
                    TurnManager.Instance.enemys[i].isDamaged = true;
                }
            }
        }
    }

    //여기서 이걸로 데미지를 전달할거임.

    public void AttackDamageEnvet()
    {
        if (attackStrategy != null)
        {
            float norAtkDamage = attackStrategy.ExcuteAttack(this); //델리게이트에 전략에서 받아온 정보값을 지역 변수에 저장

            // 데미지 이벤트 발생
            //OnDamageDealt?.Invoke(norAtkDamage);
            //DamageDelegate(norAtkDamage);
            for (int i = 0; i < TurnManager.Instance.enemys.Count; i++)
            {
                if (TurnManager.Instance.targetEnemyName == TurnManager.Instance.enemys[i].name)
                {
                    TurnManager.Instance.enemys[i].HandleDamageDealt(norAtkDamage);
                    TurnManager.Instance.enemys[i].isDamaged = true;

                }
            }
        }
    }

    public void UltimateDamageEvent()
    {
        if(skillStrategy != null)
        {
            float ultimateDamage = ultimateStrategy.ExecuteUltimate(this);

            DamageDelegate(ultimateDamage);

            //데미지 이벤트 발생
            //OnDamageDealt?.Invoke(ultimateDamage);
        }
    }

    /*
    public void DeligateLevel()
    {
        //OnLevelDealt?.Invoke(curLevel); //레벨을 전달 해야 계수 계산이 가능함.
        LevelDelegate(curLevel);

    }*/


    public void Heal(float healAmount)
    {
        if (curhp > 0)
        {
            //TurnManager.Instance.healTarget
            curhp += healAmount;

            if (curhp > maxhp)
            {
                curhp = maxhp;
            }
        }
    }

    public void CastHealSkill()
    {
        if (playerList.Count > 0)
        {
            float heal = skillStrategy.ExcuteSkill(this);



            if (TurnManager.Instance.targetPlayerName == playerList[0].name)
            {
                playerList[0].Heal(heal);
                Debug.Log(playerList[0].name + "이(가) 힐을 " + heal + " 만큼 받았습니다.");

            }
            else if (TurnManager.Instance.targetPlayerName == playerList[1].name)
            {
                playerList[1].Heal(heal);
                Debug.Log(playerList[1].name + "이(가) 힐을 " + heal + " 만큼 받았습니다.");
            }
            else if (TurnManager.Instance.targetPlayerName == playerList[2].name)
            {
                playerList[2].Heal(heal);
                Debug.Log(playerList[2].name + "이(가) 힐을 " + heal + " 만큼 받았습니다.");
            }
            else if (TurnManager.Instance.targetPlayerName == playerList[3].name)
            {
                playerList[3].Heal(heal);
                Debug.Log(playerList[3].name + "이(가) 힐을 " + heal + " 만큼 받았습니다.");
            }
            else
            {
                Debug.Log("정보 일치 하는게 없음.");
            }

        }
        else
        {
            Debug.LogWarning("Heal target is not set.");
        }
    }



    public void HandleUltimateReservations()
    {
        // 궁극기 예약 처리 로직 추가
        // ...
        IsReservingUltimate = true; //이 캐릭터가 예약 되었는가?

        engColorA.a = 1f; //알베도를 최대값으로 바꿈

        eng.color = engColorA;//알베도를 변경해서 궁극기 예약 상태로 만듬.

        Debug.Log(name + "이(가) 궁극기를 예약했습니다.");
    }


    /* ========================================================여기부터 실험내용 ============================================ */

    /* 용도 => 광역 공격을 받았을시엔 델리게이트를 쓰지만 단일 공격이면 이 함수 호출하면됨. */
    public void HandleDamageDealt(float damage)//
    {
        SumDamage = damage * defenseCoefficient;

        curhp = curhp - SumDamage;

        TakeDamageText((int)SumDamage);

        // 이 메서드에서 데미지 값을 받아 처리
        // Debug.Log("데미지를 전달 받았습니다!" + damage);
        // Debug.Log("현재 방어율 계수 : " + defenseCoefficient);
        // Debug.Log("현재 레벨" + curLevel);

        Debug.Log(transform.name + "데미지를 받았습니다: " + SumDamage);
        //  Debug.Log("현재 hp : " + curhp);

    }//전달하는 방법도 간단함.

    /* 용도 -> 상대방이 공격할때, 데미지 를 산정하기전 내 방어율 계산을 위한 방어계수 단일 공격이면 이 함수 호출하면됨.*/
    public void HandleLevelDealt(float level)
    {

        Debug.Log("전달 받은 Level은 " + level);

        liciveOpponentLevel = level;
    }


    // 이벤트 구독
    public void SubscribeToPlayerDamageEvent()
    {
        //  PlayerController[] playerController = FindObjectsOfType<PlayerController>(); // 혹은 다른 방식으로 플레이어 컨트롤러를 찾습니다.

        EnemyAIController[] enemys = FindObjectsOfType<EnemyAIController>(); // 모든 플레이어 컨트롤러를 찾습니다.

        if (enemys != null)
        {
            foreach (EnemyAIController enemy in enemys)
            {
                //playerController.OnDamageDealt += HandleDamageDealt;
                enemy.OnLevelDealt += HandleLevelDealt;
                enemy.OnDamageDealt += HandleDamageDealt;
            }
        }
        else
        {
            Debug.LogWarning("적 컨트롤러를 찾을 수 없습니다.");
        }

    }

    // 이벤트 구독 해제
    public void UnsubscribeFromPlayerDamageEvent()
    {
        EnemyAIController enemy = FindObjectOfType<EnemyAIController>();
        if (enemy != null)
        {
            //playerController.OnDamageDealt -= HandleDamageDealt;
            enemy.OnLevelDealt -= HandleLevelDealt;
            enemy.OnDamageDealt -= HandleDamageDealt;
        }


    }


    public void SetDamage(float damage)
    {
        curhp -= damage;
    }

}
