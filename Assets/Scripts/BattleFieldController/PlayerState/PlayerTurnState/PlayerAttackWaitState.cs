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

        //���⼭ �ɺ� Ÿ�� �ٽ� ���.
        StartVoice();

    }

    private void StartVoice()
    {
        if (player.CompareTag("Mei"))
        {
            SoundManager.instance.SFXPlay("TurnStart", player.playerSound[0]);
        }
        else if (player.CompareTag("Kiana"))
        {
            SoundManager.instance.SFXPlay("TurnStart", player.playerSound[0]);
        }
        else if (player.CompareTag("Elysia"))
        {
            SoundManager.instance.SFXPlay("TurnStart", player.playerSound[0]);
        }
        else if (player.CompareTag("Durandal"))
        {
            SoundManager.instance.SFXPlay("TurnStart", player.playerSound[0]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();

        //TurnManager.Instance.ChangeEnemyTarget();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //���⼭ ���� ��� �غ� idle ����
            //Ȯ������ Ű �Է½� �۵�
            player.stateMachine.ChangeState(player.targetMoveState);
          
        
        }

        if (Input.GetKeyDown(KeyCode.E) && TurnManager.Instance.SkillStack > 0)
        {
            //Ű����� ��ų �غ� ������� �̵�
            
            player.stateMachine.ChangeState(player.skillWaitState);

      
        }else if(TurnManager.Instance.SkillStack == 0 && Input.GetKeyDown(KeyCode.E)) 
        { 
            Debug.Log("��ų ����Ʈ�� ���ڶ��ϴ�."); 
        }
        

    }
}
