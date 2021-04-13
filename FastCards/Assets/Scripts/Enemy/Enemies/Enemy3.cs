using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyBehaviour
{
    int defense = 30;
    int damage = 5;

    public override void Init()
    {
        StartingPassive();
        options.Add(0, FirstOption);
        options.Add(1, SecondOption);
    }

    public override void ChooseOption()
    {
        base.ConsecutiveOption();
        UpdateIntention();
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Defend(defense);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Attack(damage + LookUpTable.lookUpTable[LookUpTable.DelegateType.currentTurn]());
    }

    public override void UpdateIntention()
    {
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString();
                action = EnemyAction.Defend;
                break;
            case 1:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage() + LookUpTable.lookUpTable[LookUpTable.DelegateType.currentTurn]()).ToString();
                action = EnemyAction.Attack;
                break;
            default:
                break;
        }
    }
}
