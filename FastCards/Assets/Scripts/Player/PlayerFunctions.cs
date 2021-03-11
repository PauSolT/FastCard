using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerFunctions : Player
{
    TextAsset jsonFile;
    Player player;

    string path = "Jsons/PlayerValues";


    // Start is called before the first frame update
    public void Init()
    {
        //New player
        player = new Player();
        //Read variables from Json
        jsonFile = Resources.Load(path) as TextAsset;
        player = JsonUtility.FromJson<Player>(jsonFile.text);

        //Set variables
        player.SetCurrentMaxHealth(player.GetStartingMaxHealth());
        player.SetCurrentMaxHandSize(player.GetStartingMaxHandSize());
        player.SetCurrentHealth(player.GetStartingHealth());
        player.SetCurrentLevel(player.GetStartingLevel());
        player.SetCurrentArmor(player.GetStartingArmor());
        player.SetCurrentMaxMana(player.GetStartingMaxMana());
        player.SetCurrentMana(player.GetStartingMana());

        if (File.Exists(path))
        {
            string rsPath = Path.Combine(Application.dataPath, "/Resources/SavedGame.json");
            jsonFile = Resources.Load(rsPath) as TextAsset;
            player = JsonUtility.FromJson<Player>(jsonFile.text);
        }
    }


    public override void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        if (player.GetCurrentArmor() > 0)
        {
            int damageHealth = damage - player.GetCurrentArmor();
            if(damageHealth <= 0)
            {
                player.SetCurrentArmor(player.GetCurrentArmor() - damage);
                CombatManager.playerArmor.text = GameManager.player.GetCurrentArmor().ToString();

            }

            else if(damageHealth > 0)
            {
                player.SetCurrentArmor(0);
                CombatManager.playerArmor.text = GameManager.player.GetCurrentArmor().ToString();
                LoseHP(damageHealth);
            }
        }
        else
        {
            LoseHP(damage);
        }
        //Debug.Log("Armor: " + player.GetCurrentArmor() + "Health: " + player.GetCurrentHealth() + "/" + player.GetCurrentMaxHealth());


    }

    public override void Heal(int heal)
    {
        int healing = heal + player.GetStatusHeal();
        if (healing >= 0)
            player.SetCurrentHealth(player.GetCurrentHealth() + healing);

        if (player.GetCurrentHealth() >= player.GetCurrentMaxHealth())
            player.SetCurrentHealth(player.GetCurrentMaxHealth());

        CombatManager.playerHealth.text = player.GetCurrentHealth() + " / " + player.GetCurrentMaxHealth();
        CombatManager.playerSlider.value = player.GetCurrentHealth();

        //Debug.Log("Armor: " + player.GetCurrentArmor() + "Health: " + player.GetCurrentHealth() + "/" + player.GetCurrentMaxHealth());
    }

    public override void AddArmor(int armor)
    {
        int def = armor + player.GetStatusDefense();
        if (def >= 0)
        {
            player.SetCurrentArmor(player.GetCurrentArmor() + def);
            CombatManager.playerArmor.text = GameManager.player.GetCurrentArmor().ToString();
        }

        //Debug.Log("Armor: " + player.GetCurrentArmor() + "Health: " + player.GetCurrentHealth() + "/" + player.GetCurrentMaxHealth());
    }

    public override void ApplyStatus(int damageBuff, int defenseBuff, int healingBuff)
    {
        player.SetStatusDamage(player.GetStatusDamage() + damageBuff);
        player.SetStatusDefense(player.GetStatusDefense() + defenseBuff);
        player.SetStatusHeal(player.GetStatusHeal() + healingBuff);
        //Debug.Log("Damage Buff: " + player.GetStatusDamage() + "Defense Buff: " + player.GetStatusDefense() + "Healing Buff " + player.GetStatusHeal());
    }

    public override void LoseHP(int hpLoss)
    {
        player.SetCurrentHealth(player  .GetCurrentHealth() - hpLoss);
        CombatManager.playerHealth.text = player.GetCurrentHealth() + " / " + player.GetCurrentMaxHealth();
        CombatManager.playerSlider.value = player.GetCurrentHealth();
        if (player.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }

    //Mana functions
    public override void SpendMana(Card card)
    {
        player.SetCurrentMana(player.GetCurrentMana()- card.cost);
        CombatManager.manaText.text = "Volts: " + GameManager.player.GetPlayer().GetCurrentMana().ToString() + " / " + GameManager.player.GetPlayer().GetCurrentMaxMana().ToString();
    }

    public override void RefillMana()
    {
        player.SetCurrentMana(player.GetCurrentMaxMana());
        CombatManager.manaText.text = "Volts: " + GameManager.player.GetPlayer().GetCurrentMana().ToString() + " / " + GameManager.player.GetPlayer().GetCurrentMaxMana().ToString();
    }


    public Player GetPlayer() { return player; }

    public override void LevelUp()
    {
        CombatManager.levelUpHUD.SetActive(true);
    }

    public override void IncreaseCurrentHandSize()
    {
        //Don't show if player has more than 10
        player.SetCurrentMaxHandSize(player.GetCurrentMaxHandSize() + 1);
        //Debug.Log("Increased Hand size to: " + player.GetCurrentMaxHandSize());
    }

    public override void IncreaseCurrentMaxHealth()
    {
        player.SetCurrentMaxHealth(player.GetCurrentMaxHealth() + 5);
        //Debug.Log("Increased Max health to: " + player.GetCurrentMaxHealth());
    }

    public override void IncreaseCurrentMaxMana()
    {
        //Don't show if player has more than 9
        player.SetCurrentMaxMana(player.GetCurrentMaxMana() + 1);
        //Debug.Log("Increased Max mana to: " + player.GetCurrentMaxMana());
    }

    public override void IncreaseComboMultiplier()
    {
        GameManager.combatManager.comboMultiplier += 1;
        //Debug.Log("Increased Combo multiplir to: " + GameManager.combatManager.comboMultiplier);
    }

    public override void Die()
    {
        //Debug.Log("Game Over");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    
#endif
        Application.Quit();
    }
}

