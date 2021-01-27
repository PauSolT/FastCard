using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCards
{
    //CardType
    public List<Card> attackCards = new List<Card>();
    public List<Card> defenseCards = new List<Card>();
    public List<Card> healingCards = new List<Card>();
    public List<Card> statusCards = new List<Card>();
    public List<Card> drawCards = new List<Card>();
    public List<Card> passiveCards = new List<Card>();

    //CardTier
    public List<Card> commonCards = new List<Card>();
    public List<Card> rareCards = new List<Card>();
    public List<Card> epicCards = new List<Card>();
    public List<Card> legendaryCards = new List<Card>();

    //Tier + Type
    //Attack
    public List<Card> commonAttacks = new List<Card>();
    public List<Card> rareAttacks = new List<Card>();
    public List<Card> epicAttacks = new List<Card>();
    public List<Card> legendaryAttacks = new List<Card>();

    //Defense
    public List<Card> commonDefense = new List<Card>();
    public List<Card> rareDefense = new List<Card>();
    public List<Card> epicDefense = new List<Card>();
    public List<Card> legendaryDefense = new List<Card>();

    //Healing
    public List<Card> commonHealing = new List<Card>();
    public List<Card> rareHealing = new List<Card>();
    public List<Card> epicHealing = new List<Card>();
    public List<Card> legendaryHealing = new List<Card>();

    //Status
    public List<Card> commonStatus = new List<Card>();
    public List<Card> rareStatus = new List<Card>();
    public List<Card> epicStatus = new List<Card>();
    public List<Card> legendaryStatus = new List<Card>();

    //Draw
    public List<Card> commonDraw = new List<Card>();
    public List<Card> rareDraw = new List<Card>();
    public List<Card> epicDraw = new List<Card>();
    public List<Card> legendaryDraw = new List<Card>();

    //Passives
    public List<Card> commonPassive = new List<Card>();
    public List<Card> rarePassive = new List<Card>();
    public List<Card> epicPassive = new List<Card>();
    public List<Card> legendaryPassive = new List<Card>();


    public void Init()
    {
        FillAttackCards();
        FillDefenseCards();
        FillHealingCards();
        FillStatusCards();
        FillDrawingCards();
        FillPassiveCards();
        FillCommonCards();
        FillRareCards();
        FillEpicCards();
        FillLegendaryCards();
    }

    void FillAttackCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Attack)
            {
                attackCards.Add(card);

                switch (card.cardTier)
                {
                    case Card.CardTier.Common:
                        commonAttacks.Add(card);
                        break;
                    case Card.CardTier.Rare:
                        rareAttacks.Add(card);
                        break;
                    case Card.CardTier.Epic:
                        epicAttacks.Add(card);
                        break;
                    case Card.CardTier.Legendary:
                        legendaryAttacks.Add(card);
                        break;
                }
            }
        }
    }

    void FillDefenseCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Defense)
            {
                defenseCards.Add(card);

                switch(card.cardTier)
                {
                    case Card.CardTier.Common:
                        commonDefense.Add(card);
                        break;
                    case Card.CardTier.Rare:
                        rareDefense.Add(card);
                        break;
                    case Card.CardTier.Epic:
                        epicDefense.Add(card);
                        break;
                    case Card.CardTier.Legendary:
                        legendaryDefense.Add(card);
                        break;
                }
            }
        }
    }

    void FillHealingCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Heal)
            {
                healingCards.Add(card);

                switch (card.cardTier)
                {
                    case Card.CardTier.Common:
                        commonHealing.Add(card);
                        break;
                    case Card.CardTier.Rare:
                        rareHealing.Add(card);
                        break;
                    case Card.CardTier.Epic:
                        epicHealing.Add(card);
                        break;
                    case Card.CardTier.Legendary:
                        legendaryHealing.Add(card);
                        break;
                }
            }
        }
    }

    void FillStatusCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Status)
            {
                statusCards.Add(card);

                switch (card.cardTier)
                {
                    case Card.CardTier.Common:
                        commonStatus.Add(card);
                        break;
                    case Card.CardTier.Rare:
                        rareStatus.Add(card);
                        break;
                    case Card.CardTier.Epic:
                        epicStatus.Add(card);
                        break;
                    case Card.CardTier.Legendary:
                        legendaryStatus.Add(card);
                        break;
                }
            }
        }
    }

    void FillDrawingCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Draw)
            {
                drawCards.Add(card);

                switch (card.cardTier)
                {
                    case Card.CardTier.Common:
                        commonDraw.Add(card);
                        break;
                    case Card.CardTier.Rare:
                        rareDraw.Add(card);
                        break;
                    case Card.CardTier.Epic:
                        epicDraw.Add(card);
                        break;
                    case Card.CardTier.Legendary:
                        legendaryDraw.Add(card);
                        break;
                }
            }
        }
    }

    void FillPassiveCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Passive)
            {
                passiveCards.Add(card);

                switch (card.cardTier)
                {
                    case Card.CardTier.Common:
                        commonPassive.Add(card);
                        break;
                    case Card.CardTier.Rare:
                        rarePassive.Add(card);
                        break;
                    case Card.CardTier.Epic:
                        epicPassive.Add(card);
                        break;
                    case Card.CardTier.Legendary:
                        legendaryPassive.Add(card);
                        break;
                }
            }
        }
    }

    void FillCommonCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardTier == Card.CardTier.Common)
            {
                commonCards.Add(card);
            }
        }
    }

    void FillRareCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardTier == Card.CardTier.Rare)
            {
                rareCards.Add(card);
            }
        }
    }

    void FillEpicCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardTier == Card.CardTier.Epic)
            {
                epicCards.Add(card);
            }
        }
    }

    void FillLegendaryCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardTier == Card.CardTier.Legendary)
            {
                legendaryCards.Add(card);
            }
        }
    }

    //Get card lists
    //Type card
    public List<Card> GetAttackCards() { return attackCards; }
    public List<Card> GetDefenseCards() { return defenseCards; }
    public List<Card> GetHealingCards() { return healingCards; }
    public List<Card> GetStatusCards() { return statusCards; }
    public List<Card> GetDrawCards() { return drawCards; }
    public List<Card> GetPassiveCards() { return passiveCards; }
    
    //Tier card
    public List<Card> GetCommonCards() { return commonCards; }
    public List<Card> GetRareCards() { return rareCards; }
    public List<Card> GetEpicCards() { return epicCards; }
    public List<Card> GetLegendaryCards() { return legendaryCards; }
    
    //Type + Tier card
    //Attacks
    public List<Card> GetCommonAttacks() { return commonAttacks; }
    public List<Card> GetRareAttacks() { return rareAttacks; }
    public List<Card> GetEpicAttacks() { return epicAttacks; }
    public List<Card> GetLegendaryAttacks() { return legendaryAttacks; }
    //Defenses
    public List<Card> GetCommonDefense() { return commonDefense; }
    public List<Card> GetRareDefense() { return rareDefense; }
    public List<Card> GetEpicDefense() { return epicDefense; }
    public List<Card> GetLegendaryDefense() { return legendaryDefense; }
    //Healings
    public List<Card> GetCommonHealing() { return commonHealing; }
    public List<Card> GetRareHealing() { return rareHealing; }
    public List<Card> GetEpicHealing() { return epicHealing; }
    public List<Card> GetLegendaryHealing() { return legendaryHealing; }
    //Status
    public List<Card> GetCommonStatus() { return commonStatus; }
    public List<Card> GetRareStatus() { return rareStatus; }
    public List<Card> GetEpicStatus() { return epicStatus; }
    public List<Card> GetLegendaryStatus() { return legendaryStatus; }
    //Draws
    public List<Card> GetCommonDraw() { return commonDraw; }
    public List<Card> GetRareDraw() { return rareDraw; }
    public List<Card> GetEpicDraw() { return epicDraw; }
    public List<Card> GetLegendaryDraw() { return legendaryDraw; }
    //Passives
    public List<Card> GetCommonPassive() { return commonPassive; }
    public List<Card> GetRarePassive() { return rarePassive; }
    public List<Card> GetEpicPassive() { return epicPassive; }
    public List<Card> GetLegendaryPassive() { return legendaryPassive; }

}
