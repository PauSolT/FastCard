using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFunctions : Enemy
{
    public TextAsset jsonFile;
    Enemy enemy;

    public void Init()
    {
        enemy = new Enemy();
        enemy = JsonUtility.FromJson<Enemy>(jsonFile.text);

        enemy.SetCurrentHealth(enemy.GetStartingHealth());

        Debug.Log("Health: " + enemy.GetCurrentHealth() + "/" + enemy.GetStartingHealth());
    }

    public override void TakeDamage(int damage)
    {
        if (enemy.GetCurrentArmor() > 0)
        {
            if (enemy.GetCurrentArmor() >= damage)
                enemy.SetCurrentArmor(enemy.GetCurrentArmor() - damage);
            if (enemy.GetCurrentArmor() < damage)
            {
                int damageHealth = damage - enemy.GetCurrentArmor();
                enemy.SetCurrentArmor(0);
                enemy.SetCurrentHealth(damageHealth);
            }
        }
        Debug.Log("Armor: " + enemy.GetCurrentArmor() + "Health: " + enemy.GetCurrentHealth() + "/" + enemy.GetCurrentMaxHealth());

        if (enemy.GetCurrentHealth() <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    public override void Heal(int heal)
    {
        int healing = heal + enemy.GetStatusHeal();
        if (healing >= 0)
            enemy.SetCurrentHealth(enemy.GetCurrentHealth() + healing);

        if (enemy.GetCurrentHealth() >= enemy.GetCurrentMaxHealth())
            enemy.SetCurrentHealth(enemy.GetCurrentMaxHealth());

        Debug.Log("Health: " + enemy.GetCurrentHealth() + "/" + enemy.GetCurrentMaxHealth());
    }

    public override void AddArmor(int armor)
    {
        int def = armor + enemy.GetStatusDefense();
        if (def >= 0)
            enemy.SetCurrentArmor(enemy.GetCurrentArmor() + def);
    }

    public override void ApplyStatus(int damageBuff, int defenseBuff, int healingBuff)
    {
        enemy.SetStatusDamage(enemy.GetStatusDamage() + damageBuff);
        enemy.SetStatusDefense(enemy.GetStatusDefense() + defenseBuff);
        enemy.SetStatusHeal(enemy.GetStatusHeal() + healingBuff);
    }
}
