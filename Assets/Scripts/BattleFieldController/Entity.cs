using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public float maxhp;//�ִ� ü��
    public float curhp;//���� ü��

    public float atk;//���� ���ݷ�
    public float def;//���� ����

    public float cureng;//���� ������
    public float maxeng;//�ִ� ������

    public float time;//Ư�� ������Ʈ���� �ٷ� ���������� ���� ��Ÿ��.


    public float baseSpeed; // ���� �ӵ�
    public float buffSpeed; // ������ ������ �� �ִ� �ӵ�
    public float flatSpeed; // ���� ���� �� �ִ� �ӵ�
    public float currentSpeed; // ���� �ӵ�

    public float baseTurnSpeed; // ���� �ൿ ��ġ
    public float currentTurnSpeed; // ���� �ൿ ��ġ

    public bool isMyTurn; // �� ������

    public bool canAct = true; //HP�� 0�� �Ǿ Ȱ���� �� �ִ��� üũ


    // Start �Լ����� �ʱ�ȭ�� �����մϴ�.
    protected virtual void Start()
    {
        // ���� �ӵ� ���
        float finalSpeed = baseSpeed * (1 + buffSpeed / 100) + flatSpeed;

        // ���� �ൿ ��ġ ���
        baseTurnSpeed = 10000 / finalSpeed;

        // �ʱ�ȭ Ȥ�� �� ���� �� ���� �ӵ��� �ൿ ��ġ�� ����
        currentSpeed = finalSpeed;
        currentTurnSpeed = baseTurnSpeed;


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
