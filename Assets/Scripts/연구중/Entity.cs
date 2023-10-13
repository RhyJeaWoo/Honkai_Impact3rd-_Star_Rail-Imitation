using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
   
    public float baseSpeed;//���ʼӵ� �� ĳ���Ͱ� ���� �ӵ�

    public float buffspeed;//������ ���� �� �� �ִ� �ӵ�

    public float flatSpeed; //�� ĳ���Ͱ� ���� ���� �� �ִ� �ӵ�

    private float finalSpeed; //���������� ������ �ӵ�

    private float TurnSpeed;//���� �ൿ ��ġ (ȯ�� �� ������ ����)

    private float TimeTurn;

    public bool ismyTurn;//�� ���� ��� (��Ȯ���� �÷��̾ �Ѱ��ϴ� ���� �� ���) �ܺο��� �� ���� ���ܿ��� ���� ���� �� �� ����
                           //���� �Ŵ����� �����ϰų�, ���� ���� ���ų�

    public bool isTurnOn; //

    protected bool notmyturn;//������ϰ�츦 �޾ƿͼ� �� ���ϰ� ��ġ���� üũ���� ����

    // finalSpeed = baseSpeed * (1 + buffspeed/100 ) + flatSpeed;
    // ���� �ӵ� = ���� �ӵ� * ������ �����ϴ� �ӵ� + ���� �����ϴ� �� �ӵ�



    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion

    protected virtual void Awake()
    {

        finalSpeed = baseSpeed * (1 + (buffspeed / 100 + buffspeed % 100) + finalSpeed);
        TurnSpeed = 10000/finalSpeed + 10000%finalSpeed;

        TimeTurn = TurnSpeed;


    }


    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

       
    }

    //��� ���̰ų� �� �ٸ� ������Ʈ�� ���� ��� �ൿ ��ġ�� �������� ����.


    protected virtual void Update()
    {

        if(ismyTurn)

        
        if (ismyTurn && !notmyturn) //������ On����, �׸��� �ٸ� �ϵ��� off���� üũ
        {
            isTurnOn = true; 

            TimeTurn -= 1;
            if (TimeTurn < 0)
            {
                TimeTurn = TurnSpeed;
                 
            }

        }

            Debug.Log(TurnSpeed);
    }



    
}
