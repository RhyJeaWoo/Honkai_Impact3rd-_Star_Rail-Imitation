using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillGivingState : PlayerState
{
    float time;
    int count;

    public PlayerSkillGivingState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        time = 0;
        count = 0;

        player.playableDirector[1].enabled = true;


    }

    public override void Exit()
    {
        base.Exit();


       

    }

    public override void Update()
    {
        base.Update();

        if (player.playableDirector[1].enabled)
        {
            time += Time.deltaTime;
        }

        if (time > 0.5f && count == 0) //힐 중복으로 사용되는걸 막기 위해 쓴 코드
        {
            int targetIndex = -1;
            for (int i = 0; i < player.playerList.Count; i++)
            {
                if (TurnManager.Instance.targetPlayerName == player.playerList[i].name)
                {
                    targetIndex = i;
                    break;
                }
            }

            // 유효한 인덱스가 발견된 경우
            if (targetIndex != -1 && player.gameobj.Length > 0 && player.gameobj[0] != null)
            {
                GameObject prefab = player.gameobj[0];
                // 프리팹 인스턴스화
                GameObject instance = Object.Instantiate(prefab, player.playerList[targetIndex].transform.position, prefab.transform.rotation);

                count++; // 상태 변경 후 중복 방지를 위해 카운트 증가
            }





        }

        if (time >= player.playableDirector[1].duration)
        {

            player.playableDirector[1].Stop();

            player.stateMachine.ChangeState(player.turnEndState);

        }

    }

}
