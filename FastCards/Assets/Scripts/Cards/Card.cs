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

    [Serializable]
    public enum CardTier
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    //Essentials
    [SerializeField] public int id;
    [SerializeField] public string cardName = null;
    [SerializeField] public string cardDescription = null;

    [SerializeField] public int cost;

    [SerializeField] public bool discard;

    [SerializeField] public CardType cardType;
    [SerializeField] public CardTier cardTier;

    [SerializeField] public List<CardBehaviour> cardBehaviours = new List<CardBehaviour>();
    //Events
    [SerializeField]
    protected Action<PlayerFunctions> cardUse;

    [SerializeField] public Color colorCard;
    [SerializeField] public Color colorBgCard;

    //Virtual functions so childs use them
    //Init card behaviours
    public virtual void CardInit()
    {
        foreach(CardBehaviour cb in cardBehaviours)
        {
            cb.InitCardBehaviour();
        }
    }

    public virtual void CardUse()
    {
        //Card functionality
        foreach (CardBehaviour c in cardBehaviours)
        {
            c.ExecuteCardBehaviour();
        }

        //Build up combo
        if (GameManager.combatManager.currentComboSeconds > 0f && 
            GameManager.combatManager.comboStarted)
        {
            GameManager.combatManager.BuildCombo();
        }

        //Start combo
        if (!GameManager.combatManager.comboStarted)
        {
            GameManager.combatManager.StartCombo();
            GameManager.combatManager.BuildCombo();
        }
        //Put used card on used card deck
        GameManager.deck.UpdateCardDescription(GameManager.player.GetPlayer().GetHand(), GameManager.deck.cardsGO);
        //If enemy has a trigger, update their intentions
        CombatManager.enemy.GetEnemy().GetBehaviour().UpdateIntention();

    }

    
    protected void OneTimeUse()
    {

    }

    public string GetCardName() { return cardName; }
    public void SetCardName(string value) { cardName = value; }
}