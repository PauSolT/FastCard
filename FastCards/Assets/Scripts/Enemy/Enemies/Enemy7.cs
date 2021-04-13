using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : EnemyBehaviour
{
    int damage = 15;
    int defense = 5;
    int defense2 = 20;

    public override void Init()
    {
        StartingPassive();
        options.Add(0, FirstOption);
        options.Add(1, SecondOption);
        options.Add(2, ThirdOption);
    }

    public override void ChooseOption()
    {
        base.ConsecutiveOption();
        UpdateIntention();
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Defend(defense);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Defend(defense);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.Attack(damage);
    }

    public override void UpdateIntention()
    {
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString();
                action = EnemyAction.Defend;
                break;
            case 1:
                CombatManager.intentionText.text = "Defense: " + (defense2 + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString();
                action = EnemyAction.Defend;
                break;
            case 2:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + " x 2";
                action = EnemyAction.Attack;
                break;
            default:
                break;
        }
    }

}
