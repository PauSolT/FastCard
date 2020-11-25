using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    string path = "Cards";

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

    public void Init()
    {
        foreach (Card card in Resources.LoadAll<Card>(path))
        {
            playerDeck.Add(card);
        }
        instance = this;
    }

    public void StartCombat()
    {
        passives = new List<CardPassive>();
        AllPasives.Init();
        combatDeck = new List<Card>(playerDeck.Count);
        foreach (Card card in playerDeck)
        {
            combatDeck.Add(Instantiate(card));
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

    public static IEnumerator Draw(Player player)
    {
        yield return new WaitForSeconds(0.33f);
        //Draw card
        if (drawDeck.Count <= 0 )
        {
            PileToDraw();
        }

        if (CheckIfDrawCardPossible(player))
            player.AddCardToPlayer(drawDeck[0]);

        drawDeck.RemoveAt(0);
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
        foreach (Card card in pileDeck)
        {
            drawDeck.Add(card);
        }
        pileDeck.Clear();
        Debug.Log("Pile deck to draw deck...");
    }

    public void UsedCardToPile(Player player, Card card)
    {
        //Apply Passives
        foreach (CardPassive cardp in passives)
        {
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

    void Update()
    {
        seePlayerHand = GameManager.player.GetPlayer().GetHand();
        seeDrawDeck = drawDeck;
        seePileDeck = pileDeck;
        seePassive = passives;

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCombat();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.player.TakeDamage(6);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            int it = GameManager.player.GetPlayer().GetHand().Count;
            for (int i = 0; i < it; i++)
            {
                HandCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[0]);
            }

            DrawStartingHand(GameManager.player.GetPlayer());
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            PileToDraw();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (Card card in GameManager.player.GetPlayer().GetHand())
            {
                Debug.Log("Draw " + card.cardName);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && GameManager.player.GetPlayer().GetHand().Count >=1 )
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && GameManager.player.GetPlayer().GetHand().Count >= 2)
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && GameManager.player.GetPlayer().GetHand().Count >= 3)
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[2]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && GameManager.player.GetPlayer().GetHand().Count >= 4)
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[3]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && GameManager.player.GetPlayer().GetHand().Count >= 5)
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[4]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && GameManager.player.GetPlayer().GetHand().Count >= 6)
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[5]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7) && GameManager.player.GetPlayer().GetHand().Count >= 7)
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[6]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) && GameManager.player.GetPlayer().GetHand().Count >= 8)
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[7]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9) && GameManager.player.GetPlayer().GetHand().Count >= 9)
        {
            UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[8]);
        }

    }
}
