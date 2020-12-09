using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBehaviour
{
    public override void Init()
    {
        StartingPassive();
        options.Add(EnemyAction.Attack, FirstOption);
        options.Add(EnemyAction.Defend, SecondOption);
        options.Add(EnemyAction.Heal, ThirdOption);
        options.Add(EnemyAction.Multiple, FourthOption);
        options.Add(EnemyAction.Status, FifthOption);
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Attack(10);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Defend(28);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.Heal(6);
    }

    public override void FourthOption()
    {
        EnemyAIFunctions.StatusDamage(2);
        EnemyAIFunctions.DebuffDamage(1);
    }

    public override void FifthOption()
    {
        EnemyAIFunctions.StatusHealing(3);
    }

    public override void StartingPassive()
    {

    }

    public override void PassiveOption()
    {

    }
}
