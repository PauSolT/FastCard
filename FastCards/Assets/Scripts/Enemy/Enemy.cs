using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    //------Variables------//
    //Health
    int startingMaxHealth = 0;
    int currentMaxHealth;
    int startingHealth;
    int currentHealth;

    int startingArmor = 0;
    int currentArmor;

    //Status
    int statusDamage;
    int statusDefense;
    int statusHeal;

    //------Functions------//
    //Health related
    public int GetStartingMaxHealth() { return startingMaxHealth; }

    public int GetCurrentMaxHealth() { return currentMaxHealth; }
    public void SetCurrentMaxHealth(int value) { currentMaxHealth = value; }

    public int GetStartingHealth() { return startingHealth; }
    public void SetStartingHealth(int value) { startingHealth = value; }

    public int GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(int value) { currentHealth = value; }

    //Armor
    public int GetStartingArmor() { return startingArmor; }

    public int GetCurrentArmor() { return currentArmor; }
    public void SetCurrentArmor(int value) { currentArmor = value; }

    //Status related
    public int GetStatusDamage() { return statusDamage; }
    public void SetStatusDamage(int value) { statusDamage = value; }

    public int GetStatusDefense() { return statusDefense; }
    public void SetStatusDefense(int value) { statusDefense = value; }

    public int GetStatusHeal() { return statusHeal; }
    public void SetStatusHeal(int value) { statusHeal = value; }

    //Combat related
    public virtual void TakeDamage(int damage) { }
    public virtual void Heal(int heal) { }
    public virtual void AddArmor(int armor) { }
    public virtual void ApplyStatus(int damageBuff, int defenseBuff, int healingBuff) { }
}
