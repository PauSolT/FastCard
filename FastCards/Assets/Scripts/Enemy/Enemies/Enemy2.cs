using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBehaviour
{
    int buffDamage = 1;
    int debuffArmor = 1;
    int attack = 7;

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
        EnemyAIFunctions.Attack(attack);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.StatusDamage(buffDamage);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.DebuffArmor(debuffArmor);
    }

    public override void UpdateIntention()
    {
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (attack + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Attack: " + buffDamage.ToString();
                action = EnemyAction.Status;
                break;
            case 2:
                CombatManager.intentionText.text = "Armor debuff: " + debuffArmor.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }
}
