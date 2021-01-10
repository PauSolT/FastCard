using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //Debug.Log("Max hand size: " + player.GetStartingMaxHandSize());
        //Debug.Log("Health: " + player.GetCurrentHealth() + "/" + player.GetCurrentMaxHealth());
        //Debug.Log("Name: " + player.GetName());
        //Debug.Log("Level: " + player.GetStartingLevel());
    }


    public override void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        if (player.GetCurrentArmor() > 0)
        {
            int damageHealth = damage - player.GetCurrentArmor();
            if(damageHealth <= 0)
                player.SetCurrentArmor(player.GetCurrentArmor() - damage);
            else if(damageHealth > 0)
            {
                player.SetCurrentArmor(0);
                LoseHP(damageHealth);
            }
        }
        else
        {
            LoseHP(damage);
        }
        Debug.Log("Armor: " + player.GetCurrentArmor() + "Health: " + player.GetCurrentHealth() + "/" + player.GetCurrentMaxHealth());

        
    }

    public override void Heal(int heal)
    {
        int healing = heal + player.GetStatusHeal();
        if (healing >= 0)
            player.SetCurrentHealth(player.GetCurrentHealth() + healing);

        if (player.GetCurrentHealth() >= player.GetCurrentMaxHealth())
            player.SetCurrentHealth(player.GetCurrentMaxHealth());

        Debug.Log("Armor: " + player.GetCurrentArmor() + "Health: " + player.GetCurrentHealth() + "/" + player.GetCurrentMaxHealth());
    }

    public override void AddArmor(int armor)
    {
        int def = armor + player.GetStatusDefense();
        if (def >= 0)
            player.SetCurrentArmor(player.GetCurrentArmor() + def);

        Debug.Log("Armor: " + player.GetCurrentArmor() + "Health: " + player.GetCurrentHealth() + "/" + player.GetCurrentMaxHealth());
    }

    public override void ApplyStatus(int damageBuff, int defenseBuff, int healingBuff)
    {
        player.SetStatusDamage(player.GetStatusDamage() + damageBuff);
        player.SetStatusDefense(player.GetStatusDefense() + defenseBuff);
        player.SetStatusHeal(player.GetStatusHeal() + healingBuff);
        Debug.Log("Damage Buff: " + player.GetStatusDamage() + "Defense Buff: " + player.GetStatusDefense() + "Healing Buff " + player.GetStatusHeal());
    }

    public override void LoseHP(int hpLoss)
    {
        player.SetCurrentHealth(player.GetCurrentHealth() - hpLoss);
        if (player.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }

    public Player GetPlayer() { return player; }

    public override void Die()
    {
        Debug.Log("Game Over");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    
#endif
        Application.Quit();
    }
}

