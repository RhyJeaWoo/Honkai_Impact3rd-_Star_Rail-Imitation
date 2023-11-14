using UnityEngine;

public class PlayerTurnGetState : PlayerState
{
    //���� ������ ����Ǵ� ����
    public PlayerTurnGetState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        //base.Enter();


        player.skin[0].enabled = true; //ĳ����
        player.skin[1].enabled = true; //ĳ���Ͱ� ��� �ִ� ����� �� �ڱ����϶�, Ȱ��ȭ

        for (int i = 0; i < TurnManager.Instance.playable.Count; i++)
        {
            if (!TurnManager.Instance.playable[i].isMyTurn) //���� �� ���� �ƴ� �÷��̾����� �ִٸ�.
            {
                TurnManager.Instance.playable[i].skin[0].enabled = false;
                TurnManager.Instance.playable[i].skin[1].enabled = false; //�� �Ͽ� ���� ��ȭ��ȭ

            }
        }
        
      
        for (int i = 0; i < TurnManager.Instance.enemys.Count; i++) 
        {
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                TurnManager.Instance.enemys[i].transform.position = TurnManager.Instance.enemys[i].transform.position
                    + new Vector3(player.transform.position.x, 0 ,0);
            }
        }

        player.vircam[0].MoveToTopOfPrioritySubqueue();

        //���⼭ ����.
        player.vircam[0].transform.position = player.transform.position + new Vector3(2, 1.5f, -3.5f);

        TurnManager.Instance.Enemy_target_simbol.SetActive(true); //�� ��ũ
        TurnManager.Instance.Player_target_simbol.SetActive(false);//�Ʊ� ��ũ


        player.toPlayerPos = player.transform.position;

        Debug.Log("���� �����");
    }

    public override void Exit()
    {
        //base.Exit();
        // player.vircam.transform.position = player.virCamPos;
        Debug.Log("attackWait�� ������");

    }

    public override void Update()
    {
        base.Update();


        //player.vircam.transform.position = player.moveCamPos + new Vector3(player.ObjPos.transform.position.x, 0, 0);//Ű�Ƴ����� �̰� -2�� �ھƹ���. �׷��� ��������.
        // player.vircam.transform.rotation = player.virCamRot; //ȸ���� ����

        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.attackWaitState);
        }
    }
}
