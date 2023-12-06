using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum property //���ε� �ý��ۿ� ���� �Ӽ�(ĳ������ �⺻ �Ӽ��� ���� ������ ���� ���ɰ���)
{
    fire, //�� 
    thunder, //����
    physical, //����
    ice, //����
    quantum, //����
    imaginary, //���
    without_weakness//������ ���� ����(�ַ� ���Ͱ� ���)

    //�� ���µ��� ���� ���Ľ� Ư�� ���·� �����ϱ� ����
};

public class Entity : MonoBehaviour
{
    public Image hp;


    //��������Ʈ

    public delegate void DamageDealtHandler(float damage); //�������� ���� ��������Ʈ�� ����
    public event DamageDealtHandler OnDamageDealt;//��������Ʈ

    public delegate void LevelDealtHandler(float level); //������ ���� ��������Ʈ
    public event LevelDealtHandler OnLevelDealt;

    public delegate void PropertyDealtHandler(property equal);//�Ӽ��� ���� ��������Ʈ(�� �Ӽ��� ����, �� �Ӽ��ϰ� ��ġ�ϴ��� �Ǵ��Ұ���)
    public event PropertyDealtHandler OnPropertyDealt;

    public delegate void StrongGaugeHandler(float strongGauge);
    public event StrongGaugeHandler OnStrongGaugeDealt;

    public  property character_attributes;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public AnimPlay anims; //���⼭ ��� �߻�.
     
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
     * ������ �����ϴ� ���
     * �� �÷��̾�� ĳ���Ͱ� ���� �������, �갡 ���� �Ӽ��� �����Ǵ� ������ ���� ������Ʈ���� �� ����.
     * 
     * ���� <-> (����) ȭ�� ���� ����
     * 
     * ��� ���� ���� ��� ����
     * 
     * �̳� Ÿ������ ����� �ɵ�
     * 
     * 
     * 4. ���� ���(�� �ȸ��� �Ѥ�) ����

        (1 - ���׼� + ���װ���)

        ���� �Ӽ��� ���� �ٸ� ���׼��� ������



        ���� �Ӽ�: 0%

        �Ϲ� �Ӽ�: 20%

        ���� �Ӽ�: 40%  ex) �������� ���� ����, ȭ������ ȭ�� ����

        ���װ����� ������ �������, ������ Ư��
     * 
     * 
     */


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
    * �������̾� �׳� �� ���� ���ڷ� ������ �ɵ� ������ ǥ���� ��... ���ͳ�
    * 
    */
    [Header("�� ������Ʈ�� ������ �Ӽ�")]
   




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

    //public float strongGaugePower = 0;

    [Header("�÷��̾��� �� ���� ����")]
    public float sumHeal; //�� ���� ��ġ

   
   

    [Header("����Ѱ�")]

    public float time;//Ư�� ������Ʈ���� �ٷ� ���������� ���� ��Ÿ��.

    public bool isDamaged=false;//�������� �Ծ��°�?

    public bool isMyTurn=false; // �� �������� üũ�ϴ� ����
  
    public bool isUltimate = false; //���� �ñر� ��ư�� �����°��� ���� üũ

    public bool IsReservingUltimate = false; //�ñرⰡ ����Ǿ��°�?

    public bool firstUltimate = false; //�� ������ ���� ���� ù��°�� ������ �ְ� �ִ����� üũ�ϴ� �Լ���.

    public bool canAct = true; //HP�� 0�� �Ǿ Ȱ���� �� �ִ��� üũ

    public bool isWeakness = false; //������ ���ĵ� �����ΰ�...?

    //�̰� True�� ���� ��� ���ο� ���·� ����.


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
        float x = Random.Range(-1, 2);

        GameObject hudText = Instantiate(hudDamageText); // ������ �ؽ�Ʈ ������Ʈ
        hudText.transform.position = hudPos.position + new Vector3(x, 1f,0); // ǥ�õ� ��ġ
        hudText.GetComponent<DamageText>().damage = damage; // ������ ����
        //base.TakeDamage(damage);
    }
    
}
