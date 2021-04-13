using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : EnemyBehaviour
{
    int damage1 = 20;
    int damage2 = 10;
    int buff = 3;

    public override void Init()
    {
        StartingPassive();
        options.Add(0, FirstOption);
        options.Add(1, SecondOption);
        options.Add(2, ThirdOption);
    }

    public override void ChooseOption()
    {
        base.ChooseOption();
        UpdateIntention();
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Attack(damage1);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Attack(damage2);
        EnemyAIFunctions.Attack(damage2);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.StatusDamage(buff);
    }

    public override void UpdateIntention()
    {
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (damage1 + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Damage: " + (damage2 + CombatManager.enemy.GetEnemy().GetStatusHeal()).ToString() + " x 2";
                action = EnemyAction.Attack;
                break;
            case 2:
                CombatManager.intentionText.text = "Attack: " + buff.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }
}
