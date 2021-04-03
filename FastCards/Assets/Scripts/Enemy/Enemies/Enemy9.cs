using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy9 : EnemyBehaviour
{
    int damage = 10;
    int buff = 1;

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
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + " x 3";
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Attack: " + buff.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }
    public override void FirstOption()
    {
        for (int i = 0; i < 3; i++)
        {
            EnemyAIFunctions.Attack(damage);

        }
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.StatusDamage(buff);
    }
}
