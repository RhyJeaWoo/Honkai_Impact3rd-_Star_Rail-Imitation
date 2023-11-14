using UnityEngine;

public class PlayerTurnGetState : PlayerState
{
    //턴을 받으면 실행되는 상태
    public PlayerTurnGetState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        //base.Enter();


        player.skin[0].enabled = true; //캐릭터
        player.skin[1].enabled = true; //캐릭터가 들고 있는 무기들 을 자기턴일때, 활성화

        for (int i = 0; i < TurnManager.Instance.playable.Count; i++)
        {
            if (!TurnManager.Instance.playable[i].isMyTurn) //만약 내 턴이 아닌 플레이어블들이 있다면.
            {
                TurnManager.Instance.playable[i].skin[0].enabled = false;
                TurnManager.Instance.playable[i].skin[1].enabled = false; //그 턴에 한해 비화성화

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

        //여기서 실험.
        player.vircam[0].transform.position = player.transform.position + new Vector3(2, 1.5f, -3.5f);

        TurnManager.Instance.Enemy_target_simbol.SetActive(true); //적 마크
        TurnManager.Instance.Player_target_simbol.SetActive(false);//아군 마크


        player.toPlayerPos = player.transform.position;

        Debug.Log("턴을 잡았음");
    }

    public override void Exit()
    {
        //base.Exit();
        // player.vircam.transform.position = player.virCamPos;
        Debug.Log("attackWait로 빠졌음");

    }

    public override void Update()
    {
        base.Update();


        //player.vircam.transform.position = player.moveCamPos + new Vector3(player.ObjPos.transform.position.x, 0, 0);//키아나에서 이걸 -2를 박아버림. 그래서 문제가됨.
        // player.vircam.transform.rotation = player.virCamRot; //회전값 대입

        if (player.isMyTurn)
        {
            player.stateMachine.ChangeState(player.attackWaitState);
        }
    }
}
