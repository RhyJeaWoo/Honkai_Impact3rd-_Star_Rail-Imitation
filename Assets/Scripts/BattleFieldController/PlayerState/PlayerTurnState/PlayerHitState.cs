public class PlayerHitState : PlayerState
{

    //데미지를 받았을 때의 상태임.
    public PlayerHitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //너무 빨리 작동됨.


        for (int i = 0; i < TurnManager.Instance.enemys.Count; i++)
        {
            if (TurnManager.Instance.enemys[i].isAttack == true)
            {
                //player.isDamaged = true; 이건 Idle에서 처리하자.
                 player.HandleDamageDealt(TurnManager.Instance.enemys[i].atk);
                /*
                 * 지금보니 상당히 조잡하고, 단순하게 만들어졌음.
                 * 이 부분에서는 수정이 필요해보임, 이중 전략을 쓰던,
                 * 전략을 쓰던
                 * 
                 * 
                 * 
                 * 
                 */

            }
        }

    }

    public override void Exit()
    {
        base.Exit();
        player.isDamaged = false;
    }

    public override void Update()
    {
        base.Update();

        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {

            player.cureng += 10;

            //player.isMyTurn = false;
            player.stateMachine.ChangeState(player.idleState);

        }
    }
}
