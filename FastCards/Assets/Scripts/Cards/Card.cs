using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public abstract class Card : ScriptableObject
{
    //Enums
    public enum CardType
    {
        Attack,
        Defense,
        Status,
        Heal,
        Draw,
        Passive
    }

    //Essentials
    [SerializeField] public string cardName = null;
    [SerializeField] public string cardDescription = null;

    [SerializeField] public int cost;

    [SerializeField] public bool discard;
    [SerializeField] public bool self;

    [SerializeField] public CardType cardType;

    //Value types
    [SerializeField] public int damage;
    [SerializeField] public int armor;
    [SerializeField] public int heal;
    [SerializeField] public int draw;

    [SerializeField] public int statusDamage;
    [SerializeField] public int statusDefense;
    [SerializeField] public int statusHealing;

    //Events
    protected Action<PlayerFunctions> cardUse;

    //Basic Events
    protected void AttackCard(PlayerFunctions player)
    {
        int fullDamage = damage + player.GetStatusDamage();
        if (fullDamage >= 0)
            player.TakeDamage(fullDamage);
    }

    public void DefenseCard(PlayerFunctions player)
    {
        player.AddArmor(armor);
    }

    public void HealCard(PlayerFunctions player)
    {
        player.Heal(heal);
    }

    protected void StatusCard(PlayerFunctions player)
    {
        player.ApplyStatus(statusDamage, statusDefense, statusHealing);
    }

    protected void DrawCard(PlayerFunctions player)
    {
        for (int i = 0; i < draw; i++)
        {
            Deck.DrawCard(player.GetPlayer());
        }
    }

    //Virtual functions so childs use them
    public virtual void CardInit()
    {
        
    }

    public virtual void CardUse()
    {
        
    }

    protected void OneTimeUse()
    {

    }

    public string GetCardName() { return cardName; }
    public void SetCardName(string value) { cardName = value; }
}


