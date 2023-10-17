using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{

  
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public SkinnedMeshRenderer[] skin;

    public CinemachineVirtualCamera vircam;

    public Vector3 toEnemyPos = Vector3.zero; //���� ��ġ

    public Vector3 toPlayerPos = Vector3.zero;//�� ���� ��ġ

    [Header("�÷��̾��� �������ͽ�")]
    public float maxhp;//�ִ� ü��
    public float curhp;//���� ü��

    public float atk;//���� ���ݷ�
    public float def;//���� ����
   
    public float cureng;//���� ������
    public float maxeng;//�ִ� ������

    public float curCrt;//���� ġ��Ÿ ��ġ
    public float criticalPower;//ũ��Ƽ�� ������ ���� ���� 

    public float baseSpeed; // ���� �ӵ�x
    public float buffSpeed; // ������ ������ �� �ִ� �ӵ�
    public float flatSpeed; // ���� ���� �� �ִ� �ӵ�
    public float currentSpeed; // ���� �ӵ�

    public float baseTurnSpeed; // ���� �ൿ ��ġ
    public float currentTurnSpeed; // ���� �ൿ ��ġ




    [Header("�÷��̾��� ������ ���� ����")]
    public float defaultDamage;//ũ��Ƽ�� ���� ������ ���ϴ� ������

    public float norAtkDamage;

    public float criticalDamage;

    public float skillDamage;//��ų���������

    public float increasedDamage;//���ϴ� ���ط� ���� ������

    [Header("����Ѱ�")]

    public float time;//Ư�� ������Ʈ���� �ٷ� ���������� ���� ��Ÿ��.

    public bool isMyTurn; // �� ������
    public bool isAtackOn = false; //���� ���� �غ� ���¿��� ������ �غ� �Ǿ�����
    public bool isSkillOn = false; //���� ��ų �غ� ���¿��� ��ų�� ����� �غ� �Ǿ����� üũ�ϰ� �Ѿ�°ɷ�

    public bool canAct = true; //HP�� 0�� �Ǿ Ȱ���� �� �ִ��� üũ


    // Start �Լ����� �ʱ�ȭ�� �����մϴ�.
    protected virtual void Start()
    {
        skin = GetComponentsInChildren<SkinnedMeshRenderer>();

        // ���� �ӵ� ���    
        float finalSpeed = baseSpeed * (1 + buffSpeed / 100) + flatSpeed;

        // ���� �ൿ ��ġ ���
        baseTurnSpeed = 10000 / finalSpeed;

        // �ʱ�ȭ Ȥ�� �� ���� �� ���� �ӵ��� �ൿ ��ġ�� ����
        currentSpeed = finalSpeed;
        currentTurnSpeed = baseTurnSpeed;

        curhp = maxhp;

        //ũ��Ƽ�� ������ ������   (���ݷ� * ��ų ���) * (1 + ���� ���� ���) 

        defaultDamage = (atk ) * (1 + increasedDamage);

    }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update �Լ����� �� �� �ൿ ��ġ�� ������Ʈ�մϴ�.
    protected virtual void Update()
    {
        time -= Time.deltaTime;
    }


    /*


    // �ٸ� �̺�Ʈ�� ���ǿ� ���� ���� ������ �� ȣ���մϴ�.
    public void StartTurn()
    {
        isMyTurn = true;
        Debug.Log(name + "'s turn started.");
    }

    // ���� �ൿ�� ��ģ �� ȣ���մϴ�.
    public void EndTurn()
    {
        isMyTurn = false;
        currentTurnSpeed = baseTurnSpeed; // ���� �ൿ ��ġ�� �ʱ�ȭ
        canAct = false;
        //Debug.Log(name + "'s turn ended.");
    }*/
}
