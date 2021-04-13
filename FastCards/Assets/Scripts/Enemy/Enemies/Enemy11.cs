using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy11 : EnemyBehaviour
{
    int damage = 4;
    int buff = 4;

    public override void Init()
    {
        StartingPassive();
        options.Add(0, FirstOption);
        options.Add(1, SecondOption);
        options.Add(2, ThirdOption);
        options.Add(3, FourthOption);
        options.Add(4, FifthOption);
    }

    public override void ChooseOption()
    {
        base.ConsecutiveOption();
        UpdateIntention();
        
    }
    public override void FirstOption()
    {
        for (int i = 0; i < 4; i++)
        {
            EnemyAIFunctions.Attack(damage);
        }
    }
    public override void SecondOption()
    {
        FirstOption();
    }

    public override void ThirdOption()
    {
        FirstOption();
    }

    public override void FourthOption()
    {
        FirstOption();
    }

    public override void FifthOption()
    {
        EnemyAIFunctions.StatusDamage(buff);
    }

    public override void UpdateIntention()
    {
        switch (numRandom)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + " x 4";
                action = EnemyAction.Attack;
                break;
            case 4:
                CombatManager.intentionText.text = "Attack: " + buff.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }

}
