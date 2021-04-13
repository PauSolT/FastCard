using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy12 : EnemyBehaviour
{
    int damage = 5;
    int defense = 12;
    int bonusDamage = 0;
    public override void Init()
    {
        StartingPassive();
        options.Add(0, FirstOption);
        options.Add(1, SecondOption);
        bonusDamage = Mathf.FloorToInt(LookUpTable.lookUpTable[LookUpTable.DelegateType.enemyCurrentArmor]() / 10);
    }

    public override void ChooseOption()
    {
        base.ConsecutiveOption();
        UpdateIntention();
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Attack(damage + bonusDamage);
        EnemyAIFunctions.Attack(damage + bonusDamage);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Defend(defense);
        EnemyAIFunctions.Defend(defense);
    }

    public override void UpdateIntention()
    {
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage() + bonusDamage).ToString() + " x 2";
                action = EnemyAction.Attack;
                break;
            case 1:
                bonusDamage = Mathf.FloorToInt(LookUpTable.lookUpTable[LookUpTable.DelegateType.enemyCurrentArmor]() / 10);
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString() + "x 2";
                action = EnemyAction.Defend;
                break;
            default:
                break;
        }
    }
}
