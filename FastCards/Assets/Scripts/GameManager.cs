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
        rewardSystem.AddRewardCardsToPlayer();
        currentEnemy++;
        combatManager.InitEnemy(enemies[currentEnemy]);
        combatManager.SetStartingHUD();
    }
    void Awake()
    {
        StartGame(currentEnemy);
    }

    public void StartGame(int enemyNumber)
    {
        player.Init();
        deck = GetComponent<Deck>();
        combatFunctions = GetComponent<CombatFunctions>();

        //Initialize together
        combatManager.InitEnemy(enemies[enemyNumber]);
        combatManager.InitCombat();
        combatManager.SetStartingHUD();
    }

    // Update is called once per frame
    private void Update()
    {
        combatManager.CombatInputs();
    }

    public static void InitCards()
    {
        //string path = Application.dataPath + "/SaveFile.json";
        //string srPath = Path.Combine(Application.streamingAssetsPath, "SavedGame.json");
        //if (File.Exists(path))
        //{
        //    File.WriteAllText(srPath, File.ReadAllText(path));
        //    using (StreamReader stream = new StreamReader(srPath))
        //    {
        //        string json = stream.ReadToEnd();
        //        cardCollection = JsonUtility.FromJson<CardCollection>(json);
        //    }
        //    File.Delete(srPath);
        //}
        //else if (!File.Exists(path))
        {
            using (StreamReader stream = new StreamReader(Path.Combine(Application.streamingAssetsPath, "CardData.json")))
            {
                string json = stream.ReadToEnd();
                cardCollection = JsonUtility.FromJson<CardCollection>(json);
            }
        }
        

        foreach (Card card in cardCollection.cards)
        {
            GameObject go = new GameObject();
            go.AddComponent<CardHolder>();
            CardHolder cardHolder = go.GetComponent<CardHolder>();
            cardHolder.card = card;
            go.name = card.cardName;

            //go.transform.parent = goDeck.transform;
        }
    }
}
