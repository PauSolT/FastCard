using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{

    //string path = "Cards";

    public List<Card> playerDeck = new List<Card>();

    public List<Card> combatDeck;
    public static List<Card> drawDeck;
    public static List<Card> pileDeck;
    public List<Card> usedDeck;

    [Header("Exhaust")]
    public List<Card> unusableDeck;

    public static List<CardPassive> passives;

    [Header("Wtachable lists")]
    public List<Card> seePlayerHand;    
    public List<Card> seeDrawDeck;
    public List<Card> seePileDeck;
    public List<CardPassive> seePassive;

    static Deck instance;

    public List<GameObject> cardsGO = new List<GameObject>();
    public GameObject cardPrefab;
    public GameObject canvas;

    static float waitDrawSeconds = 0.15f;

    //Initialize deck
    public void Init()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            playerDeck.Add(card);
        }
        instance = this;
    }

    //Initialize deck when starting combat
    public void StartCombat()
    {
        passives = new List<CardPassive>();
        AllPasives.Init();
        combatDeck = new List<Card>(playerDeck.Count);
        foreach (Card card in playerDeck)
        {
            combatDeck.Add(card);
        }
        drawDeck = new List<Card>(combatDeck.Count);
        pileDeck = new List<Card>(combatDeck.Count);
        //drawDeck = combatDeck;
        foreach (Card card in combatDeck)
        {
            drawDeck.Add(card);
        }

        Shuffle(drawDeck);

        DrawStartingHand(GameManager.player.GetPlayer());

    }

    public void UpdateCardDescription()
    {
        int i = 0;
        foreach(Card card in GameManager.player.GetPlayer().GetHand())
        {
            int[] sumTotal = new int[card.cardBehaviours.Count];
            
            for (int j = 0; j < card.cardBehaviours.Count; j++)
            {
                sumTotal[j] = card.cardBehaviours[j].CheckDamageBehaviour();
            }

            switch(card.cardBehaviours.Count)
            {
                case 1:
                    //card.cardDescription = string.Format(card.cardDescription, sumTotal[0]);
                    cardsGO[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0]);
                    break;
                case 2:
                    cardsGO[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0], sumTotal[1]);
                    break;
                case 3:
                    cardsGO[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0], sumTotal[1], sumTotal[2]);
                    break;
                case 4:
                    cardsGO[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0], sumTotal[1], sumTotal[2], sumTotal[3]);
                    break;
                case 5:
                    cardsGO[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0], sumTotal[1], sumTotal[2], sumTotal[3], sumTotal[4]);
                    break;
            }

            //cardsGO[i].GetComponentsInChildren<Text>()[2].text = card.cardDescription;
            i++;
        }
    }

    //Shuffle deck
    public void Shuffle(List<Card> deck)
    {

        for (int i = 0; i < deck.Count; i++)
        {
            Card cardHolder = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = cardHolder;
        }
    }

    //Drawing card related
    public IEnumerator Draw(Player player)
    {
        yield return null;
        //Draw card
        if (drawDeck.Count <= 0 )
        {
            PileToDraw();
        }

        if (CheckIfDrawCardPossible(player))
        {
            player.AddCardToPlayer(drawDeck[0]);
            DrawHandCards(drawDeck[0]);
            drawDeck.RemoveAt(0);
            UpdateCardDescription();
            CombatManager.hand.spacing += 100;
        }
    }
    public IEnumerator DrawCard(Player player)
    {
        yield return new WaitForSeconds(waitDrawSeconds);
        instance.StartCoroutine(Draw(player));
    }

    public static IEnumerator DrawStartingHand(Player player)
    {
        for (int i = 0; i < player.GetDrawSize(); i++)
        {
            yield return new WaitForSeconds(waitDrawSeconds);
            instance.StartCoroutine(instance.DrawCard(player));
        }
    }

    public void DrawHandCards(Card card)
    {
        card.CardInit();
        GameObject newCard = Instantiate(cardPrefab, canvas.transform);
        Draggable drag = newCard.GetComponent<Draggable>();
        drag.card = card;
        newCard.name = card.cardName;
        newCard.GetComponentsInChildren<Text>()[0].text = card.cardName;
        newCard.GetComponentsInChildren<Text>()[1].text = card.cost.ToString();
        newCard.GetComponentsInChildren<Text>()[2].text = card.cardDescription;
        newCard.GetComponentsInChildren<Text>()[3].text = card.cardType.ToString();
        cardsGO.Add(newCard);
    }

    public void DestroyCard(Object go)
    {
        Destroy(go);
    }
    public static bool CheckIfDrawCardPossible(Player player)
    {
        bool pos = false;
        if (player.GetCurrentHandSize() < player.GetCurrentMaxHandSize())
            pos = true;

        return pos;
    }

    public static void PileToDraw()
    {
        int it = pileDeck.Count;
        for (int i = 0; i < it; i++)
        {
            drawDeck.Add(pileDeck[0]);
            pileDeck.RemoveAt(0);
        }

        //pileDeck.Clear();
        Debug.Log("Pile deck to draw deck...");
    }

    public void UsedCardToPile(Player player, Card card)
    {
        //Apply Passives
        foreach (CardPassive cardp in passives)
        {
            if(!card.passivesApplied.Contains(cardp))
                cardp.PassiveCard(card);
        }

        //Use card
        card.CardUse();
        
        //After card is used
        if (card.discard)
            OneTimeCard(player, card);
        else if (card.cardType == Card.CardType.Passive)
            RemoveCardFromHand(player, card);
        else if (!card.discard)
            HandCardToPile(player, card);


        Debug.Log("Used " + card.cardName);
    }

    //Card destination
    public void OneTimeCard(Player player, Card card)
    {
        unusableDeck.Add(player.GetHand()[player.GetHand().IndexOf(player.GetHand().Find(x => x.GetHashCode() == card.GetHashCode()))]);
        RemoveCardFromHand(player, card);
    }

    public void HandCardToPile(Player player, Card card)
    {
        pileDeck.Add(player.GetHand()[player.GetHand().IndexOf(player.GetHand().Find(x => x.GetHashCode() == card.GetHashCode()))]);
        RemoveCardFromHand(player, card);
    }

    public void RemoveCardFromHand(Player player, Card card)
    {
        player.GetHand().RemoveAt(player.GetHand().IndexOf(player.GetHand().Find(x => x.GetHashCode() == card.GetHashCode())));
        player.SetCurrentHandSize(player.GetCurrentHandSize() - 1);
    }

    
}
