using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateWaitState : PlayerState //���⼭ �ñر⸦ �ߵ��� Ÿ�� �����ϰ� �����̽� ������ �۵�
{
    public PlayerUltimateWaitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.vircam[0].MoveToTopOfPrioritySubqueue();
        if(player.CompareTag("Mei"))
        player.anims.UltimateWaitEffect();

        TurnManager.Instance.Enemy_target_simbol.SetActive(true);

        player.toEnemyPos = TurnManager.Instance.EnemyTransForm;//Ÿ�� ����


        // Ÿ�� �������� ȸ����
        player.transform.LookAt(player.toEnemyPos);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.beforeUltimateExcute);
        }



    }
}
