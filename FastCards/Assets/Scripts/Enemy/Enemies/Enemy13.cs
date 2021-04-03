using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy13 : EnemyBehaviour
{
    int damage = 12;
    int defense = 30;
    int buff = 1;
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
        base.ChooseOption();
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + " x 3";
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString();
                action = EnemyAction.Defend;
                break;
            case 2:
                CombatManager.intentionText.text = "Attack: " + buff.ToString() + "\n" + "Attack debuff: " + debuff.ToString();
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
        EnemyAIFunctions.Defend(defense);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.StatusDamage(buff);
        EnemyAIFunctions.DebuffDamage(debuff);
    }
}
