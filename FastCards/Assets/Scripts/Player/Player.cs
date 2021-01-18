using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player 
{
    //------Variables------//
    //Name
    [SerializeField] string name;

    //Health
    [SerializeField] int startingMaxHealth = 0;
    [SerializeField] int currentMaxHealth;
    [SerializeField] int startingHealth = 0;
    [SerializeField] int currentHealth;

    //Armor
    [SerializeField] int startingArmor = 0;
    [SerializeField] int currentArmor;

    //Mana
    [SerializeField] int startingMaxMana = 0;
    [SerializeField] int currentMaxMana;
    [SerializeField] int startingMana = 0;
    [SerializeField] int currentMana;

    //Hand size
    [SerializeField] int startingMaxHandSize = 0;
    [SerializeField] int currentMaxHandSize;
    //int startingHandSize;
    [SerializeField] int currentHandSize;
    [SerializeField] int drawSize = 0;

    //Status
    [SerializeField] int statusDamage;
    [SerializeField] int statusDefense;
    [SerializeField] int statusHeal;

    //Levels
    [SerializeField] int startingLevel = 0;
    [SerializeField] int currentLevel;

    //Cards Hand
    [SerializeField] List<Card> hand = new List<Card>();

    //------Functions------//
    //Health related
    public int GetStartingMaxHealth() { return startingMaxHealth; }

    public int GetCurrentMaxHealth() { return currentMaxHealth; }
    public void SetCurrentMaxHealth(int value) { currentMaxHealth = value; }

    public int GetStartingHealth() { return startingHealth; }
    public void SetStartingHealth(int value) { startingHealth = value; }

    public int GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(int value) { currentHealth = value; }

    //Mana related
    public int GetStartingMaxMana() { return startingMaxMana; }

    public int GetCurrentMaxMana() { return currentMaxMana; }
    public void SetCurrentMaxMana(int value) { currentMaxMana = value; }

    public int GetStartingMana() { return startingMana; }
    public void SetStartingMana(int value) { startingMana = value; }

    public int GetCurrentMana() { return currentMana; }
    public void SetCurrentMana(int value) { currentMana = value; }

    public virtual void SpendMana(Card card) { }

    public virtual void RefillMana() { }

    //Armor
    public int GetStartingArmor() { return startingArmor; }

    public int GetCurrentArmor() { return currentArmor; }
    public void SetCurrentArmor(int value) { currentArmor = value; }

    //Hand size related
    public int GetStartingMaxHandSize() { return startingMaxHandSize; }

    public int GetCurrentMaxHandSize() { return currentMaxHandSize; }
    public void SetCurrentMaxHandSize(int value) { currentMaxHandSize = value; }

    public int GetCurrentHandSize() { return currentHandSize; }
    public void SetCurrentHandSize(int value) { currentHandSize = value; }

    public int GetDrawSize() { return drawSize; }

    //Name related
    public string GetName() { return name; }
    public void SetName(string value) { name = value; }

    //Status related
    public int GetStatusDamage() { return statusDamage; }
    public void SetStatusDamage(int value) { statusDamage = value; }

    public int GetStatusDefense() { return statusDefense; }
    public void SetStatusDefense(int value) { statusDefense = value; }

    public int GetStatusHeal() { return statusHeal; }
    public void SetStatusHeal(int value) { statusHeal = value; }

    //Level related
    public int GetStartingLevel() { return startingLevel; }

    public int GetCurrentLevel() { return currentLevel; }
    public void SetCurrentLevel(int value) { currentLevel = value; }

    //Combat related
    public virtual void TakeDamage(int damage) { }
    public virtual void Heal(int heal) { }
    public virtual void AddArmor(int armor) { }
    public virtual void ApplyStatus(int damageBuff, int defenseBuff, int healingBuff) { }
    
    public virtual void LoseHP(int hpLoss) { }

    public virtual void Die() { }

    //Comparison related
    public int GetHalfHealth() { return currentMaxHealth / 2; }
    public int GetThreeQuartersHealth() { return currentMaxHealth * (3/4); }
    public int GetOneThirdHealth() { return currentMaxHealth / 3; }
    public int GetOneFourthHealth() { return currentMaxHealth / 4; }

    //Hand related
    public List<Card> GetHand() { return hand; }
    public void AddCardToPlayer(Card card)
    {
        hand.Add(card);
        currentHandSize++;
    }
}
