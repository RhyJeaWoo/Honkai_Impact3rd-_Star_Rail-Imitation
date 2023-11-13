using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerUltimateState : PlayerState
{
   
    float time = 0;

    public PlayerUltimateState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        time = 0;

      


        player.playableDirector[0].enabled = true;

        player.LevelDelegate();


        if (player.CompareTag("Elysia"))
        {
            for (int i = 0; i < TurnManager.Instance.playable.Count; i++)
            {
                TurnManager.Instance.playable[i].skin[0].enabled = true;
                TurnManager.Instance.playable[i].skin[1].enabled = true; //�� �Ͽ� ���� ��ȭ��ȭ
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
      
        player.cureng += 5;
      
        time = 0;

        player.playableDirector[0].time = 0;

        player.playableDirector[0].enabled = false;

        player.engColorA.a = 0.64f;

        player.eng.color = player.engColorA;

        //�ʵ��� ��� ���� �˻�

        for (int i = 0; i < TurnManager.Instance.enemys.Count && i < TurnManager.Instance.EnemyInitialPosition.Length; i++)
        {

            //���� ���� Enemy�� ���� ���
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                //��� ���� ��ġ�� ������ �� �Ŵ����� ������ ���� ���� ��ġ�� ����.
                TurnManager.Instance.enemys[i].transform.position = TurnManager.Instance.EnemyInitialPosition[i];
            }
        }


    }

    public override void Update()
    {
        base.Update();


        if (player.playableDirector[0].enabled)
        {
            time += Time.deltaTime;
        }


        //player.playableDirector.duration -= Time.deltaTime;




        if (time >= player.playableDirector[0].duration)
        {

            player.playableDirector[0].Stop();
            stateMachine.ChangeState(player.ultimateEndState);
        
        }
      
    }

}
