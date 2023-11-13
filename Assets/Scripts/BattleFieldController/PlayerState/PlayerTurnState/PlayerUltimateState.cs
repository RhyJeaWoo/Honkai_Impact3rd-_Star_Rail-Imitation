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
                TurnManager.Instance.playable[i].skin[1].enabled = true; //그 턴에 한해 비화성화
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

        //필드위 모든 적들 검사

        for (int i = 0; i < TurnManager.Instance.enemys.Count && i < TurnManager.Instance.EnemyInitialPosition.Length; i++)
        {

            //턴을 잡은 Enemy가 없을 경우
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                //모든 적의 위치를 기존에 턴 매니저에 저장한 적의 기존 위치로 변경.
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
