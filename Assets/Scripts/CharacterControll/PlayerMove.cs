using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //기본적인 플레이어의 이동을 제어할 것임.

    //카메라 회전을 위한 좌표값
    public Transform body;
    public Transform cameraArm;
    public Animator anim;
    public GameObject atkEffect;

    //이동속도
    public float speed;

    //따라오는 카메라를 public으로 선언 (카메라 암)으로 붙이기 위함
    public Camera follwowCamera;


    //이동하기 위한 변수
    float hAxis;
    float vAxis;

    //공격(전투 진입)키를 사용하기 위한 변수
    //키는 마우스 공격과 E 키로만 제어
    bool atkDown;
    bool skillDown;
    bool rDown;

    Vector3 moveVec; // 이동 벡터



    //이동함수
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 벡터값을 정규화




        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;//바라보는 방향 정규화
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized; //카메라 우방향 정규화
        Vector3 moveDir = lookForward * moveVec.z + lookRight * moveVec.x;//카메라 이동과 캐릭터 이동을 일치 시키기위해서 세팅


        if (moveVec.magnitude != 0f)
        {
            body.forward = moveDir;
        }

        transform.position += moveDir * speed  * Time.deltaTime;

        anim.SetBool("Move", moveVec != Vector3.zero);
        anim.SetBool("isRun", rDown);

        cameraArm.transform.position = body.transform.position;

    }

    void Turn()
    {
        var dir = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            dir -= 2f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            dir += 2f;
        }
        Vector2 mouseDelta = new Vector2(dir, 0f);
        // 카메라의 원래 각도를 오일러 각으로 저장
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        // 카메라 암 회전 시키기
        cameraArm.rotation = Quaternion.Euler(camAngle.x, camAngle.y + mouseDelta.x, camAngle.z);

    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        atkDown = Input.GetButton("Fire1");
        skillDown = Input.GetKey(KeyCode.R);
        rDown = Input.GetButton("Run");



    }

    void NorAttack()
    {
        // anim.SetBool("atkDown", atkDown); //트리거로 쓰자, 키 입력 받자 마자 풀려버린다. 아니면, 코루틴으로 on/ off 제어하는데, 귀찮음. ㅋㅋ
        if (atkDown)
            anim.Play("NormalAtk");

    }

 

    void Skill()
    {
       // anim.SetBool("SkillDown", skillDown);
       if(skillDown)
            anim.Play("SkillAtk");
    }

    void InBattle()
    {

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();


        //이동 함수
        Move();
        //회전 함수
        Turn();


        NorAttack();
        Skill();



    }
}
