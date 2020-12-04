using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyAIFunctions 
{
    public static void Attack(int value)
    {
        int fullDamage = value + CombatManager.enemy.GetEnemy().GetStatusDamage();
        if (fullDamage >= 0)
            GameManager.player.TakeDamage(fullDamage);
    }

    public static void Defend(int value)
    {
        int fullArmor = value + CombatManager.enemy.GetEnemy().GetStatusDefense();
        if (fullArmor >= 0)
            CombatManager.enemy.AddArmor(fullArmor);
    }

    public static void Heal(int value)
    {
        int fullHealing = value + CombatManager.enemy.GetEnemy().GetStatusHeal();
        if (fullHealing >= 0)
            CombatManager.enemy.AddArmor(fullHealing);
    }

    public static void StatusDamage(int value)
    {
        CombatManager.enemy.ApplyStatus(value, 0,0);
    }

    public static void StatusArmor(int value)
    {
        CombatManager.enemy.ApplyStatus(0, value, 0);
    }

    public static void StatusHealing(int value)
    {
        CombatManager.enemy.ApplyStatus(0, 0, value);
    }

}
