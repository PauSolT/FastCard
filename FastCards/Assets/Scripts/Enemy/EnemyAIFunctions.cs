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

        Debug.Log("Enemy attacked for: " + value + " damage");
    }

    public static void Defend(int value)
    {
        int fullArmor = value + CombatManager.enemy.GetEnemy().GetStatusDefense();
        if (fullArmor >= 0)
            CombatManager.enemy.AddArmor(fullArmor);

        Debug.Log("Enemy defended for: " + value + " armor");
    }

    public static void Heal(int value)
    {
        int fullHealing = value + CombatManager.enemy.GetEnemy().GetStatusHeal();
        if (fullHealing >= 0)
            CombatManager.enemy.AddArmor(fullHealing);

        Debug.Log("Enemy healed for: " + value + " HP");

    }

    public static void StatusDamage(int value)
    {
        CombatManager.enemy.ApplyStatus(value, 0,0);
        Debug.Log("Enemy buffed damage for: " + value + " points");

    }

    public static void StatusArmor(int value)
    {
        CombatManager.enemy.ApplyStatus(0, value, 0);
        Debug.Log("Enemy buffed armor for: " + value + " points");
    }

    public static void StatusHealing(int value)
    {
        CombatManager.enemy.ApplyStatus(0, 0, value);
        Debug.Log("Enemy buffed healing for: " + value + " points");
    }

    public static void DebuffDamage(int value)
    {
        GameManager.player.ApplyStatus(-value, 0, 0);
        Debug.Log("Enemy debuffed player damage for: " + value + " points");
    }

    public static void DebuffArmor(int value)
    {
        GameManager.player.ApplyStatus(0, -value, 0);
        Debug.Log("Enemy debuffed player armorfor: " + value + " points");
    }

    public static void DebuffHealing(int value)
    {
        GameManager.player.ApplyStatus(0, 0, -value);
        Debug.Log("Enemy debuffed player healing for: " + value + " points");
    }

}
