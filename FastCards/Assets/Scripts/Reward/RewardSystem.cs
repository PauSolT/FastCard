﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem
{
    List<Card> rewardCards;
    List<GameObject> rewardCardsGO;

    int commonPossibilities = 150;
    int rarePossibilities = 45;
    int epicPossibilities = 5;
    //int legendaryPossibilities = 0;

    public static int numMaxRewards = 3;

    int numRewardCards = 4;

    int maxPossibility = 150;

    public static int rewardsSelected;

    public void GetRewards()
    {
        SelectCards();
        GameManager.deck.canvasDrawPile.transform.parent.parent.gameObject.SetActive(true);
    }

    void SelectCards()
    {
        rewardCards = new List<Card>();
        rewardCardsGO = new List<GameObject>();
        int randNum;
        int randCard;
        int cardsSelected = 0;
        rewardsSelected = 0;
        do
        {
            randNum = Random.Range(0, maxPossibility + 1);

            if (randNum <= commonPossibilities)
            {
                randCard = Random.Range(0, GameManager.rewardCards.GetCommonCards().Count);
                if (!rewardCards.Contains(GameManager.rewardCards.GetCommonCards()[randCard]))
                {
                    rewardCards.Add(GameManager.rewardCards.GetCommonCards()[randCard]);
                    cardsSelected++;
                }
            }
            else if (randNum <= (commonPossibilities + rarePossibilities))
            {
                randCard = Random.Range(0, GameManager.rewardCards.GetRareCards().Count);
                if (!rewardCards.Contains(GameManager.rewardCards.GetRareCards()[randCard]))
                {
                    rewardCards.Add(GameManager.rewardCards.GetRareCards()[randCard]);
                    cardsSelected++;
                }
            }
            else if (randNum <= (commonPossibilities + rarePossibilities + epicPossibilities))
            {
                randCard = Random.Range(0, GameManager.rewardCards.GetEpicCards().Count);
                if (!rewardCards.Contains(GameManager.rewardCards.GetEpicCards()[randCard]))
                {
                    rewardCards.Add(GameManager.rewardCards.GetEpicCards()[randCard]);
                    cardsSelected++;
                }
            }
            else
            {
                randCard = Random.Range(0, GameManager.rewardCards.GetLegendaryCards().Count);
                if (!rewardCards.Contains(GameManager.rewardCards.GetLegendaryCards()[randCard]))
                    rewardCards.Add(GameManager.rewardCards.GetLegendaryCards()[randCard]);
                {
                    cardsSelected++;
                }
            }


        } while (cardsSelected < numRewardCards);


        foreach (Card card in rewardCards)
        {
            GameObject newCard = GameManager.Instantiate(GameManager.deck.cardRewardPrefab, GameManager.deck.canvasDrawPile.transform);
            newCard.GetComponent<Selectable>().card = card;
            newCard.name = card.cardName;
            GameManager.deck.SetCardTexts(newCard, card);
            rewardCardsGO.Add(newCard);
        }

        GameManager.deck.UpdateCardDescription(rewardCards, rewardCardsGO);
    }


    public void AddRewardCardsToPlayer()
    {
        foreach (Selectable select in GameManager.deck.canvasDrawPile.transform.GetComponentsInChildren<Selectable>())
        {
            if(select.selected)
            {
                GameManager.deck.playerDeck.Add(select.card);
            }
        }
    }

    public static void IncreaseRewardsSelected()
    {
        rewardsSelected++;
    }

    public static void DecreaseRewardsSelected()
    {
        rewardsSelected--;
    }
}
