using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite2 : EnemyBehaviour
{
    int defense = 40;
    int damage = 10;
    int debuff = 3;
    int buff = 3;

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
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString();
                action = EnemyAction.Defend;
                break;
            case 1:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 2:
                CombatManager.intentionText.text = "Armor debuff to self: " + debuff.ToString() + "\n Attack: " + buff.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Defend(defense);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Attack(damage);
    }
    public override void ThirdOption()
    {
        EnemyAIFunctions.StatusArmor(-debuff);
        EnemyAIFunctions.StatusDamage(buff);
    }
}
