using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

 [System.Serializable]
public class EnemyFunctions : Enemy
{
    TextAsset jsonFile;
    Enemy enemy;

    //Jsons/EnemyValues
    [SerializeField] string path = "Jsons/Enemies/EnemyValues";

    [SerializeField]EnemyBehaviour selectedBehaviour = null;
    int healingCombatDone = 0;

    public void Init()
    {
        enemy = new Enemy();
        jsonFile = Resources.Load(path) as TextAsset;
        enemy = JsonUtility.FromJson<Enemy>(jsonFile.text);
        
        enemy.SetCurrentHealth(enemy.GetStartingHealth());
        enemy.SetCurrentArmor(enemy.GetStartingArmor());
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

        //If enemy dies
        if (enemy.GetCurrentHealth() <= 0)
        {
            Die();  
        }
    }

    public override void Heal(int heal)
    {
        CombatManager.enemy.GetEnemy().SetCurrentHealth(CombatManager.enemy.GetEnemy().GetCurrentHealth() + heal);
        healingCombatDone += heal;

        if (CombatManager.enemy.GetEnemy().GetCurrentHealth() >= CombatManager.enemy.GetEnemy().GetStartingMaxHealth())
            CombatManager.enemy.GetEnemy().SetCurrentHealth(CombatManager.enemy.GetEnemy().GetStartingMaxHealth());

        CombatManager.enemyHealth.text = CombatManager.enemy.GetEnemy().GetCurrentHealth() + " / " + CombatManager.enemy.GetEnemy().GetStartingMaxHealth();
        CombatManager.enemySlider.value = CombatManager.enemy.GetEnemy().GetCurrentHealth();

        //Debug.Log("Health: " + enemy.GetCurrentHealth() + "/" + enemy.GetStartingMaxHealth());
    }

    public int GetHealingCombat() { return healingCombatDone; }

    public override void AddArmor(int armor)
    {
        enemy.SetCurrentArmor(CombatManager.enemy.GetEnemy().GetCurrentArmor() + armor);
        CombatManager.enemyArmor.text = CombatManager.enemy.GetEnemy().GetCurrentArmor().ToString();

        //Debug.Log("Armor: " + enemy.GetCurrentArmor() + "Health: " + enemy.GetCurrentHealth() + "/" + enemy.GetStartingHealth());
    }

    public override void ApplyStatus(int damageBuff, int defenseBuff, int healingBuff)
    {
        CombatManager.enemy.GetEnemy().SetStatusDamage(CombatManager.enemy.GetEnemy().GetStatusDamage() + damageBuff);
        CombatManager.enemy.GetEnemy().SetStatusDefense(CombatManager.enemy.GetEnemy().GetStatusDefense() + defenseBuff);
        CombatManager.enemy.GetEnemy().SetStatusHeal(CombatManager.enemy.GetEnemy().GetStatusHeal() + healingBuff);
        //Debug.Log("Damage Buff: " + enemy.GetStatusDamage() + "Defense Buff: " + enemy.GetStatusDefense() + "Healing Buff " + enemy.GetStatusHeal());

        CombatManager.enemyAttackText.text = CombatManager.enemy.GetEnemy().GetStatusDamage().ToString();
        CombatManager.enemyArmorText.text = CombatManager.enemy.GetEnemy().GetStatusDefense().ToString();
        CombatManager.enemyRecoveryText.text = CombatManager.enemy.GetEnemy().GetStatusHeal().ToString();
    }

    public override void Die()
    {
        GameManager.player.GetPlayer().SetCurrentLevel(GameManager.player.GetPlayer().GetCurrentLevel() + 1);
        if (GameManager.player.GetPlayer().GetCurrentLevel() % 2 == 0)
        {
            GameManager.player.LevelUp();
        } else
        {
            GameManager.rewardSystem.GetRewards();
        }


        CombatManager.enemyHUD.SetActive(false);
        CombatManager.playerHUD.SetActive(false);
        CombatManager.combatHUD.SetActive(false);
        CombatManager.hand.gameObject.SetActive(false);
        GameManager.combatManager.EmptyHand();
    }
    public Enemy GetEnemy() { return enemy; }

    public override void LoseHP(int hpLoss)
    {
        CombatManager.enemy.GetEnemy().SetCurrentHealth(enemy.GetCurrentHealth() - hpLoss);
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
