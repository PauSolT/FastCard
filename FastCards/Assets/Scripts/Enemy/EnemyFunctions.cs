﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

 [System.Serializable]
public class EnemyFunctions : Enemy
{
    TextAsset jsonFile;
    Enemy enemy;

    //Jsons/EnemyValues
    [SerializeField] string path = "Jsons/EnemyValues";

    [SerializeField]EnemyBehaviour selectedBehaviour = null;

    public void Init()
    {
        enemy = new Enemy();
        jsonFile = Resources.Load(path) as TextAsset;
        enemy = JsonUtility.FromJson<Enemy>(jsonFile.text);

        enemy.SetCurrentHealth(enemy.GetStartingHealth());
        enemy.SetBehaviour(selectedBehaviour);
        enemy.GetBehaviour().Init();

        //Debug.Log("Enemy Health: " + enemy.GetCurrentHealth() + "/" + enemy.GetStartingHealth());
    }

    public override void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        if (enemy.GetCurrentArmor() > 0)
        {
            int damageHealth = damage - enemy.GetCurrentArmor();
            if (damageHealth <= 0)
            {
                enemy.SetCurrentArmor(enemy.GetCurrentArmor() - damage);
                CombatManager.enemyArmor.text = enemy.GetCurrentArmor().ToString();
            }
            if (damageHealth > 0)
            {
                enemy.SetCurrentArmor(0);
                CombatManager.enemyArmor.text = enemy.GetCurrentArmor().ToString();
                LoseHP(damageHealth);
            }
        }
        else
        {
            LoseHP(damage);
        }

        if (enemy.GetCurrentHealth() <= 0)
        {
            GameManager.player.LevelUp();
            GameObject.Find("Canvas/EnemyHUD").gameObject.SetActive(false);
        }
    }

    public override void Heal(int heal)
    {
        int healing = heal + enemy.GetStatusHeal();
        if (healing >= 0)
            enemy.SetCurrentHealth(enemy.GetCurrentHealth() + healing);

        if (enemy.GetCurrentHealth() >= enemy.GetStartingHealth())
            enemy.SetCurrentHealth(enemy.GetStartingHealth());

        CombatManager.enemyHealth.text = enemy.GetCurrentHealth() + " / " + enemy.GetStartingMaxHealth();
        CombatManager.enemySlider.value = enemy.GetCurrentHealth();

        //Debug.Log("Armor: " + enemy.GetCurrentArmor() + "Health: " + enemy.GetCurrentHealth() + "/" + enemy.GetStartingHealth());
    }

    public override void AddArmor(int armor)
    {
        int def = armor + enemy.GetStatusDefense();
        if (def >= 0)
        {
            enemy.SetCurrentArmor(enemy.GetCurrentArmor() + def);
            CombatManager.enemyArmor.text = CombatManager.enemy.GetEnemy().GetCurrentArmor().ToString();
        }

        //Debug.Log("Armor: " + enemy.GetCurrentArmor() + "Health: " + enemy.GetCurrentHealth() + "/" + enemy.GetStartingHealth());
    }

    public override void ApplyStatus(int damageBuff, int defenseBuff, int healingBuff)
    {
        enemy.SetStatusDamage(enemy.GetStatusDamage() + damageBuff);
        enemy.SetStatusDefense(enemy.GetStatusDefense() + defenseBuff);
        enemy.SetStatusHeal(enemy.GetStatusHeal() + healingBuff);
        //Debug.Log("Damage Buff: " + enemy.GetStatusDamage() + "Defense Buff: " + enemy.GetStatusDefense() + "Healing Buff " + enemy.GetStatusHeal());
    }

    public Enemy GetEnemy() { return enemy; }

    public override void LoseHP(int hpLoss)
    {
        enemy.SetCurrentHealth(enemy.GetCurrentHealth() - hpLoss);
        CombatManager.enemyHealth.text = enemy.GetCurrentHealth() + " / " + enemy.GetStartingMaxHealth();
        CombatManager.enemySlider.value = enemy.GetCurrentHealth();

    }
    public void DoOption()
    {
        selectedBehaviour.ChooseOption();
    }
    public void ExecuteOption()
    {
        selectedBehaviour.ExecuteOption();
    }
}
