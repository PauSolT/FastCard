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

    public static Deck instance;

    public List<GameObject> cardsGO = new List<GameObject>();
    public GameObject cardPrefab;
    public GameObject cardRewardPrefab;
    public GameObject hand;
    public GameObject canvasDrawPile;

    static float waitDrawSeconds = 0.15f;

    //Initialize deck
    public void Init()
    {
        foreach (Card card in GameManager.cardCollection.cards)
        {
            card.CardInit();
            playerDeck.Add(card);  
        }
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                playerDeck.Add(GameManager.cardCollection.cards[i]);
            }
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

    public void UpdateCardDescription(List<Card> cardList, List<GameObject> cardGameObject)
    {
        int i = 0;
        foreach(Card card in cardList)
        {
            int[] sumTotal = new int[card.cardBehaviours.Count];
            
            for (int j = 0; j < card.cardBehaviours.Count; j++)
            {
                sumTotal[j] = card.cardBehaviours[j].CheckDamageBehaviour();
            }

            switch(card.cardBehaviours.Count)
            {
                case 1:
                    cardGameObject[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0]);
                    break;
                case 2:
                    cardGameObject[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0], sumTotal[1]);
                    break;
                case 3:
                    cardGameObject[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0], sumTotal[1], sumTotal[2]);
                    break;
                case 4:
                    cardGameObject[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0], sumTotal[1], sumTotal[2], sumTotal[3]);
                    break;
                case 5:
                    cardGameObject[i].GetComponentsInChildren<Text>()[2].text = string.Format(card.cardDescription, sumTotal[0], sumTotal[1], sumTotal[2], sumTotal[3], sumTotal[4]);
                    break;
            }

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
    void Draw(Player player)
    {
        //Draw card
        if (drawDeck.Count <= 0 )
        {
            PileToDraw();
        }

        if (CheckIfDrawCardPossible(player))
        {
            player.AddCardToPlayer(drawDeck[0]);
            DrawHandCards(drawDeck[0], hand);
            drawDeck.RemoveAt(0);
            UpdateCardDescription(GameManager.player.GetPlayer().GetHand(), GameManager.deck.cardsGO);
            CombatManager.hand.spacing += 100;
        }
    }
    public IEnumerator DrawCard(Player player)
    {
        yield return new WaitForSeconds(waitDrawSeconds);
        Draw(player);
    }

    public static IEnumerator DrawStartingHand(Player player)
    {
        for (int i = 0; i < player.GetDrawSize(); i++)
        {
            yield return new WaitForSeconds(waitDrawSeconds);
            instance.StartCoroutine(instance.DrawCard(player));
        }
    }

    public void DrawHandCards(Card card, GameObject parent)
    {
        GameObject newCard = Instantiate(cardPrefab, parent.transform);
        Draggable drag = newCard.GetComponent<Draggable>();
        drag.card = card;
        newCard.name = card.cardName;
        SetCardTexts(newCard, card);
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

    public void SeeDrawPile()
    {
        List<GameObject> drawPileGO = new List<GameObject>();


        foreach (Card card in drawDeck)
        {
            GameObject newCard = Instantiate(cardPrefab, canvasDrawPile.transform);
            newCard.GetComponent<Draggable>().enabled = false;
            newCard.name = card.cardName;
            SetCardTexts(newCard, card);
            cardsGO.Add(newCard);
        }
        foreach (Transform child in canvasDrawPile.transform)
        {
            drawPileGO.Add(child.gameObject);
        }


        UpdateCardDescription(drawDeck, drawPileGO);

        canvasDrawPile.transform.parent.parent.gameObject.SetActive(true);
    }

    public void HideDrawPile()
    {
        canvasDrawPile.transform.parent.parent.gameObject.SetActive(false);

        foreach (Transform child in canvasDrawPile.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SeePileDeck()
    {
        List<GameObject> pileDeckGO = new List<GameObject>();


        foreach (Card card in pileDeck)
        {
            GameObject newCard = Instantiate(cardPrefab, canvasDrawPile.transform);
            newCard.GetComponent<Draggable>().enabled = false;
            newCard.name = card.cardName;
            SetCardTexts(newCard, card);
            cardsGO.Add(newCard);
        }
        foreach (Transform child in canvasDrawPile.transform)
        {
            pileDeckGO.Add(child.gameObject);
        }


        UpdateCardDescription(pileDeck, pileDeckGO);

        canvasDrawPile.transform.parent.parent.gameObject.SetActive(true);
    }

    public void HidePileDeck()
    {
        canvasDrawPile.transform.parent.parent.gameObject.SetActive(false);

        foreach (Transform child in canvasDrawPile.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetCardTexts(GameObject go, Card card)
    {
        go.GetComponentsInChildren<Text>()[0].text = card.cardName;
        go.GetComponentsInChildren<Text>()[1].text = card.cost.ToString();
        go.GetComponentsInChildren<Text>()[2].text = card.cardDescription;
        go.GetComponentsInChildren<Text>()[3].text = card.cardType.ToString();
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
