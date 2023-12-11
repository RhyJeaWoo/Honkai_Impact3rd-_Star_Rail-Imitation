using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUlimateCutSceneState : PlayerState
{
    //컷씬을 여기서 할거임.
    public PlayerUlimateCutSceneState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        SoundManager.instance.SFXPlay("", player.anims.UltimatePlayClip);
        player.cutScene.SetActive(true);

        
    }

    public override void Exit()
    {
        //여기서 컷신을 종료함.
        player.ultimateScene.color = player.ultimateDefaultAlbedo;
        player.cutScene.SetActive(false);
    }
        

    public override void Update()
    {
        //여기서 알베도 값을 채움
        player.ultimatePlusAlbedo.a += 0.005f;

        player.ultimateScene.color = player.ultimatePlusAlbedo;


        //여기서 알베도 값이 풀인지 체크하고 다음씬으로 넘김
        if (player.ultimateScene.color.a >= 1)
        {
            stateMachine.ChangeState(player.ultimateWaitState);
        }
    }
}
