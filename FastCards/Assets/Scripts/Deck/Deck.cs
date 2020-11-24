using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Deck : MonoBehaviour
{
    string path = "Cards";

    public List<Card> playerDeck = new List<Card>();

    public List<Card> combatDeck;
    public List<Card> drawDeck;
    public List<Card> pileDeck;

    public static List<CardPassive> passives;


    public void Init()
    {
        foreach (Card card in Resources.LoadAll<Card>(path))
        {
            playerDeck.Add(card);
        }
        drawDeck = new List<Card>(combatDeck.Count);
        pileDeck = new List<Card>();
    }

    public void StartCombat()
    {
        combatDeck = new List<Card>(playerDeck.Count);
        foreach (Card card in playerDeck)
        {
            combatDeck.Add(Instantiate(card));
        }
        drawDeck = new List<Card>(combatDeck.Count);
        pileDeck = new List<Card>(combatDeck.Count);

        drawDeck = combatDeck;
        for (int i = 0; i < GameManager.player.GetPlayer().GetCurrentMaxHandSize(); i++)
        {
            DrawCard(GameManager.player.GetPlayer());
        }
    }

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

    public IEnumerator DrawCard(Player player)
    {
        yield return new WaitForSeconds(0.33f);
        //Draw card
        if (drawDeck[0] == null)
        {
            PileToDraw();
        }

        player.AddCardToPlayer(drawDeck[0]);
        drawDeck[0] = null;
    }

    public void DrawStartingHand(Player player)
    {
        for (int i = 0; i < player.GetDrawSize(); i++)
        {
            DrawCard(player);
        }
    }

    public bool CheckIfDrawCardPossible(Player player)
    {
        bool pos = false;
        if (player.GetCurrentHandSize() < player.GetCurrentMaxHandSize())
            pos = true;

        return pos;
    }

    public void PileToDraw()
    {
        
        drawDeck = pileDeck;
        pileDeck.Clear();

    }
}
