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
                lumine.isWeaknessBurned = true; //ȭ�� ����
                lumine.TakeDamageText(5000);
            }
            else if (lumine.isThunderDamaged)
            {
                lumine.isWeaknessElectrocuted = true;//���� ����
                lumine.TakeDamageText(5000);
            }
            else if (lumine.isPhysicalDamaged)
            {
                lumine.isWeaknessLaceration = true;//���� ����
                lumine.TakeDamageText( (int)(17 * (lumine.curhp/100)) + 3000);
            }

            /*
            if (lumine.isMyTurn)
            {
                TurnManager.Instance.TurnEnd(); //�ϸŴ����� �� ���嵵 Ǯ�� ��������.(�ٽ� �����ߵǴϱ�)

                lumine.isMyTurn = false;
            }
            */
          

            stateMachine.ChangeState(lumine.defeatedState);
            //���⼭ ��̳� �ӵ� ���, �ڱ� ���ϰ�찡 �����ε� ���� ������ ��������. ���¸� �ƿ� ��ȯ ���ѹ���.
        }

    }


}
