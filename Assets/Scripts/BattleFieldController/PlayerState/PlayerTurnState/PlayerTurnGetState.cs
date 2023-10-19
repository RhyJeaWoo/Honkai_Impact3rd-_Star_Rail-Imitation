using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnGetState : PlayerState
{
    //���� ������ ����Ǵ� ����
    public PlayerTurnGetState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skin[1].enabled = true;
        player.skin[0].enabled = true;

        TurnManager.Instance.target_simbol.SetActive(true);


        player.vircam.MoveToTopOfPrioritySubqueue();

        player.toPlayerPos = player.transform.position;


        //player.toPlayerPos = TurnManager.Instance.PlayerTranfrom;


        //Debug.Log("���� ȹ�� �Ͽ���.");
        //���⼭ �⺻���� �غ� �ִϸ��̼� ����
        //player.vircam.transform.position = player.transform.position + new Vector3(0,-0.3f,-9.7f);

        //  player.moveCamPos = player.vircam.transform.position; //���� ���� ī�޶� �ʱ� ��ġ ��ǥ�� ������
        //   player.virCamRot = player.vircam.transform.rotation; //���� ī�޶� ȸ�� ���� ����

        // �� ������Ʈ ��ġ���� Vector�� ����

        //player.virCamPos = player.vircam.transform.position;//�Ȱ��� ī�޶��� ���� ���� ������.

        //���� �߻� ������Ʈ�� X��ǥ 0�ϰ�� ������ �߻����� �ʴ´�. ������,

        //

        //player.vircam



    }
        
    public override void Exit()
    {
        base.Exit();
       // player.vircam.transform.position = player.virCamPos;
      
    }

    public override void Update()
    {
        base.Update();


        //player.vircam.transform.position = player.moveCamPos + new Vector3(player.ObjPos.transform.position.x, 0, 0);//Ű�Ƴ����� �̰� -2�� �ھƹ���. �׷��� ��������.
       // player.vircam.transform.rotation = player.virCamRot; //ȸ���� ����

        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.attackWaitState);
        }
    }
}
