using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite1 : EnemyBehaviour
{
    int damage1 = 17;
    int damage2 = 15;
    int debuff = 1;

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
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (damage1 + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Damage: " + (damage2 + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 2:
                CombatManager.intentionText.text = "Armor Debuff: " + debuff.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Attack(damage1);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Attack(damage2);
    }
    public override void ThirdOption()
    {
        EnemyAIFunctions.DebuffArmor(debuff);
    }
}
