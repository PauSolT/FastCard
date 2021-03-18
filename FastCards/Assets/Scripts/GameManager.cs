using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static PlayerFunctions player = new PlayerFunctions();
    public static Deck deck;
    public static CombatFunctions combatFunctions;
    public static CombatManager combatManager = new CombatManager();
    public static RewardCards rewardCards = new RewardCards();

    public List<EnemyFunctions> enemies;

    public static RewardSystem rewardSystem = new RewardSystem();

    public Card[] cardsCreated;
    public List<Card> showCardsCreated;

    public static CardCollection cardCollection;

    public static GameObject goDeck;

    public static int currentEnemy = 0;

    public void NextEnemy()
    {
        //Add the cardsm picked by the player to the player deck
        rewardSystem.AddRewardCardsToPlayer();
        //Next enemy
        currentEnemy++;
        //Initialize enemy
        combatManager.InitEnemy(enemies[currentEnemy]);
        //Set HUD to player and enemy
        combatManager.SetStartingHUD();
        //Hide button of reward cards
        CombatManager.addCards.gameObject.SetActive(false);
        //Hide viewport of reward cards
        deck.canvasDrawPile.transform.parent.parent.gameObject.SetActive(false);
        //Show enemy HUD
        CombatManager.enemyHUD.SetActive(true);
    }
    void Awake()
    {
        //Starts game with enemy selected
        StartGame(currentEnemy);
    }

    public void StartGame(int enemyNumber)
    {
        //Initialize player
        player.Init();
        //Gets deck script from GameManager
        deck = GetComponent<Deck>();
        //Gets combat functions from GameManager
        combatFunctions = GetComponent<CombatFunctions>();

        //Initialize together
        //Initialize enemy selected
        combatManager.InitEnemy(enemies[enemyNumber]);
        //Initialize combat
        combatManager.InitCombat();
        //Set HUD to player and enemy
        combatManager.SetStartingHUD();
    }

    // Update is called once per frame
    private void Update()
    {
        combatManager.CombatInputs();
    }

    public static void InitCards()
    {
        {
            //Reads cards from JSON 
            using (StreamReader stream = new StreamReader(Path.Combine(Application.streamingAssetsPath, "CardData.json")))
            {
                string json = stream.ReadToEnd();
                cardCollection = JsonUtility.FromJson<CardCollection>(json);
            }
        }
        
        
        //foreach (Card card in cardCollection.cards)
        //{
        //    GameObject go = new GameObject();
        //    go.AddComponent<CardHolder>();
        //    CardHolder cardHolder = go.GetComponent<CardHolder>();
        //    cardHolder.card = card;
        //    go.name = card.cardName;

        //    //go.transform.parent = goDeck.transform;
        //}
    }
}
