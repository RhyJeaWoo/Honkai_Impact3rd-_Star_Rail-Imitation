using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        TurnManager.Instance.target_simbol.SetActive(false);

        enemy.moveCamPos = enemy.vircam.transform.position; //먼저 가상 카메라 초기 위치 좌표를 저장함
        enemy.virCamRot = enemy.vircam.transform.rotation; //가상 카메라 회전 값을 저장

       // enemy.ObjPos.transform.position = enemy.transform.position;// 내 오브젝트 위치값을 Vector에 저장

        enemy.virCamPos = enemy.vircam.transform.position;//똑같이 카메라의 원래 값을 저장함.


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.vircam.transform.position = enemy.moveCamPos + new Vector3(enemy.ObjPos.transform.position.x, 0, 0);//키아나에서 이걸 -2를 박아버림. 그래서 문제가됨.
        enemy.vircam.transform.rotation = enemy.virCamRot; //회전값 대입

        if (enemy.isMyTurn)
        {
            stateMachine.ChangeState(lumine.selectState);
        }
    }

    
}
