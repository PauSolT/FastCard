using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem
{

    //CardType
    public List<Card> attackCards;
    public List<Card> defenseCards;
    public List<Card> healingCards;
    public List<Card> statusCards;
    public List<Card> drawCards;
    public List<Card> passiveCards;

    //CardTier
    public List<Card> commonCards;
    public List<Card> rareCards;
    public List<Card> epicCards;
    public List<Card> legendaryCards;

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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FillAttackCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Attack)
            {
                attackCards.Add(card);
            }
        }
    }

    public void FillDefenseCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Defense)
            {
                defenseCards.Add(card);
            }
        }
    }

    public void FillHealingCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Heal)
            {
                healingCards.Add(card);
            }
        }
    }

    public void FillStatusCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Status)
            {
                statusCards.Add(card);
            }
        }
    }

    public void FillDrawingCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Draw)
            {
                drawCards.Add(card);
            }
        }
    }

    public void FillPassiveCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardType == Card.CardType.Passive)
            {
                passiveCards.Add(card);
            }
        }
    }

    public void FillCommonCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardTier == Card.CardTier.Common)
            {
                commonCards.Add(card);
            }
        }
    }

    public void FillRareCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardTier == Card.CardTier.Rare)
            {
                rareCards.Add(card);
            }
        }
    }

    public void FillEpicCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardTier == Card.CardTier.Epic)
            {
                epicCards.Add(card);
            }
        }
    }

    public void FillLegendaryCards()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            if (card.cardTier == Card.CardTier.Legendary)
            {
                legendaryCards.Add(card);
            }
        }
    }
}
