using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite4 : EnemyBehaviour
{
    int damage = 3;
    int times = 1;

    public override void Init()
    {
        StartingPassive();
        options.Add(0, FirstOption);
    }

    public override void ChooseOption()
    {
        base.ConsecutiveOption();
        switch (numRandom)
        {
            case 0:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + LookUpTable.lookUpTable[LookUpTable.DelegateType.currentTurn]() + " x " + (times + LookUpTable.lookUpTable[LookUpTable.DelegateType.currentTurn]()).ToString();
                action = EnemyAction.Attack;
                break;
            default:
                break;
        }
    }
    public override void FirstOption()
    {
        for (int i = 0; i < times + LookUpTable.lookUpTable[LookUpTable.DelegateType.currentTurn](); i++)
        {
            EnemyAIFunctions.Attack(damage + CombatManager.enemy.GetEnemy().GetStatusDamage() + LookUpTable.lookUpTable[LookUpTable.DelegateType.currentTurn]());
        }
    }
}
