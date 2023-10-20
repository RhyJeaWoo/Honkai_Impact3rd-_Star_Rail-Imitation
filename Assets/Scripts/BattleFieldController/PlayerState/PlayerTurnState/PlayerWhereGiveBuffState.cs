using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWhereGiveBuffState : PlayerState
{
    public PlayerWhereGiveBuffState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
    }

    public override void Update()
    {
        base.Update();
        TurnManager.Instance.ChangePlayerTarget();

        // Debug.Log("target_simbol ��ǥ :" + TurnManager.Instance.target_simbol.transform.position);





        if (Input.GetKeyDown(KeyCode.E))
        {

            player.stateMachine.ChangeState(player.buffgiveState);


        }
    }

   
}
