public class PlayerHitState : PlayerState
{

    //�������� �޾��� ���� ������.
    public PlayerHitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //�ʹ� ���� �۵���.


        for (int i = 0; i < TurnManager.Instance.enemys.Count; i++)
        {
            if (TurnManager.Instance.enemys[i].isAttack == true)
            {
                //player.isDamaged = true; �̰� Idle���� ó������.
                player.HandleDamageDealt(TurnManager.Instance.enemys[i].atk);

                
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
