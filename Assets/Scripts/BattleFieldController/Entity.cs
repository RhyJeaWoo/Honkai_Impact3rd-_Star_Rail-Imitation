using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Entity : MonoBehaviour
{
    public Image hp;


    //��������Ʈ

    public delegate void DamageDealtHandler(float damage); //�������� ���� ��������Ʈ�� ����
    public event DamageDealtHandler OnDamageDealt;//��������Ʈ

    public delegate void LevelDealtHandler(float level); //������ ���� ��������Ʈ
    public event LevelDealtHandler OnLevelDealt;

    

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public AnimPlay anims;
     
    public SkinnedMeshRenderer[] skin;

    public CinemachineVirtualCamera[] vircam;

    public Vector3 toEnemyPos = Vector3.zero; //���� ��ġ

    public Vector3 toPlayerPos = Vector3.zero;//�� ���� ��ġ

    public Vector3 TargetEnemyTranform { get; set; } // TargetEnemyTranform�� �Ӽ����� �߰�

    public GameObject hudDamageText;

    public Transform hudPos;

    [Header("�÷��̾��� �������ͽ�")]
    public float curLevel;//���� ����
    public float maxLevel;//�ִ� ����

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

    /* �Ӽ� ���� ��Ÿ���� ���� �ƴ� �� ������ �������� ������.
     * 
     * ���� <-> (����) ȭ�� ���� ����
     * 
     * ��� ���� ���� ��� ����
     * 
     * 
     * 
     */
    [Header("�� ������Ʈ�� ������ �Ӽ�")]
    public string s = "s";


    [Header("�÷��̾��� ������ ���� ����")]
    public float defaultDamage;//ũ��Ƽ�� ���� ������ ���ϴ� ������

    public float norAtkDamage;

    public float criticalDamage;

    public float skillDamage;//��ų���������

    public float increasedDamage;//���ϴ� ���ط� ���� ������

    public float SumDamage;//���� ���� �� �������� ��.

    public float defenseCoefficient;//��� ���
    public float liciveOpponentLevel;//���� ���� ��� ���� ���
    public float ignoredDefense;//��� ���� �޴� ���

    [Header("�÷��̾��� �� ���� ����")]
    public float sumHeal; //�� ���� ��ġ

    /*
     * ���⼭ �÷��̾ ������ �Ӽ��� ����
     * ��, ����, ���� , ���, ���� ,���� �� �ٷ������.
     * �ϴ� Ű�Ƴ� ��
     * ���� ���� Ȯ��
     * �����þƴ� ����
     * ������� ����� ������
     * 
     * ��̳״� ���� �Ӽ� �� ���� �׸��� ������ ������
     * 
     */
   

    [Header("����Ѱ�")]

    public float time;//Ư�� ������Ʈ���� �ٷ� ���������� ���� ��Ÿ��.

    public bool isDamaged=false;//�������� �Ծ��°�?

    public bool isMyTurn=false; // �� �������� üũ�ϴ� ����
  
    public bool isUltimate = false; //���� �ñر� ��ư�� �����°��� ���� üũ

    public bool IsReservingUltimate = false; //�ñرⰡ ����Ǿ��°�?

    public bool firstUltimate = false; //�� ������ ���� ���� ù��°�� ������ �ְ� �ִ����� üũ�ϴ� �Լ���.

    public bool canAct = true; //HP�� 0�� �Ǿ Ȱ���� �� �ִ��� üũ


    //public bool StopTurn;
    // public bool isAtackOn = false; //���� ���� �غ� ���¿��� ������ �غ� �Ǿ����� üũ
    //public bool isSkillOn = false; //���� ��ų �غ� ���¿��� ��ų�� ����� �غ� �Ǿ����� üũ
    //�Ǿ����� üũ�ϰ� �Ѿ�°ɷ�

    // Start �Լ����� �ʱ�ȭ�� �����մϴ�.

    protected virtual void Start()
    {
        skin = GetComponentsInChildren<SkinnedMeshRenderer>();

        ObjectStatCal();

    }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update �Լ����� �� �� �ൿ ��ġ�� ������Ʈ�մϴ�.
    protected virtual void Update()
    {
        time -= Time.deltaTime;

        hp.fillAmount = curhp / maxhp;
    }

    public void ObjectStatCal() //�ʱ� ��ġ ���
    {
        // ���� �ӵ� ���    
        float finalSpeed = baseSpeed * (1 + buffSpeed / 100) + flatSpeed;
        // ���� �ൿ ��ġ ���
        baseTurnSpeed = 10000 / finalSpeed;
        // �ʱ�ȭ Ȥ�� �� ���� �� ���� �ӵ��� �ൿ ��ġ�� ����
        currentSpeed = finalSpeed;
        currentTurnSpeed = baseTurnSpeed;
        curhp = maxhp;
        //ũ��Ƽ�� ������ ������   (���ݷ� * ��ų ���) * (1 + ���� ���� ���) 

        defaultDamage = (atk) * (1 + increasedDamage);

       
    }

    public void DamageDelegate(float damage)
    {
        OnDamageDealt?.Invoke(damage);
    }

    public void LevelDelegate() 
    {
        OnLevelDealt?.Invoke(curLevel);
    }

    /*
    private void OnParticleCollision(GameObject other)
    {
        if(other != null)
        {
            if(other.CompareTag("Lumine"))
            {
                curhp = curhp - TurnManager.Instance.enemys[0].atk;

                Debug.Log("�������� �޾ҽ��ϴ�.");
            }
        }
        else if (other == null) return;

    }*/

    
    public void TakeDamageText(int damage)
    {
        float x = Random.Range(-1, 2);

        GameObject hudText = Instantiate(hudDamageText); // ������ �ؽ�Ʈ ������Ʈ
        hudText.transform.position = hudPos.position + new Vector3(x, 1f,0); // ǥ�õ� ��ġ
        hudText.GetComponent<DamageText>().damage = damage; // ������ ����
        //base.TakeDamage(damage);
    }
    
}
