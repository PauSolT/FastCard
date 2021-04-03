using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : EnemyBehaviour
{
    int damage = 10;
    int heal = 6;
    int buff = 6;

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
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage() + LookUpTable.lookUpTable[LookUpTable.DelegateType.enemyHealingCombat]()).ToString();
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Heal: " + (heal + CombatManager.enemy.GetEnemy().GetStatusHeal()).ToString();
                action = EnemyAction.Heal;
                break;
            case 2:
                CombatManager.intentionText.text = "Recovery: " + buff.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Attack(damage + LookUpTable.lookUpTable[LookUpTable.DelegateType.enemyHealingCombat]());
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Heal(heal);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.StatusHealing(buff);
    }
}
