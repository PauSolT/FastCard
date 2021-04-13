using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8 : EnemyBehaviour
{
    int damage = 15;
    int defense = 5;
    int heal = 20;
    int buff = 2;
    int buff2 = 1;
    int buff3 = 1;

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
        EnemyAIFunctions.Heal(heal);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.StatusDamage(buff);
        EnemyAIFunctions.StatusArmor(buff2);
        EnemyAIFunctions.StatusHealing(buff3);
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
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString() + "\n"
                    + "Heal: " + (heal + CombatManager.enemy.GetEnemy().GetStatusHeal()).ToString();
                action = EnemyAction.Multiple;
                break;
            case 2:
                CombatManager.intentionText.text = "Attack: " + buff.ToString() + "\n Armor: " + buff2.ToString() + "\n Recovery: " + buff3.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }

}
