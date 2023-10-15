using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject target_simbol;

    private static TurnManager instance = null;
    bool StopTurn;
    bool isPlayerTurn;

    [SerializeField] private List<Entity> all_obj = new List<Entity>();
    //현제 게임내에 존재하는 오브젝트 전체의 리스트

    [SerializeField] private List<EnemyAIController> enemys = new List<EnemyAIController>();
    //적들만 구분하기 위해 다시 저장하는 리스트

    [SerializeField] private List<PlayerController> playable = new List<PlayerController>();
    //플레어블만 구분하기 위해 선언된 리스트

    [SerializeField] private int curIndex = 0;


    Vector3 TargetEnemyTranform; //내가 지정한 적의 위치 앞에 이동하기 위해 저의 좌표를 저장할 벡터3 좌표 변수임
                                 //턴이 끝나면 초기화 시킬거임.

    Vector3 PlayerTranfrom; //내가 공겨한 이후 내 원래 위치로 다시 돌아기 위한 벡터3 좌표임.
                            //공격 시전 전에 내 위치를 먼저 저장하고 적 위치앞으로 이동한 다음 공격이 끝나면
                            //내 자리로 다시 돌아오게 그리고 이 값을 초기화 해서 돌려 쓸거임.

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

    public void InputKey()
    {

    }

    public void PlayerAttack()
    {
        //EndPlayerTurn(players[currentPlayerIndex]);   
    }

    public void PlayerSkill()
    {
        //EndPlayerTurn(players[currentPlayerIndex]);
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

        if (curIndex < 0)
        {
            curIndex = 0;



        }
        else if (curIndex == enemys.Count)
        {
            curIndex = enemys.Count - 1;


        }

        if ( Input.GetKeyUp(KeyCode.D)|| Input.GetKeyUp(KeyCode.A))
        {
            target_simbol.transform.eulerAngles = ResetTargetRot;
        }


        ChangeTarget();

    }

    public void ChangeTarget()
    {
        target_simbol.transform.position = new Vector3(enemys[curIndex].transform.position.x+0.1f, target_simbol.transform.position.y, target_simbol.transform.position.z);

        TargetEnemyTranform = target_simbol.transform.position; //공격할 적 위치 저장
        //여기에다가 플레이어 위치도 같이 저장할지 고민중.

        Debug.Log("ChangeTarget은 실행 되었음.");
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





    ///////////////////////////////////////////////////////////////// 필요없으면 삭제할 코드들 /////////////////////////////////////////////////////////////////////
    ///

    /* 사용 보류
    public void WhoisTurn(GameObject obj)
    {
        //먼저 여기서 플레이어의 턴인지 아니면, 플레이어가 아닌지에 대해서부터 구별이 필요함.


        if (obj.GetComponent<PlayerController>() != null) //플레이어라면? 여기서 UI이동 제어권을 얻을거임.
        {
            //나 말고 적 오브젝트를 위치를 받아야되고 target_simbol 를 띄워서 걔 위치를 표시해야하며,
            //키 입력시 걔 위치로 이동해서 공격을 해야됨. got it?
            if(Input.GetKeyDown(KeyCode.A)) 
            {
                curIndex--;
                if(curIndex < 0) 
                {
                    curIndex = 0;

                    ChangeTarget();
                }

                Debug.Log("키 입력은 됬음");
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                curIndex++;
                if(curIndex > enemys.Count)
                {
                    curIndex = enemys.Count -1;
                    
                    ChangeTarget();
                }
                Debug.Log("키 입력은 됬음");
            }
         

        }
        else
        {

            Debug.Log("플레이어 턴이 아니라 반환 되었음!");
            return;
          
        }


    }*/

    /*
    //모든 플레이어 리스트에 넣고 저장할거임
    private void AllObjectList()
    {


        Entity nextPlayer = new Entity(); ;
        //한줄 함수 실행법

        if (nextPlayer != null)
        {
      
            currentObjectIndex = players.IndexOf(nextPlayer);
            //nextPlayer.StartTurn();
            //Debug.Log(nextPlayer.name + " has the next turn.");
        }
    }

    // FindAllPlayer();
    
    private Entity FindAllPlayer()
    {
        Entity nextPlayer = null;
        // float lowestTurnSpeed = float.MaxValue;

        // Debug.Log(lowestTurnSpeed);

        foreach (Entity player in players)
        {
            //일단 리스트에 다 넣어야되니까
        }

        return nextPlayer;
    }

    private void AllEnemyList()
    {
        BaseEnemy enemy = new BaseEnemy();

        if(enemy != null) 
        {
            currentEnemyIndex = enemys.IndexOf(enemy);
        }


    }
    */


}
