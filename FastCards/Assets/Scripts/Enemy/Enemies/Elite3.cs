using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite3 : EnemyBehaviour
{
    int heal = 10;
    int damage = 1;
    int debuff = 1;
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
                CombatManager.intentionText.text = "Heal: " + (heal + CombatManager.enemy.GetEnemy().GetStatusHeal()).ToString() + "x 5";
                action = EnemyAction.Heal;
                break;
            case 1:
                CombatManager.intentionText.text = "Damage: " + (damage + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString() + "x 5";
                action = EnemyAction.Attack;
                break;
            case 2:
                CombatManager.intentionText.text = "Recovery debuff to self: " + debuff.ToString() + "\n Attack: " + buff.ToString();
                action = EnemyAction.Status;
                break;
            default:
                break;
        }
    }
    public override void FirstOption()
    {

        for (int i = 0; i < 5; i++)
        {
            EnemyAIFunctions.Heal(heal + CombatManager.enemy.GetEnemy().GetStatusHeal());
        }
    }
    public override void SecondOption()
    {
        for (int i = 0; i < 5; i++)
        {
            EnemyAIFunctions.Attack(damage + CombatManager.enemy.GetEnemy().GetStatusDamage());
        }
    }
    public override void ThirdOption()
    {
        EnemyAIFunctions.StatusHealing(-debuff);
        EnemyAIFunctions.StatusDamage(buff);
    }
}
