using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static PlayerFunctions player = new PlayerFunctions();
    public static Deck deck;
    public static CombatManager combatManager = new CombatManager();

    public static List<EnemyFunctions> enemies = new List<EnemyFunctions>();

    public Card[] cardsCreated;
    public List<Card> showCardsCreated;

    public CardCollection cardCollection;


    void Awake()
    {
        player.Init();
        deck = GetComponent<Deck>();
        for (int i = 0; i < 1; i++)
        {
            enemies.Add(new EnemyFunctions());
            enemies[i].Init(i.ToString());
        }
        combatManager.Init(enemies[0]);
        LookUpTable.LoadTable();

        string jsonData = File.ReadAllText("Assets/Resources/Jsons/CardData.json");
        Debug.Log(jsonData);

        ;

        using (StreamReader stream = new StreamReader("Assets/Resources/Jsons/CardData.json"))
        {
            string json = stream.ReadToEnd();
            cardCollection = JsonUtility.FromJson<CardCollection>(json);
        }

        Debug.Log(cardCollection.cards[0].cardName);
        Debug.Log(cardCollection.cards[1].cardName);
        Debug.Log(cardCollection.cards[2].cardName);
        cardCollection.cards[cardCollection.cards.Length - 1].CardUse();
        //for (int i = 0; i < cardsCreated.Length; i++)
        //{
        //    showCardsCreated.Add(cardsCreated[i]);
        //}
    }

    // Update is called once per frame
    private void Update()
    {
        combatManager.CombatInputs();
    }
}
