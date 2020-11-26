using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager 
{

    public static EnemyFunctions enemy;
    public bool playerTurn = true;
    int combo;
    float comboSeconds =0f;
    //float currentComboSeconds = 0f;

    float turnSeconds =0f;
    //float currentTurnSeconds = 0f;

    public Slider playerSlider;
    public Slider enemySlider;
    public Slider comboSlider;
    public Slider turnSlider;

    public Text playerHealth;
    public Text enemyHealth;
    public Text playerArmor;
    public Text enemyArmor;
    public Text playerName;
    public Text enemyName;

    TextAsset jsonFile;
    string path = "Jsons/CombatValues";

    public void Init(EnemyFunctions combatEnemy)
    {
        jsonFile = Resources.Load(path) as TextAsset;
        GameManager.combatManager = JsonUtility.FromJson<CombatManager>(jsonFile.text);
        enemy = combatEnemy;
        GameManager.deck.Init();
        GameManager.deck.StartCombat();
    }

    void EndPlayerTurn()
    {
        int it = GameManager.player.GetPlayer().GetHand().Count;
        for (int i = 0; i < it; i++)
        {
            GameManager.deck.HandCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[0]);
        }
        playerTurn = false;
    }

    void StartPlayerTurn()
    {
        playerTurn = true;
        GameManager.deck.DrawStartingHand(GameManager.player.GetPlayer());
    }

    public void CombatInputs()
    {
        //playerSlider.maxValue = GameManager.player.GetPlayer().GetCurrentMaxHealth();
        //playerSlider.value = GameManager.player.GetPlayer().GetCurrentHealth();
        //enemySlider.maxValue = enemy.GetEnemy().GetStartingMaxHealth();
        //enemySlider.value = enemy.GetEnemy().GetCurrentHealth();

        //playerHealth.text = GameManager.player.GetPlayer().GetCurrentHealth() + "/" + GameManager.player.GetPlayer().GetCurrentMaxHealth();
        //enemyHealth.text = enemy.GetEnemy().GetCurrentHealth() + "/" + enemy.GetEnemy().GetStartingMaxHealth();
        //playerArmor.text = GameManager.player.GetPlayer().GetCurrentArmor().ToString();
        //enemyArmor.text = enemy.GetCurrentArmor().ToString();
        //playerName.text = GameManager.player.GetPlayer().GetName();
        //enemyName.text = enemy.GetEnemy().GetName();

        GameManager.deck.seePlayerHand = GameManager.player.GetPlayer().GetHand();
        GameManager.deck.seeDrawDeck = Deck.drawDeck;
        GameManager.deck.seePileDeck = Deck.pileDeck;
        GameManager.deck.seePassive = Deck.passives; 

        if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.player.TakeDamage(6);
        }

        if (Input.GetKeyDown(KeyCode.E) && playerTurn)
        {
            EndPlayerTurn();
        }

        if (Input.GetKeyDown(KeyCode.R) && !playerTurn)
        {
            StartPlayerTurn();
        }

        if (playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && GameManager.player.GetPlayer().GetHand().Count >= 1)
            {
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[0]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && GameManager.player.GetPlayer().GetHand().Count >= 2)
            {
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[1]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && GameManager.player.GetPlayer().GetHand().Count >= 3)
            {
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[2]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && GameManager.player.GetPlayer().GetHand().Count >= 4)
            {
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[3]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) && GameManager.player.GetPlayer().GetHand().Count >= 5)
            {
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[4]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6) && GameManager.player.GetPlayer().GetHand().Count >= 6)
            {
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[5]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha7) && GameManager.player.GetPlayer().GetHand().Count >= 7)
            {
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[6]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha8) && GameManager.player.GetPlayer().GetHand().Count >= 8)
            {
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[7]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha9) && GameManager.player.GetPlayer().GetHand().Count >= 9)
            {
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[8]);
            }
        }
        

        
    }
}
