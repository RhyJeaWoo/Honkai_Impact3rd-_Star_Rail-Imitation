using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerIsMyUltimateTurnState : PlayerState
{
    //�ñر⸦ ��������, �ڱ� ���¸� ��ٸ��� ��
    private bool ultimateReserved = false; // �ñر� ���� ���θ� ��Ÿ���� �÷���
    public PlayerIsMyUltimateTurnState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        //base.Enter();
        Debug.Log(player.name + "�� �ô�� ���¿� �����Ͽ���");
    

    }

    public override void Exit()
    {
        //base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.isUltimate)
        {

            //stateMachine.ChangeState(player.ultimateWaitState);
            stateMachine.ChangeState(player.ulimateCutSceneState);
            return;
        }

        // �ٸ� �÷��̾ �̹� ���� ��Ҵ��� Ȯ��
        bool otherPlayerIsTurn = false;
        foreach (PlayerController otherPlayer in TurnManager.Instance.playable)
        {
            if (otherPlayer != player && otherPlayer.isMyTurn)
            {
                otherPlayerIsTurn = true;
                Debug.Log("�������� ���� �����");
                break;
            }
        }

        if (otherPlayerIsTurn)
        {
            // �ٸ� �÷��̾ ���� �̹� ������Ƿ� �ñر⸦ �ߵ����� �ʰ� ���¸� ����
            return;
        }

        if (!ultimateReserved && player.IsReservingUltimate)
        {
            // �ñر⸦ �����ϰ� �� ���� ���·� ����
            ultimateReserved = true;
            player.IsReservingUltimate = false;
            //TurnManager.Instance.TurnEnd();
        }

       

       
    }
        
       

    
}