using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy10 : EnemyBehaviour
{
    int damage = 7;
    int defense = 7;
    int heal = 7;

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
        EnemyAIFunctions.Attack(damage);
        EnemyAIFunctions.Attack(damage);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Defend(defense);
        EnemyAIFunctions.Defend(defense);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.Heal(heal);
        EnemyAIFunctions.Heal(heal);
    }

    public override void UpdateIntention()
    {
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + " x 2";
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString() + "x 2";
                action = EnemyAction.Defend;
                break;
            case 2:
                CombatManager.intentionText.text = "Heal: " + (heal + CombatManager.enemy.GetEnemy().GetStatusHeal()).ToString() + " x 2";
                action = EnemyAction.Heal;
                break;
            default:
                break;
        }
    }

}
