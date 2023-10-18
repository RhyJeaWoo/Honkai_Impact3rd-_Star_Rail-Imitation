using UnityEngine;

public class PlayerTargetToMoveState : PlayerState
{

    public PlayerTargetToMoveState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        player.toEnemyPos = TurnManager.Instance.TargetEnemyTranform;


        // 타겟 방향으로 회전함
        player.transform.LookAt(player.toEnemyPos);

        //Vector3 l_vector = toEnemyPos - player.transform.position;
        //player.transform.rotation = Quaternion.LookRotation(l_vector).normalized;

        //Debug.Log(player.toEnemyPos);

        base.Enter();

        //player.stateMachine.currentState
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.transform.position = Vector3.Lerp(player.transform.position, player.toEnemyPos - new Vector3(0, 0, 1f), 0.05f);

        if (player.stateMachine.prevState is PlayerSkillWaitState)
        {
            if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("TargetToMove")
          && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {

                player.stateMachine.ChangeState(player.skillState);
            }
        }else if(player.stateMachine.prevState is PlayerAttackWaitState)
        {
            if(player.anim.GetCurrentAnimatorStateInfo(0).IsName("TargetToMove")
                && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                player.stateMachine.ChangeState(player.attackState);
            }
        }
    }
}
