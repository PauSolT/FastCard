using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : EnemyBehaviour
{
    int damage = 16;
    int defense = 16;
    int heal = 16;
    int buff1 = 2;
    int buff2 = 2;

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
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString();
                action = EnemyAction.Defend;
                break;
            case 2:
                CombatManager.intentionText.text = "Attack: " + buff1.ToString() + "\n Armor: " + buff2.ToString();
                action = EnemyAction.Status;
                break;
            case 3:
                CombatManager.intentionText.text = "Heal: " + (heal + CombatManager.enemy.GetEnemy().GetStatusHeal()).ToString();
                action = EnemyAction.Heal;
                break;
            default:
                break;
        }
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Attack(damage);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Defend(defense);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.StatusDamage(buff1);
        EnemyAIFunctions.StatusArmor(buff2);
    }

    public override void FourthOption()
    {
        EnemyAIFunctions.Heal(heal + CombatManager.enemy.GetEnemy().GetStatusHeal());
    }
}
