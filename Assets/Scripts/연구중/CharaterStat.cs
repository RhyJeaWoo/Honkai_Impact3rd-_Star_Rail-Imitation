using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterStat : MonoBehaviour
{
    //ĳ���͵��� ������ ������ ����.

    [Header("CharaterDefaultStat")]
    //���⼭ ĳ���� �� �÷��̾� �� ���� ���� ����� ���� ����� ����.
    //�⺻ 6���� ���ݸ� �����
    public int hp;//ĳ���� hp
    public int Atk;//ĳ���� ���ݷ�
    public int def;//ĳ���� ����
    public int AtkSpeed;//���� �ӵ�
    public int Critical;//ġ��Ÿ Ȯ��
    public int CriticalPower;//ġ��Ÿ ���ط� ����

    //����� ���⼭ ������ ���ΰ�?�� ���� ���⼭�� ���� ������.
    //ĳ���͸��� ���� ����ؾߵǱ� ����. ��� ��� �޾Ƽ�
    // �� ĳ���͵� ���� ���� ������ ����.

    #region Components

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }



    #endregion

    protected virtual void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }



}
