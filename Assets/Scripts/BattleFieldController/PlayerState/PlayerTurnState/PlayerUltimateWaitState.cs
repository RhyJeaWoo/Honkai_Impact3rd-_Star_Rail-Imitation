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

        player.skin[0].enabled = true; //ĳ����
        player.skin[1].enabled = true; //ĳ���Ͱ� ��� �ִ� ����� �� �ڱ����϶�, Ȱ��ȭ

        player.vircam[0].transform.position = player.transform.position + new Vector3(2, 1.5f, -3.5f);
   
     


            for (int i = 0; i < TurnManager.Instance.playable.Count; i++)
            {
                if (!TurnManager.Instance.playable[i].isUltimate) //���� �� ���� �ƴ� �÷��̾����� �ִٸ�.
                {
                    TurnManager.Instance.playable[i].skin[0].enabled = false;
                    TurnManager.Instance.playable[i].skin[1].enabled = false; //�� �Ͽ� ���� ��ȭ��ȭ

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
            //���̱ô���� �׽�Ʈ��
            player.anims.UltimateWaitEffect();
            //player.anims.ParticleUltimateStart(); << �̰� �ȵ�
        }

        TurnManager.Instance.Enemy_target_simbol.SetActive(true);

        player.toEnemyPos = TurnManager.Instance.EnemyTransForm;//Ÿ�� ����


        // Ÿ�� �������� ȸ����
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
