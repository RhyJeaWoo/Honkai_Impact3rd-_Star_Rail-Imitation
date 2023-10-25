using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComeBackState : PlayerState
{
    public PlayerComeBackState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //player.toPlayerPos = TurnManager.Instance.PlayerTranfrom;

       //player.transform.LookAt(player.toPlayerPos);

        //Debug.Log("내 돌아갈 좌표" + player.toPlayerPos);
    }

    public override void Exit()
    {
        base.Exit();
        player.transform.LookAt(player.toEnemyPos);
        player.toEnemyPos = Vector3.zero;

    }

    public override void Update()
    {
        base.Update();

        player.transform.position = Vector3.Lerp(player.transform.position, player.toPlayerPos, 0.05f);


        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("ComeBack")
         && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            player.isMyTurn = false;
            TurnManager.Instance.TurnEnd();
            player.stateMachine.ChangeState(player.idleState);

        }
      
      
    }
}
