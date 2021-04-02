using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBehaviour
{
    int attack = 6;
    int defense = 7;
    int buffattack = 1;

    public override void Init()
    {
        StartingPassive();
        options.Add(0, FirstOption);
        options.Add(1, SecondOption);
    }

    public override void ChooseOption()
    {
        base.ChooseOption();
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (attack + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString() + "\n" +
                    "Attack Buff: " + buffattack;
                action = EnemyAction.Multiple;
                break;
            default:
                break;
        }
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Attack(attack);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Defend(defense);
        EnemyAIFunctions.StatusDamage(buffattack);
    }
}
