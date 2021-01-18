using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[Serializable]
public class Card 
{
    //Enums
    [Serializable]
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

    [SerializeField] public CardType cardType;

    [SerializeField] public List <CardPassive> passivesApplied = new List<CardPassive>();

    [SerializeField] public List<CardBehaviour> cardBehaviours = new List<CardBehaviour>();
    //Events
    [SerializeField]
    protected Action<PlayerFunctions> cardUse;

    //Virtual functions so childs use them
    public virtual void CardInit()
    {
        foreach(CardBehaviour cb in cardBehaviours)
        {
            cb.Init();
        }
    }

    public virtual void CardUse()
    {
        foreach (CardBehaviour c in cardBehaviours)
        {
            c.Execute();
        }

        if (GameManager.combatManager.currentComboSeconds > 0f)
        {
            GameManager.combatManager.BuildCombo();
        }

        GameManager.deck.UpdateCardDescription();
    }


    protected void OneTimeUse()
    {

    }

    public string GetCardName() { return cardName; }
    public void SetCardName(string value) { cardName = value; }
}