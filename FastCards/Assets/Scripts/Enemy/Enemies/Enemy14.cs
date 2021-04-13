using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy14 : EnemyBehaviour
{
    int damage = 30;
    int defense = 30;
    int heal = 5;
    int buff = 1;
    int buff2 = 1;
    int debuff = 1;

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
        base.ChooseOption();
        UpdateIntention();
        
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Attack(damage);
        EnemyAIFunctions.StatusDamage(buff);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Defend(defense);
        EnemyAIFunctions.StatusArmor(buff2);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.Heal(heal);
        EnemyAIFunctions.Heal(heal);
    }

    public override void FourthOption()
    {
        EnemyAIFunctions.DebuffDamage(debuff);
    }

    public override void UpdateIntention()
    {
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + "\n" + "Attack: " + buff.ToString();
                action = EnemyAction.Multiple;
                break;
            case 1:
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString() + "\n" + "Armor: " + buff2.ToString();
                action = EnemyAction.Multiple;
                break;
            case 2:
                CombatManager.intentionText.text = "Heal: " + (heal + CombatManager.enemy.GetEnemy().GetStatusHeal()).ToString() + " x 2";
                action = EnemyAction.Heal;
                break;
            case 3:
                CombatManager.intentionText.text = "Attack debuff: " + debuff.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }
}
