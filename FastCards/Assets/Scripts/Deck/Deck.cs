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

    //Initialize deck
    public void Init()
    {
        //foreach (Card card in Resources.LoadAll<Card>(path))
        //{
        //    playerDeck.Add(card);
        //}
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
            //combatDeck.Add(Instantiate(card));
        }
        drawDeck = new List<Card>(combatDeck.Count);
        pileDeck = new List<Card>(combatDeck.Count);
        //drawDeck = combatDeck;
        foreach (Card card in combatDeck)
        {
            drawDeck.Add(card);

        }

        foreach (Card card in drawDeck)
        {
            card.CardInit();
            GameObject newCard = Instantiate(cardPrefab, canvas.transform);
            newCard.name = card.cardName;
            newCard.GetComponentsInChildren<Text>()[0].text = card.cardName;
            newCard.GetComponentsInChildren<Text>()[1].text = card.cost.ToString();
            newCard.GetComponentsInChildren<Text>()[2].text = card.cardDescription;
            newCard.GetComponentsInChildren<Text>()[3].text = card.cardType.ToString();
            cardsGO.Add(newCard);
        }

        Shuffle(drawDeck);
        DrawStartingHand(GameManager.player.GetPlayer());


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
    public static IEnumerator Draw(Player player)
    {
        yield return new WaitForSeconds(0.33f);
        //Draw card
        if (drawDeck.Count <= 0 )
        {
            PileToDraw();
        }

        if (CheckIfDrawCardPossible(player))
        {
            player.AddCardToPlayer(drawDeck[0]);
            drawDeck.RemoveAt(0);
        }

    }
    public static void DrawCard(Player player)
    {
        instance.StartCoroutine(Draw(player));
    }

    public void DrawStartingHand(Player player)
    {
        for (int i = 0; i < player.GetDrawSize(); i++)
        {
            StartCoroutine(Draw(player));
        }
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
