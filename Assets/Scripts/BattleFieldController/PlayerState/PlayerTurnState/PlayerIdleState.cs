using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    //���� �Ǳ� ������ ����ϴ� ����
    public PlayerIdleState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log(player.name + "idle ���°� ����Ǿ���");
        //player.skin[1].enabled = false;
        //player.skin[0].enabled = false;



    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
   
        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.turnGetState);
        }

        for(int i = 0; i < player.playerList.Count; i++) {

            if (!TurnManager.Instance.StopTurn && player.isUltimate && !player.playerList[i].isMyTurn && !player.isMyTurn) //�������� ���� �ƴϾ�߸� �� ���ϵ�, �����ϵ�, �׷��鼭 ���� ��
            {
                //���⸸ �ǵ帮�� �ɵ�?


                player.stateMachine.ChangeState(player.isMyUltimateTurnState);
                //���� �̻��·� �Ѿ�� ��̳׳� �ٸ� ������ �ൿ�� ��� �����Ǿ����.
            }
        }
      

        //���⼭ ���࿡ ���� �����̻� ���¶���� �������� �ް� �����ϰ� �����ȴ�/
        // ex)if(player.isMyTurn && player.isStatusEffect)�� �˻縦 �ؼ�, PlayerStatusEffectState�ӽ� �����, ���ʿ��� �������� �ް� �Ѿ�� �ɵ�.

    }

}
