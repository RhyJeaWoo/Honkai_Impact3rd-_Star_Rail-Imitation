using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyAssigner
{
    //모든 플레이어블 캐릭터 관련 전략 패턴 할당은 이 클래스가 담당함

    public void AssignStrategies(PlayerController player)
    {
        PlayerSkillStrategy skillStrategy = null;
        PlayerAttackStrategy attackStrategy = null;
        PlayerUltimateStrategy ultimateStrategy = null;

        if (player.CompareTag("Mei"))
        {
            skillStrategy = new MeiSkill();
            attackStrategy = new MeiAttack();
            ultimateStrategy = new MeiUltimate();
        }
        else if (player.CompareTag("Kiana"))
        {
            skillStrategy = new KianaSkill();
            attackStrategy = new KianaAttack();
            ultimateStrategy = new KianaUltimate();
        }
        else if (player.CompareTag("Elysia"))
        {
            skillStrategy = new ElysiaSkill();
            attackStrategy = new ElysiaAttack();
            ultimateStrategy = new ElysiaUltimate();
        }
        else if (player.CompareTag("Durandal"))
        {
            skillStrategy = new DurandalSkill();
            attackStrategy = new DurandalAttack();
            ultimateStrategy = new DurandalUltimate();
        }

        if (skillStrategy != null && attackStrategy != null && ultimateStrategy != null)
        {
            player.SetSkillStrategy(skillStrategy);
            player.SetAttackStrategy(attackStrategy);
            player.SetUltimateStratergy(ultimateStrategy);
        }
    }
}
