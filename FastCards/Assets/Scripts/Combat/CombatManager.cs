﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager 
{

    public static EnemyFunctions enemy;
    public bool playerTurn = true;
    public int combo = 0;
    public int comboBuilder = 0;
    public float comboMultiplier = 0; 
    public float comboSeconds = 5f;
    public static HorizontalLayoutGroup hand;
    public float currentComboSeconds = 0f;


    [SerializeField] float turnSeconds = 10f;
    float currentTurnSeconds = 0f;

    //Sliders
    public static Slider playerSlider;
    public static Slider enemySlider;
    public static Slider comboSlider;
    public static Slider turnSlider;
    public static Slider timeSlider;

    //Texts 
    public static Text playerHealth;
    public static Text enemyHealth;
    public static Text playerArmor;
    public static Text enemyArmor;
    public static Text playerName;
    public static Text enemyName;
    public static Text comboText;
    public static Text manaText;
    public static Text intentionText;
    public static Text endTurnInfo;

    //Buttons
    public static Button endTurnButton;

    //LevelUp
    public static GameObject levelUpHUD;

    //Comparison related
    //Cards type played this round
    public static int attackCardsRound = 0;
    public static int defendCardsRound = 0;
    public static int healingCardsRound = 0;
    public static int statusCardsRound = 0;
    public static int drawsCardsRound = 0;

    //Values played this round
    public static int damageDealtRound = 0;
    public static int armorDealtRound = 0;
    public static int healingDealtRound = 0;
    public static int statusDealtRound = 0;
    public static int drawsDealtRound = 0;

    //Values all combat
    public static int damageDealt = 0;
    public static int armorGotten = 0;
    public static int healingDone = 0;
    public static int statusInflicted = 0;
    public static int cardsDrawn = 0;

    public static int GetAttackCardsRound() { return attackCardsRound; }
    public static int GetDefendCardsRound() { return defendCardsRound; }
    public static int GetHealingCardsRound() { return healingCardsRound; }
    public static int GetStatusCardsRound() { return statusCardsRound; }
    public static int GetDrawCardsRound() { return drawsCardsRound; }
    public static int GetDamageDealtRound() { return damageDealtRound; }
    public static int GetArmorDealtRound() { return armorDealtRound; }
    public static int GetHealingDealtRound() { return healingDealtRound; }
    public static int GetStatusDealtRound() { return statusDealtRound; }
    public static int GetDrawsDealtRound() { return drawsDealtRound; }
    public static int GetDamageDealt() { return damageDealt; }
    public static int GetArmorGotten() { return armorGotten; }
    public static int GetHealingDone() { return healingDone; }
    public static int GetStatusInflicted() { return statusInflicted; }
    public static int GetCardsDrawn() { return cardsDrawn; }


        
    TextAsset jsonFile;
    string path = "Jsons/CombatValues";

    public void Init(EnemyFunctions combatEnemy)
    {
        jsonFile = Resources.Load(path) as TextAsset;
        GameManager.combatManager = JsonUtility.FromJson<CombatManager>(jsonFile.text);

        hand = GameObject.Find("Canvas/Hand").GetComponent<HorizontalLayoutGroup>();
        enemy = combatEnemy;
        
        //Player HUD
        playerSlider = GameObject.Find("Canvas/PlayerHUD/PlayerSlider").GetComponent<Slider>();
        playerHealth = GameObject.Find("Canvas/PlayerHUD/PlayerHealth").GetComponent<Text>();
        playerName = GameObject.Find("Canvas/PlayerHUD/PlayerName").GetComponent<Text>();
        playerArmor = GameObject.Find("Canvas/PlayerHUD/PlayerArmor").GetComponent<Text>();

        //Enemy HUD
        enemySlider = GameObject.Find("Canvas/EnemyHUD/EnemySlider").GetComponent<Slider>();
        enemyHealth = GameObject.Find("Canvas/EnemyHUD/EnemyHealth").GetComponent<Text>();
        enemyArmor = GameObject.Find("Canvas/EnemyHUD/EnemyArmor").GetComponent<Text>();
        enemyName = GameObject.Find("Canvas/EnemyHUD/EnemyName").GetComponent<Text>();
        intentionText = GameObject.Find("Canvas/EnemyHUD/EnemyIntention").GetComponent<Text>();

        //Combat HUD
        timeSlider = GameObject.Find("Canvas/CombatHUD/TimeSlider").GetComponent<Slider>();
        comboSlider = GameObject.Find("Canvas/CombatHUD/ComboSlider").GetComponent<Slider>();
        comboText = GameObject.Find("Canvas/CombatHUD/ComboText").GetComponent<Text>();
        manaText = GameObject.Find("Canvas/CombatHUD/ManaText").GetComponent<Text>();
        endTurnButton = GameObject.Find("Canvas/CombatHUD/EndTurnButton").GetComponent<Button>();
        endTurnInfo = GameObject.Find("Canvas/CombatHUD/EndTurnInfo").GetComponent<Text>();

        //Level HUD
        levelUpHUD = GameObject.Find("Canvas/LevelUpHUD");
        levelUpHUD.SetActive(false);

        StartPlayerTurn();
    }

    public void EndPlayerTurn()
    {
        int it = GameManager.player.GetPlayer().GetHand().Count;
        for (int i = 0; i < it; i++)
        {
            GameManager.deck.HandCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[0]);
            GameManager.deck.DestroyCard(GameManager.deck.canvas.transform.GetChild(i).gameObject);
            GameManager.deck.cardsGO.RemoveAt(0);
        }
        hand.spacing = -1150;
        playerTurn = false;
        currentTurnSeconds = 0f;
        currentComboSeconds = 0f;
        ResetCombo();
    }

    void StartPlayerTurn()
    {
        playerTurn = true;
        GameManager.deck.StartCoroutine(Deck.DrawStartingHand(GameManager.player.GetPlayer()));
        GameManager.combatManager.currentTurnSeconds = GameManager.combatManager.turnSeconds;
        GameManager.combatManager.currentComboSeconds = GameManager.combatManager.comboSeconds;
        GameManager.player.RefillMana();
        GameManager.combatFunctions.ShowCombo();
        hand.enabled = true;
        enemy.DoOption();
        //Values reset
        ResetCombatRoundValues();
    }

    public void BuildCombo()
    {
        comboBuilder++;
        combo += (int)comboMultiplier * comboBuilder;
    }

    void ResetCombo()
    {
        combo = 0;
        comboBuilder = 0;
    }

    void ResetCombatRoundValues()
    {
        attackCardsRound = 0;
        defendCardsRound = 0;
        healingCardsRound = 0;
        statusCardsRound = 0;
        drawsCardsRound = 0;
        damageDealtRound = 0;
        armorDealtRound = 0;
        healingDealtRound = 0;
        statusDealtRound = 0;
        drawsDealtRound = 0;
    }

    public void CombatInputs()
    {   
        playerSlider.maxValue = GameManager.player.GetPlayer().GetCurrentMaxHealth();
        playerSlider.value = GameManager.player.GetPlayer().GetCurrentHealth();
        enemySlider.maxValue = enemy.GetEnemy().GetStartingMaxHealth();
        enemySlider.value = enemy.GetEnemy().GetCurrentHealth();

        playerHealth.text = GameManager.player.GetPlayer().GetCurrentHealth() + " / " + GameManager.player.GetPlayer().GetCurrentMaxHealth();
        enemyHealth.text = enemy.GetEnemy().GetCurrentHealth() + " / " + enemy.GetEnemy().GetStartingMaxHealth();
        playerArmor.text = GameManager.player.GetPlayer().GetCurrentArmor().ToString();
        enemyArmor.text = enemy.GetCurrentArmor().ToString();
        playerName.text = GameManager.player.GetPlayer().GetName();
        enemyName.text = enemy.GetEnemy().GetName();
        enemyArmor.text = enemy.GetEnemy().GetCurrentArmor().ToString();
        comboText.text = "COMBO: " + combo.ToString();
        manaText.text = "Volts: " + GameManager.player.GetPlayer().GetCurrentMana().ToString() + " / " + GameManager.player.GetPlayer().GetCurrentMaxMana().ToString();

        GameManager.deck.seePlayerHand = GameManager.player.GetPlayer().GetHand();
        GameManager.deck.seeDrawDeck = Deck.drawDeck;
        GameManager.deck.seePileDeck = Deck.pileDeck;
        GameManager.deck.seePassive = Deck.passives;

        timeSlider.maxValue = turnSeconds;
        currentTurnSeconds -= Time.deltaTime;
        timeSlider.value = currentTurnSeconds;

        comboSlider.maxValue = comboSeconds;
        currentComboSeconds -= Time.deltaTime;
        comboSlider.value = currentComboSeconds;

        if (currentTurnSeconds <= 0f)
            EndPlayerTurn();

        if (currentComboSeconds <= 0f)
        {
            ResetCombo();
            GameManager.deck.UpdateCardDescription();
            GameManager.combatFunctions.HideCombo();
        }

        if (Input.GetKeyDown(KeyCode.E) && playerTurn)
        {
            EndPlayerTurn();
        }

        if (!playerTurn)
        {
            enemy.ExecuteOption();
            StartPlayerTurn();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.player.IncreaseCurrentMaxHealth();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.player.IncreaseCurrentMaxMana();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.player.IncreaseCurrentHandSize();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.player.IncreaseComboMultiplier();
        }

        //if (playerTurn)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1) && GameManager.player.GetPlayer().GetHand().Count >= 1)
        //    {
        //        GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[0]);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha2) && GameManager.player.GetPlayer().GetHand().Count >= 2)
        //    {
        //        GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[1]);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha3) && GameManager.player.GetPlayer().GetHand().Count >= 3)
        //    {
        //        GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[2]);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha4) && GameManager.player.GetPlayer().GetHand().Count >= 4)
        //    {
        //        GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[3]);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha5) && GameManager.player.GetPlayer().GetHand().Count >= 5)
        //    {
        //        GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[4]);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha6) && GameManager.player.GetPlayer().GetHand().Count >= 6)
        //    {
        //        GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[5]);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha7) && GameManager.player.GetPlayer().GetHand().Count >= 7)
        //    {
        //        GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[6]);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha8) && GameManager.player.GetPlayer().GetHand().Count >= 8)
        //    {
        //        GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[7]);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha9) && GameManager.player.GetPlayer().GetHand().Count >= 9)
        //    {
        //        GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[8]);
        //    }
        //}



    }
}
