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

    /*[SerializeField]*/public Color colorCard;
    /*[SerializeField]*/public Color colorBgCard;

    //Virtual functions so childs use them
    public virtual void CardInit()
    {
        foreach(CardBehaviour cb in cardBehaviours)
        {
            cb.Init();
        }

        switch (cardTier)
        {
            case CardTier.Common:
                //colorCard = new Color(0.1831613f, 0.7924528f, 0.210603f);
                colorBgCard = new Color(0.631052f, 0.8207547f, 0.6396192f);
                break;
            case CardTier.Rare:
                //colorCard = new Color(0.05598078f, 0.4471672f, 0.6981132f);
                colorBgCard = new Color(0.508366f, 0.7501974f, 0.9056604f);
                break;
            case CardTier.Epic:
                //colorCard = new Color(0.7138336f, 0.1642044f, 0.7735849f);
                colorBgCard = new Color(0.8453416f, 0.6345674f, 0.8679245f);
                break;
            case CardTier.Legendary:
                //colorCard = new Color(0.8392157f, 0.1568318f, 0.1215686f);
                colorBgCard = new Color(0.9528302f, 0.7519784f, 0.7415895f);
                break;
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

        GameManager.deck.UpdateCardDescription(GameManager.player.GetPlayer().GetHand(), GameManager.deck.cardsGO);
    }


    protected void OneTimeUse()
    {

    }

    public string GetCardName() { return cardName; }
    public void SetCardName(string value) { cardName = value; }
}