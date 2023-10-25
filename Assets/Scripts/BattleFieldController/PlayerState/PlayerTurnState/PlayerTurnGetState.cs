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

        TurnManager.Instance.Enemy_target_simbol.SetActive(true); //�� ��ũ
        TurnManager.Instance.Player_target_simbol.SetActive(false);//�Ʊ� ��ũ


        player.vircam[0].MoveToTopOfPrioritySubqueue();
       

        player.toPlayerPos = player.transform.position;


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
