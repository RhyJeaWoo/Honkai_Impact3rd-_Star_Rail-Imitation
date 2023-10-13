using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�⺻���� �÷��̾��� �̵��� ������ ����.

    //ī�޶� ȸ���� ���� ��ǥ��
    public Transform body;
    public Transform cameraArm;
    public Animator anim;
    public GameObject atkEffect;

    //�̵��ӵ�
    public float speed;

    //������� ī�޶� public���� ���� (ī�޶� ��)���� ���̱� ����
    public Camera follwowCamera;


    //�̵��ϱ� ���� ����
    float hAxis;
    float vAxis;

    //����(���� ����)Ű�� ����ϱ� ���� ����
    //Ű�� ���콺 ���ݰ� E Ű�θ� ����
    bool atkDown;
    bool skillDown;
    bool rDown;

    Vector3 moveVec; // �̵� ����



    //�̵��Լ�
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // ���Ͱ��� ����ȭ




        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;//�ٶ󺸴� ���� ����ȭ
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized; //ī�޶� ����� ����ȭ
        Vector3 moveDir = lookForward * moveVec.z + lookRight * moveVec.x;//ī�޶� �̵��� ĳ���� �̵��� ��ġ ��Ű�����ؼ� ����


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
        // ī�޶��� ���� ������ ���Ϸ� ������ ����
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        // ī�޶� �� ȸ�� ��Ű��
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
        // anim.SetBool("atkDown", atkDown); //Ʈ���ŷ� ����, Ű �Է� ���� ���� Ǯ��������. �ƴϸ�, �ڷ�ƾ���� on/ off �����ϴµ�, ������. ����
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


        //�̵� �Լ�
        Move();
        //ȸ�� �Լ�
        Turn();


        NorAttack();
        Skill();



    }
}
