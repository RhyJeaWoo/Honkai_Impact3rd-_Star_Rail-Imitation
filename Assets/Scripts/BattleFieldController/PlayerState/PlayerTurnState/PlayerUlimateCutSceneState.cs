using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUlimateCutSceneState : PlayerState
{
    //�ƾ��� ���⼭ �Ұ���.
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
        //���⼭ �ƽ��� ������.
        player.ultimateScene.color = player.ultimateDefaultAlbedo;
        player.cutScene.SetActive(false);
    }
        

    public override void Update()
    {
        //���⼭ �˺��� ���� ä��
        player.ultimatePlusAlbedo.a += 0.005f;

        player.ultimateScene.color = player.ultimatePlusAlbedo;


        //���⼭ �˺��� ���� Ǯ���� üũ�ϰ� ���������� �ѱ�
        if (player.ultimateScene.color.a >= 1)
        {
            stateMachine.ChangeState(player.ultimateWaitState);
        }
    }
}
