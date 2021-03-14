using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEnemy : EnemyBehaviour
{
    int attack = 15;
    int attack2 = 35;
    int defense = 20;

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
                CombatManager.intentionText.text = "Attack: " + (attack + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 1:
                CombatManager.intentionText.text = "Attack: " + (attack + CombatManager.enemy.GetEnemy().GetStatusDamage()).ToString();
                action = EnemyAction.Attack;
                break;
            case 2:
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString();
                action = EnemyAction.Defend;
                break;
            default:
                break;
        }
    }
    public override void FirstOption()
    {
        EnemyAIFunctions.Attack(attack);
    }
    public override void SecondOption()
    {
        EnemyAIFunctions.Attack(attack2);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.Defend(defense);
    }

    public override void FourthOption()
    {
    }

    public override void FifthOption()
    {
    }

    public override void StartingPassive()
    {

    }

    public override void PassiveOption()
    {

    }
}
