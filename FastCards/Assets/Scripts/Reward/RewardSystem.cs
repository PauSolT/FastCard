using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem
{
    List<Card> rewardCards = new List<Card>();
    List<GameObject> rewardCardsGO = new List<GameObject>();

    int commonPossibilities = 100;
    int rarePossibilities = 45;
    int epicPossibilities = 5;
    //int legendaryPossibilities = 0;

    public static int numMaxRewards = 3;

    int numRewardCards = 5;

    int maxPossibility = 150;

    public static int rewardsSelected;

    public void GetRewards()
    {
        SelectCards();
        GameManager.deck.rewardContent.transform.parent.parent.gameObject.SetActive(true);
    }

    void SelectCards()
    {
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
                {
                    rewardCards.Add(GameManager.rewardCards.GetLegendaryCards()[randCard]);
                    cardsSelected++;
                }
            }


        } while (cardsSelected < numRewardCards);


        foreach (Card card in rewardCards)
        {
            GameObject newCard = GameManager.Instantiate(GameManager.deck.cardRewardPrefab, GameManager.deck.rewardContent.transform);
            newCard.GetComponent<Selectable>().card = card;
            newCard.name = card.cardName;
            GameManager.deck.SetCardTexts(newCard, card);
            newCard.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = card.colorCard;
            rewardCardsGO.Add(newCard);
        }

        //Set combo to 0 so reward cards have correct values
        GameManager.combatManager.ResetCombo();
        GameManager.deck.UpdateCardDescription(rewardCards, rewardCardsGO);
    }


    public void AddRewardCardsToPlayer()
    {
        foreach (Selectable select in GameManager.deck.rewardContent.transform.GetComponentsInChildren<Selectable>())
        {
            if(select.selected)
            {
                GameManager.deck.playerDeck.Add(select.card);
            }
        }

        for (byte i = 0; i < rewardCards.Count;)
        {
            rewardCards.RemoveAt(0);
            GameManager.Destroy(rewardCardsGO[0]);
            rewardCardsGO.RemoveAt(0);
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
