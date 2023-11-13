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

        TurnManager.Instance.Player_target_simbol.SetActive(true); // Ȱ��ȭ
        TurnManager.Instance.Enemy_target_simbol.SetActive(false);

        Debug.Log("������ �� �� �ִ� ���¿� �����Ͽ���.");

        player.vircam[2].MoveToTopOfPrioritySubqueue();
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


        }else if (Input.GetKeyDown(KeyCode.Q))
        {
            player.stateMachine.ChangeState(player.turnGetState);
        }
    }

   
}
