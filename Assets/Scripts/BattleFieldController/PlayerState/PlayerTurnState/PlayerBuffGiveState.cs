using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineStoryboard;

public class PlayerBuffGiveState : PlayerState
{
    public PlayerBuffGiveState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    Vector3 baseCam = Vector3.zero;
    Vector3 baseMoveCam = Vector3.zero;

    public override void Enter()
    {
        base.Enter();

        baseCam = new Vector3(0f, 1.45f, 0.5f); //기본 캠 위치
        baseMoveCam = new Vector3(0f, 1.15f, 1f); //기본캠 위치
        //스킬 종료시 원래 위치로 초기화 할거임.

        player.vircam[5].transform.position = baseCam;
        player.vircam[6].transform.position = baseMoveCam;
        //여기서 일단 기초 위치 자체를 초기화 그래야 나가고 다시 들어와도 일단 초기화를 하고 시작함.

        player.vircam[3].MoveToTopOfPrioritySubqueue();

        

        TurnManager.Instance.SkillStackUse();

    }

    public override void Exit()
    {
        base.Exit();



        TurnManager.Instance.Enemy_target_simbol.SetActive(true); // 활성화
        

        for (int i = 0; i < TurnManager.Instance.enemys.Count && i < TurnManager.Instance.EnemyInitialPosition.Length; i++)
        {
            if (!TurnManager.Instance.enemys[i].isMyTurn)
            {
                TurnManager.Instance.enemys[i].transform.position = TurnManager.Instance.EnemyInitialPosition[i];
            }
        }


        for (int i = 0; i < TurnManager.Instance.playable.Count; i++)
        {
            if (!TurnManager.Instance.playable[i].isMyTurn) //만약 내 턴이 아닌 플레이어블들이 있다면.
            {
                TurnManager.Instance.playable[i].skin[0].enabled = false;
                TurnManager.Instance.playable[i].skin[1].enabled = false; //그 턴에 한해 비화성화

            }
        }


        if (TurnManager.Instance.targetPlayerName == player.playerList[0].name)
        {
            Debug.Log(player.playerList[0].name + "이 통과되었음");

            player.vircam[5].transform.position = new Vector3(player.vircam[5].transform.position.x + player.playerList[0].transform.position.x  , 1.45f, 0.5f);
            player.vircam[6].transform.position = new Vector3(player.vircam[6].transform.position.x + player.playerList[0].transform.position.x  , 1.15f, 1f);

            TurnManager.Instance.playable[0].skin[0].enabled = true;
            TurnManager.Instance.playable[0].skin[1].enabled = true; //그 턴에 한해 비화성화


        }
        else if (TurnManager.Instance.targetPlayerName == player.playerList[1].name)
        {
            Debug.Log(player.playerList[1].name + "이 통과되었음");

            player.vircam[5].transform.position = new Vector3(player.vircam[5].transform.position.x + player.playerList[1].transform.position.x , 1.45f, 0.5f);
            player.vircam[6].transform.position = new Vector3(player.vircam[6].transform.position.x + player.playerList[1].transform.position.x , 1.15f, 1f);

            TurnManager.Instance.playable[1].skin[0].enabled = true;
            TurnManager.Instance.playable[1].skin[1].enabled = true; //그 턴에 한해 비화성화
        }
        else if (TurnManager.Instance.targetPlayerName == player.playerList[2].name)
        {
            Debug.Log(player.playerList[2].name + "이 통과되었음");
            player.vircam[5].transform.position = new Vector3(player.vircam[5].transform.position.x + player.playerList[2].transform.position.x , 1.45f, 0.5f);
            player.vircam[6].transform.position = new Vector3(player.vircam[6].transform.position.x + player.playerList[2].transform.position.x  , 1.15f, 1f);

            TurnManager.Instance.playable[2].skin[0].enabled = true;
            TurnManager.Instance.playable[2].skin[1].enabled = true; //그 턴에 한해 비화성화
        }
        else if (TurnManager.Instance.targetPlayerName == player.playerList[3].name)
        {
            Debug.Log(player.playerList[3].name + "이 통과되었음");
            player.vircam[5].transform.position = new Vector3(player.vircam[5].transform.position.x + player.playerList[3].transform.position.x  , 1.45f, 0.5f);
            player.vircam[6].transform.position = new Vector3(player.vircam[6].transform.position.x + player.playerList[3].transform.position.x , 1.15f, 1f);

            TurnManager.Instance.playable[3].skin[0].enabled = true;
            TurnManager.Instance.playable[3].skin[1].enabled = true; //그 턴에 한해 비화성화
        }
        else
        {
            Debug.Log("정보 일치 하는게 없음.");
        }


    }

    public override void Update()
    {
        base.Update();

        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("BuffGive")
            && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            player.CastHealSkill();

            player.cureng += 30;

           
            player.stateMachine.ChangeState(player.playerSkillGivingState); //여기서 끝내지 말고, 누가 힐을 받았는지 카메라 전환이 필요함.

        }


    }
}
