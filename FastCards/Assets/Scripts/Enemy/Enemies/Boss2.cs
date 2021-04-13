using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : EnemyBehaviour
{
    int damage = 40;
    int damage2 = 5;
    int buff = 1;
    int debuff = 4;
    int debuff2 = 4;
    int debuff3 = 4;

    bool debuffApplied = false;

    public override void Init()
    {
        StartingPassive();
        options.Add(0, FirstOption);
        options.Add(1, SecondOption);
        options.Add(2, ThirdOption);
        options.Add(3, FourthOption);
    }

    public override void ChooseOption()
    {
        base.ConsecutiveOption();

        if (debuffApplied && numRandom ==0)
            numRandom++;

        UpdateIntention();
        
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.DebuffDamage(debuff);
        EnemyAIFunctions.DebuffArmor(debuff2);
        EnemyAIFunctions.DebuffHealing(debuff3);
        debuffApplied = true;
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Attack(damage);
    }

    public override void ThirdOption()
    {
        for (int i = 0; i < 15; i++)
        {
            EnemyAIFunctions.Attack(damage2);
        }
    }

    public override void FourthOption()
    {
        EnemyAIFunctions.StatusDamage(buff);
    }

    public override void UpdateIntention()
    {
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Attack debuff: " + debuff.ToString() + "\n Armor debuff: " + debuff2.ToString() + "\n Recovery debuff: " + debuff3.ToString();
                action = EnemyAction.Status;
                break;
            case 1:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 2:
                CombatManager.intentionText.text = "Damage: " + (damage2 + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + " x 15";
                action = EnemyAction.Attack;
                break;
            case 3:
                CombatManager.intentionText.text = "Attack: " + buff.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }

}
