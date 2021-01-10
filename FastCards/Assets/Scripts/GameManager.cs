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
        cardsCreated = JsonHelper.FromJson<Card>(jsonData);

        for (int i = 0; i < cardsCreated.Length; i++)
        {
            showCardsCreated.Add(cardsCreated[i]);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        combatManager.CombatInputs();
    }
}
