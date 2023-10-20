using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineStoryboard;

public class PlayerBuffGiveState : PlayerState
{
    public PlayerBuffGiveState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        TurnManager.Instance.target_simbol.SetActive(true); // Ȱ��ȭ

        Debug.Log("������ �� �� �ִ� ���¿� �����Ͽ���.");

        player.vircam[1].MoveToTopOfPrioritySubqueue();
        //player.CastDefensiveSkill();

      
        // Elysia�� ��쿡�� ��ų ����

    }

    public override void Exit()
    {
        base.Exit();
        TurnManager.Instance.target_simbol.SetActive(false); // Ȱ��ȭ
    }

    public override void Update()
    {
        base.Update();

        TurnManager.Instance.ChangePlayerTarget();

       // Debug.Log("target_simbol ��ǥ :" + TurnManager.Instance.target_simbol.transform.position);

       

     

        if (Input.GetKeyDown(KeyCode.E))
        {


            //Debug.Log("Ű�� ������");

            player.CastHealSkill();

        }
    }
}
