public class LumineIdleState : EnemyState
{
    private EnemyLumine lumine;
    public LumineIdleState(EnemyAIController _enemyBase, EnemyStateMachine2 _stateMachine, string _animBoolName, EnemyLumine lumine) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.lumine = lumine;
    }

    public override void Enter()
    {
        base.Enter();

        TurnManager.Instance.Enemy_target_simbol.SetActive(false);
        TurnManager.Instance.Player_target_simbol.SetActive(false);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();



        if (lumine.isMyTurn)
        {
            stateMachine.ChangeState(lumine.selectState);
        }

        if (lumine.isDamaged && !lumine.isMyTurn)
        {
            stateMachine.ChangeState(lumine.hitState);
        }

        if (lumine.isWeakness)
        {
            if (lumine.isFireDamaged)
            {
                lumine.isWeaknessBurned = true; //화상 상태
                lumine.TakeDamageText(5000);
            }
            else if (lumine.isThunderDamaged)
            {
                lumine.isWeaknessElectrocuted = true;//감전 상태
                lumine.TakeDamageText(5000);
            }
            else if (lumine.isPhysicalDamaged)
            {
                lumine.isWeaknessLaceration = true;//열상 상태
                lumine.TakeDamageText( (int)(17 * (lumine.curhp/100)) + 3000);
            }

            /*
            if (lumine.isMyTurn)
            {
                TurnManager.Instance.TurnEnd(); //턴매니저의 턴 엔드도 풀어 버려야함.(다시 돌려야되니까)

                lumine.isMyTurn = false;
            }
            */
          

            stateMachine.ChangeState(lumine.defeatedState);
            //여기서 루미네 속도 깎고, 자기 턴일경우가 문제인데 턴을 강제로 꺼버리고. 상태를 아예 전환 시켜버림.
        }

    }


}
