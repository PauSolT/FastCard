using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyBehaviour
{
    int damage = 10;
    int damage2 = 5;
    int damage3 = 3;
    int buff = 2;
    int buff2 = 3;

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
        EnemyAIFunctions.Attack(damage);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Attack(damage);
        EnemyAIFunctions.StatusDamage(buff);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.Attack(damage);
        EnemyAIFunctions.StatusDamage(buff2);
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
                CombatManager.intentionText.text = "Damage: " + (damage2 + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + "\n Attack: " + buff.ToString();
                action = EnemyAction.Multiple;
                break;
            case 2:
                CombatManager.intentionText.text = "Damage: " + (damage3 + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + " x 2" + "\n Attack: " + buff2.ToString();
                action = EnemyAction.Multiple;
                break;
            default:
                break;
        }
    }

}
