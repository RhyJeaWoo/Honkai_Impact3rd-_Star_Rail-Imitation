using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<PlayerController> player;
    


    //������� ���� ��Ÿ���� ���� 
    //������ Ư¡ �ſ� �ܼ��� ���� ���������, ���� ���۱��� ĳ���Ͱ� ���� �ӵ��� ����� �ȴ�.
    //�ӵ� ���� ���� ���� �ؾߵ�.
    //ĳ���Ͱ� ���͸� ã�� �����ϴ°� �ι�° ������.

    public enum State
    {
        whoisTurn, playerTurn, enemyTurn, win, Lose // whoisTurn ����
            //�÷��̾�� ���� �ӵ��� ����ؼ� ���� �������� ������ ����.
    }

    public State state;
    public bool isLive;

    void Awake()
    {
        state= State.whoisTurn; //�⺻ ������, ���� ���� ���°� �ƴ�.
                                //BattleStart(); ���� �ǰݵǰų� �Ǵ� �÷��̰Ű� �ǰݽ� ���� ������ �Ұ���. 
        player = new List<PlayerController>();
    }

    private void Update()
    {
        if(state == State.whoisTurn)
        {
            player.Clear();
            foreach( PlayerController controller in player ) 
            {
            
            }
        }
    }

    void BattleStart()
    {
        //���� ���۽� ĳ������ ���� ȿ�� �Ǵ� ����Ʈ�� �ִϸ��̼��� ���

        //�÷��̾ ������ ���� �ѱ� �� �ӵ��� ����
        state = State.whoisTurn;
    }

    public void PlayerAttackButton()
    {
        //���� ��ų, ������ �� �ڵ� �Ǵ� �Լ� ȣ��
        //��ư�� ��� ������ �Ÿ� �����ϱ�

        if (state != State.playerTurn)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("�÷��̾� ���� ����");
        //���� ��ų, ������ �� �ڵ� �Ǵ� �÷��̾� �ൿ ���� ����

        //���� �׾����� ���� ����

        if(!isLive)
        {
            state = State.win;
            EndBattle();
            //���� �������, ������ �ӵ��� ����ؼ� �� �ѱ��

        }
        else
        {
            state = State.whoisTurn;
        }
        
    }

    void EndBattle()
    {
        Debug.Log("���� ����");
    }
}
