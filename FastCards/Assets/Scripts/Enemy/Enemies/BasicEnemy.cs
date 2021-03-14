using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBehaviour
{
    int attack = 10;
    int defense = 15;
    int heal = 4;
    int buffattack = 1;
    int debuffHealing = 1;

    public override void Init()
    {
        StartingPassive();
        options.Add(0, FirstOption);
        options.Add(1, SecondOption);
        options.Add(2, ThirdOption);
        options.Add(3, FourthOption);
        options.Add(4, FifthOption);
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
                CombatManager.intentionText.text = "Defense: " + (defense + CombatManager.enemy.GetEnemy().GetStatusDefense()).ToString();
                action = EnemyAction.Defend;
                break;
            case 2:
                CombatManager.intentionText.text = "Healing: " + (heal + CombatManager.enemy.GetEnemy().GetStatusHeal()).ToString();
                action = EnemyAction.Heal;
                break;
            case 3:
                CombatManager.intentionText.text = "Attack buff: " + buffattack.ToString();
                action = EnemyAction.Status;
                break;
            case 4:
                CombatManager.intentionText.text = "Healing debuff: " + debuffHealing.ToString();
                action = EnemyAction.Status;
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
        EnemyAIFunctions.Defend(defense);
    }

    public override void ThirdOption()
    {
        EnemyAIFunctions.Heal(heal);
    }

    public override void FourthOption()
    {
        EnemyAIFunctions.StatusDamage(buffattack);
    }

    public override void FifthOption()
    {
        EnemyAIFunctions.DebuffHealing(debuffHealing);
    }

    public override void StartingPassive()
    {

    }

    public override void PassiveOption()
    {

    }
}
