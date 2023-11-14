using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateWaitState : PlayerState //여기서 궁극기를 발동할 타겟 지정하고 스페이스 누르면 작동
{
    public PlayerUltimateWaitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.vircam[0].MoveToTopOfPrioritySubqueue();

        player.skin[0].enabled = true; //캐릭터
        player.skin[1].enabled = true; //캐릭터가 들고 있는 무기들 을 자기턴일때, 활성화

        player.vircam[0].transform.position = player.transform.position + new Vector3(2, 1.5f, -3.5f);
   
     


            for (int i = 0; i < TurnManager.Instance.playable.Count; i++)
            {
                if (!TurnManager.Instance.playable[i].isUltimate) //만약 내 턴이 아닌 플레이어블들이 있다면.
                {
                    TurnManager.Instance.playable[i].skin[0].enabled = false;
                    TurnManager.Instance.playable[i].skin[1].enabled = false; //그 턴에 한해 비화성화

                }
            }
        

        for (int i = 0; i < TurnManager.Instance.enemys.Count && i < TurnManager.Instance.EnemyInitialPosition.Length; i++)
        {
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                TurnManager.Instance.enemys[i].transform.position = TurnManager.Instance.EnemyInitialPosition[i];
            }
        }


        for (int i = 0; i < TurnManager.Instance.enemys.Count; i++)
        {
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                TurnManager.Instance.enemys[i].transform.position = TurnManager.Instance.enemys[i].transform.position
                    + new Vector3(player.transform.position.x, 0, 0);
            }
        }

        if (player.CompareTag("Mei"))
        {
            //메이궁대기모션 테스트중
            player.anims.UltimateWaitEffect();
            //player.anims.ParticleUltimateStart(); << 이거 안됨
        }

        TurnManager.Instance.Enemy_target_simbol.SetActive(true);

        player.toEnemyPos = TurnManager.Instance.EnemyTransForm;//타겟 지정


        // 타겟 방향으로 회전함
        player.transform.LookAt(player.toEnemyPos);

    }

    public override void Exit()
    {
        base.Exit();
        if(player.CompareTag("Mei"))
        {
            player.anims.DestroyUltimate();
            //player.anims.DestroyUltimate();
            //player.anims.StopParticleUltimate();
        }
       


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
