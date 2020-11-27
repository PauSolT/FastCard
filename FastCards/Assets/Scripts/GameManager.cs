using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static PlayerFunctions player = new PlayerFunctions();
    public static Deck deck;
    public static CombatManager combatManager = new CombatManager();

    public static List<EnemyFunctions> enemies = new List<EnemyFunctions>();


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
    }

    // Update is called once per frame
    private void Update()
    {
        combatManager.CombatInputs();
    }
}
