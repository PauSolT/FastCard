using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListTry : MonoBehaviour
{

    string path = "Cards";

    public List<Card> playerDeck = new List<Card>();

    public List<Card> combatDeck;
    public List<Card> drawDeck;
    public List<Card> pileDeck;

    public static List<CardPassive> passives;

    int itDraw = 0;

    public void Init()
    {
        foreach (Card card in Resources.LoadAll<Card>(path))
        {
            playerDeck.Add(card);
        }
        drawDeck = new List<Card>(combatDeck.Count);
        pileDeck = new List<Card>(combatDeck.Count);
        passives = new List<CardPassive>();
    }

    public void StartCombat()
    {
        AllPasives.Init();
        combatDeck = new List<Card>(playerDeck.Count);
        foreach (Card card in playerDeck)
        {
            combatDeck.Add(Instantiate(card));
        }
        drawDeck = new List<Card>(combatDeck.Count);
        pileDeck = new List<Card>(combatDeck.Count);
        drawDeck = combatDeck;
        foreach (Card card in drawDeck)
        {
            card.CardInit();
        }

        Shuffle(drawDeck);
        DrawStartingHand(GameManager.player.GetPlayer());
        
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
        if (drawDeck[itDraw] == null)
        {
            PileToDraw();
        }

        player.AddCardToPlayer(drawDeck[itDraw]);
        drawDeck[itDraw] = null;
        itDraw++;
    }

    public void DrawStartingHand(Player player)
    {
        for (int i = 0; i < player.GetDrawSize(); i++)
        {
            StartCoroutine(DrawCard(player));
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
        Debug.Log("Pile deck to draw deck...");
    }

    public void UsedCardToPile(Player player, Card card)
    {
        if (passives.Count > 0)
        {
            foreach (CardPassive cardp in passives)
            {
                cardp.PassiveCard(card);
            }
        }
       
        card.CardUse();
        pileDeck.Add(player.GetHand()[player.GetHand().IndexOf(player.GetHand().Find(x => x.GetHashCode() == card.GetHashCode()))]);
        player.GetHand()[player.GetHand().IndexOf(player.GetHand().Find(x => x.GetHashCode() == card.GetHashCode()))] = null;
        player.SetCurrentHandSize(player.GetCurrentHandSize() - 1);

        Debug.Log("Used " + card.cardName);
    }

    private void Start()
    {
        Init();
        GameManager.player.Init();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCombat();
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.player.TakeDamage(6);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (Card card in GameManager.player.GetPlayer().GetHand())
            {
                Debug.Log("Draw " + card.cardName);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[1]);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[2]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[3]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[4]);
        }


    }
}
