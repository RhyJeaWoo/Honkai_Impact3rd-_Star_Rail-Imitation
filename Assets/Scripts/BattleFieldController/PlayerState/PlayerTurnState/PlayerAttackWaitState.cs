using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackWaitState : PlayerState
{

    //���� ������ ���� ���� ������ ������
    public PlayerAttackWaitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(player.name + "�� ����(�Ϲݰ���) ������");
        TurnManager.Instance.target_simbol.SetActive(true);
        //���⼭ �⺻���� �غ� �ִϸ��̼� ����
        //player.vircam.transform.position = player.transform.position + new Vector3(0,-0.3f,-9.7f);
        player.virCamPos = player.vircam.transform.position; //���� ����� ī�޶� �ʱ� ��ġ ��ǥ�� ������
        player.virCamRot = player.vircam.transform.rotation; //ī�޶� ȸ�� ���� ����

        player.ObjPos = player.transform.position;


        //���⼭ ī�޶� ������ �ٽ� �ؾߵ� �� ����. ���װ� ����.
        //�׸��� �ϴ� ĳ���͵� �ϼ����� �ؾߵɰ� ����.

       //player.vircam


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.vircam.transform.position = player.virCamPos + new Vector3(player.ObjPos.x, 0,0);
        player.vircam.transform.rotation = player.virCamRot;

       //TurnManager.Instance.WhoisTurn(player.gameObject);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //���⼭ ���� ��� �غ� idle ����
            //Ȯ������ Ű �Է½� �۵�
            player.stateMachine.ChangeState(player.attackState);
            //Debug.Log("Q�� ������");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Ű����� ��ų �غ� ������� �̵�
            player.stateMachine.ChangeState(player.skillwaitState);
            //Debug.Log("E�� ������");
        }

    }
}
