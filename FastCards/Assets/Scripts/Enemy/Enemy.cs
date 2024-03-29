﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class Enemy
{
    //------Variables------//
    //Name

    [HideInInspector] [SerializeField] string enemyName = "";

    //Health
    [HideInInspector] [SerializeField] int startingMaxHealth = 0;
    [HideInInspector] [SerializeField] int startingHealth;
    [HideInInspector] [SerializeField] int currentHealth;

    [HideInInspector] [SerializeField] int startingArmor = 0;
    [HideInInspector] [SerializeField] int currentArmor;

    //Status
    [HideInInspector] [SerializeField] int statusDamage;
    [HideInInspector] [SerializeField] int statusDefense;
    [HideInInspector] [SerializeField] int statusHeal;
    [HideInInspector] [SerializeField] EnemyBehaviour behaviour;

    //------Functions------//
    //Health related
    public int GetStartingMaxHealth() { return startingMaxHealth; }

    public int GetStartingHealth() { return startingHealth; }
    public void SetStartingHealth(int value) { startingHealth = value; }

    public int GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(int value) { currentHealth = value; }

    //Armor related
    public int GetStartingArmor() { return startingArmor; }

    public int GetCurrentArmor() { return currentArmor; }
    public void SetCurrentArmor(int value) { currentArmor = value; }

    //Name related
    public string GetName() { return enemyName; }

    //Status related
    public int GetStatusDamage() { return statusDamage; }
    public void SetStatusDamage(int value) { statusDamage = value; }

    public int GetStatusDefense() { return statusDefense; }
    public void SetStatusDefense(int value) { statusDefense = value; }

    public int GetStatusHeal() { return statusHeal; }
    public void SetStatusHeal(int value) { statusHeal = value; }

    public void SetBehaviour(EnemyBehaviour value) { behaviour = value; }
    public EnemyBehaviour GetBehaviour() { return behaviour; }

    //Combat related
    public virtual void TakeDamage(int damage) { }
    public virtual void Heal(int heal) { }
    public virtual void AddArmor(int armor) { }
    public virtual void ApplyStatus(int damageBuff, int defenseBuff, int healingBuff) { }
    public virtual void Die() { }


    //Comparison related
    public int GetHalfHealth() { return startingMaxHealth / 2; }
    public int GetThreeQuartersHealth() { return startingMaxHealth * (3 / 4); }
    public int GetOneThirdHealth() { return startingMaxHealth / 3; }
    public int GetOneFourthHealth() { return startingMaxHealth / 4; }

    public virtual void LoseHP(int hpLoss) { }
}
