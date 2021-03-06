﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager 
{

    public static EnemyFunctions enemy;
    public bool playerTurn;
    public int combo = 0;
    public int comboBuilder = 0;
    public float comboMultiplier = 0; 
    public float comboSeconds = 5f;
    public float currentComboSeconds = 0f;
    [SerializeField] float turnSeconds = 10f;
    float currentTurnSeconds = 0f;

    public static HorizontalLayoutGroup hand;

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
    public static Text currentTurnInfo;

    public static Text playerAttackText;
    public static Text playerArmorText;
    public static Text playerRecoveryText;
    public static Text enemyAttackText;
    public static Text enemyArmorText;
    public static Text enemyRecoveryText;

    //Images
    public static Image playerSliderImage;
    public static Image enemySliderImage;

    //Buttons
    public static Button endTurnButton;
    public static Button deckButton;
    public static Button hideDeckButton;
    public static Button pileButton;
    public static Button hidePileButton;
    public static Button addCards;

    //LevelUp
    public static GameObject levelUpHUD;

    //GameObjects
    public static GameObject enemyHUD;
    public static GameObject playerHUD;
    public static GameObject combatHUD;

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

    //Turn
    public static int currentTurn;

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
    public static int GetCurrentTurn() { return currentTurn; }

    public List<AllPasives.PassiveName> passivesInPlay = new List<AllPasives.PassiveName>();
        
    TextAsset jsonFile;
    string path = "Jsons/CombatValues";

    public void InitCombat()
    {

        //Player HUD
        playerHUD = GameObject.Find("Canvas/PlayerHUD");
        playerSlider = GameObject.Find("Canvas/PlayerHUD/PlayerSlider").GetComponent<Slider>();
        playerHealth = GameObject.Find("Canvas/PlayerHUD/PlayerHealth").GetComponent<Text>();
        playerName = GameObject.Find("Canvas/PlayerHUD/PlayerName").GetComponent<Text>();
        playerArmor = GameObject.Find("Canvas/PlayerHUD/PlayerDefense").GetComponent<Text>();
        playerAttackText = GameObject.Find("Canvas/PlayerHUD/PlayerAttack").GetComponent<Text>();
        playerArmorText = GameObject.Find("Canvas/PlayerHUD/PlayerArmor").GetComponent<Text>();
        playerRecoveryText = GameObject.Find("Canvas/PlayerHUD/PlayerRecovery").GetComponent<Text>();
        playerSliderImage = GameObject.Find("Canvas/PlayerHUD/PlayerSlider/FillArea/Fill").GetComponent<Image>();

        //Enemy HUD
        enemyHUD = GameObject.Find("Canvas/EnemyHUD");
        enemySlider = GameObject.Find("Canvas/EnemyHUD/EnemySlider").GetComponent<Slider>();
        enemyHealth = GameObject.Find("Canvas/EnemyHUD/EnemyHealth").GetComponent<Text>();
        enemyArmor = GameObject.Find("Canvas/EnemyHUD/EnemyDefense").GetComponent<Text>();
        enemyName = GameObject.Find("Canvas/EnemyHUD/EnemyName").GetComponent<Text>();
        intentionText = GameObject.Find("Canvas/EnemyHUD/EnemyIntention").GetComponent<Text>();
        enemyAttackText = GameObject.Find("Canvas/EnemyHUD/EnemyAttack").GetComponent<Text>();
        enemyArmorText = GameObject.Find("Canvas/EnemyHUD/EnemyArmor").GetComponent<Text>();
        enemyRecoveryText = GameObject.Find("Canvas/EnemyHUD/EnemyRecovery").GetComponent<Text>();
        enemySliderImage = GameObject.Find("Canvas/EnemyHUD/EnemySlider/FillArea/Fill").GetComponent<Image>();


        //Combat HUD
        combatHUD = GameObject.Find("Canvas/CombatHUD");
        timeSlider = GameObject.Find("Canvas/CombatHUD/TimeSlider").GetComponent<Slider>();
        comboSlider = GameObject.Find("Canvas/CombatHUD/ComboSlider").GetComponent<Slider>();
        comboText = GameObject.Find("Canvas/CombatHUD/ComboText").GetComponent<Text>();
        currentTurnInfo = GameObject.Find("Canvas/CombatHUD/TurnText").GetComponent<Text>();
        manaText = GameObject.Find("Canvas/CombatHUD/ManaText").GetComponent<Text>();
        endTurnButton = GameObject.Find("Canvas/CombatHUD/EndTurnButton").GetComponent<Button>();
        endTurnInfo = GameObject.Find("Canvas/CombatHUD/EndTurnInfo").GetComponent<Text>();
        deckButton = GameObject.Find("Canvas/CombatHUD/DeckButton").GetComponent<Button>();
        hideDeckButton = GameObject.Find("Canvas/CombatHUD/HideDeckButton").GetComponent<Button>();
        pileButton = GameObject.Find("Canvas/CombatHUD/PileDeck").GetComponent<Button>();
        hidePileButton = GameObject.Find("Canvas/CombatHUD/HidePileDeck").GetComponent<Button>();

        //Level HUD
        levelUpHUD = GameObject.Find("Canvas/LevelUpHUD");
        levelUpHUD.SetActive(false);

        //Get hand HUD
        hand = GameObject.Find("Canvas/Hand").GetComponent<HorizontalLayoutGroup>();

        //Initialize variables checked in middle of combat
        LookUpTable.LoadTable();

        //Initializes cards
        GameManager.InitCards();
        GameManager.rewardCards.Init();
        //Initializes deck
        GameManager.deck.Init();
        //Prepares deck for combat
        GameManager.deck.StartCombat();

    }

    public void InitEnemy(EnemyFunctions combatEnemy)
    {
        //Loads enemy file
        jsonFile = Resources.Load(path) as TextAsset;
        GameManager.combatManager = JsonUtility.FromJson<CombatManager>(jsonFile.text);
        //make enemy selected enemy for all scripts
        enemy = combatEnemy;
        //Initialize enemy
        enemy.Init();

        currentTurn = 0;
    }

    public void ResetPlayerCombatVariables()
    {
        GameManager.player.GetPlayer().SetCurrentArmor(0);
        GameManager.player.GetPlayer().SetStatusDamage(0);
        GameManager.player.GetPlayer().SetStatusDefense(0);
        GameManager.player.GetPlayer().SetStatusHeal(0);
    }

    public void SetStartingHUD()
    {
        //Set HUD
        playerHealth.text = GameManager.player.GetPlayer().GetCurrentHealth() + " / " + GameManager.player.GetPlayer().GetCurrentMaxHealth();
        enemyHealth.text = enemy.GetEnemy().GetCurrentHealth() + " / " + enemy.GetEnemy().GetStartingMaxHealth();
        playerArmor.text = GameManager.player.GetPlayer().GetCurrentArmor().ToString();
        enemyArmor.text = enemy.GetEnemy().GetCurrentArmor().ToString();
        playerSlider.maxValue = GameManager.player.GetPlayer().GetCurrentMaxHealth();
        playerSlider.value = GameManager.player.GetPlayer().GetCurrentHealth();
        enemySlider.maxValue = enemy.GetEnemy().GetStartingMaxHealth();
        enemySlider.value = enemy.GetEnemy().GetCurrentHealth();
        playerName.text = GameManager.player.GetPlayer().GetName();
        enemyName.text = enemy.GetEnemy().GetName();
        playerAttackText.text = GameManager.player.GetPlayer().GetStatusDamage().ToString();
        playerArmorText.text = GameManager.player.GetPlayer().GetStatusDefense().ToString();
        playerRecoveryText.text = GameManager.player.GetPlayer().GetStatusHeal().ToString();
        enemyAttackText.text = enemy.GetEnemy().GetStatusDamage().ToString();
        enemyArmorText.text = enemy.GetEnemy().GetStatusDefense().ToString();
        enemyRecoveryText.text = enemy.GetEnemy().GetStatusHeal().ToString();

        //Hide unnecesary HUD
        ModsHUD();
        DefenseHUD();

        comboText.text = "COMBO: " + combo.ToString();

        StartPlayerTurn();
    }

    public void EndPlayerTurn()
    {
        EmptyHand();
        //Resize hand space
        hand.spacing = -1150;

        //Sets times to 0
        currentTurnSeconds = 0f;
        currentComboSeconds = 0f;
        //Sets combo to 0
        ResetCombo();
        playerTurn = false;

        enemy.ExecuteOption();
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        playerTurn = true;
        currentTurn++;
        currentTurnInfo.text = "Turn: " + currentTurn;
        //Draws cards to hand
        GameManager.deck.StartCoroutine(Deck.DrawStartingHand(GameManager.player.GetPlayer()));
        //Sets times
        GameManager.combatManager.currentTurnSeconds = GameManager.combatManager.turnSeconds;
        GameManager.combatManager.currentComboSeconds = GameManager.combatManager.comboSeconds;
        //Refills mana
        GameManager.player.RefillMana();
        //Shows combo HUD
        GameManager.combatFunctions.ShowCombo();
        //enables hand so cards can reposicion
        hand.enabled = true;
        //Enemy tells option
        enemy.DoOption();
        //Values reset
        ResetCombatRoundValues();

        OnStartTurn();
    }

    public void BuildCombo()
    {
        combo += (2 + comboBuilder); 
        currentComboSeconds += 0.5f;
        comboText.text = "COMBO: " + combo.ToString();
    }

    public void ResetCombo()
    {
        combo = 0;
        comboBuilder = 0;
        comboText.text = "COMBO: " + combo.ToString();
    }

    private void ResetCombatRoundValues()
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

    public static void ModsHUD()
    {
        if (GameManager.player.GetPlayer().GetStatusDamage() == 0)
            playerAttackText.gameObject.SetActive(false);
        else if (GameManager.player.GetPlayer().GetStatusDamage() != 0)
            playerAttackText.gameObject.SetActive(true);
        if (GameManager.player.GetPlayer().GetStatusDefense() == 0)
            playerArmorText.gameObject.SetActive(false);
        else if (GameManager.player.GetPlayer().GetStatusDefense() != 0)
            playerArmorText.gameObject.SetActive(true);
        if (GameManager.player.GetPlayer().GetStatusHeal() == 0)
            playerRecoveryText.gameObject.SetActive(false);
        else if (GameManager.player.GetPlayer().GetStatusHeal() == 0)
            playerRecoveryText.gameObject.SetActive(true);
        if (enemy.GetEnemy().GetStatusDamage() == 0)
            enemyAttackText.gameObject.SetActive(false);
        else if (enemy.GetEnemy().GetStatusDamage() != 0)
            enemyAttackText.gameObject.SetActive(true);
        if (enemy.GetEnemy().GetStatusDefense() == 0)
            enemyArmorText.gameObject.SetActive(false);
        else if (enemy.GetEnemy().GetStatusDefense() != 0)
            enemyArmorText.gameObject.SetActive(true);
        if (enemy.GetEnemy().GetStatusHeal() == 0)
            enemyRecoveryText.gameObject.SetActive(false);
        else if (enemy.GetEnemy().GetStatusHeal() != 0)
            enemyRecoveryText.gameObject.SetActive(true);
    }

    public static void DefenseHUD()
    {
        AddDefenseHUD();
        TakeDefenseHUD();
    }

    public static void AddDefenseHUD()
    {
        if (GameManager.player.GetPlayer().GetCurrentArmor() != 0)
        {
            playerArmor.gameObject.SetActive(true);
            playerSliderImage.color = new Color(0f, 0.572549f, 1f, 1f);
        }
        if (enemy.GetEnemy().GetCurrentArmor() != 0)
        {
            enemyArmor.gameObject.SetActive(true);
            enemySliderImage.color = new Color(0f, 0.572549f, 1f, 1f);
        }
    }

    public static void TakeDefenseHUD()
    {
        if (GameManager.player.GetPlayer().GetCurrentArmor() == 0)
        {
            playerArmor.gameObject.SetActive(false);
            playerSliderImage.color = new Color(0f, 0.7921569f, 0.01176471f, 1f);
        }
        if (enemy.GetEnemy().GetCurrentArmor() == 0)
        {
            enemyArmor.gameObject.SetActive(false);
            enemySliderImage.color = new Color(0f, 0.7921569f, 0.01176471f, 1f);
        }
    }

    public void EmptyHand()
    {
        //Gets the number of cards
        int it = GameManager.player.GetPlayer().GetHand().Count;
        //Removes all cards from hand
        for (int i = 0; i < it; i++)
        {
            GameManager.deck.HandCardToPile(GameManager.player.GetPlayer(), GameManager.player.GetPlayer().GetHand()[0]);
            GameManager.deck.DestroyCard(GameManager.deck.hand.transform.GetChild(i).gameObject);
            GameManager.deck.cardsGO.RemoveAt(0);
        }

        //Reset hand spacing -100 (original hand spacing -1150)
        hand.spacing = -1050;
    }

    void OnStartTurn()
    {
        foreach (AllPasives.PassiveName passive in passivesInPlay)
        {
            AllPasives.passives[passive].Invoke();
        }
    }

    public void CombatInputs()
    {   
        //if enemy has no health, stops combat
        if (enemy.GetEnemy().GetCurrentHealth() <= 0)
        {
            return;
        }

        GameManager.deck.seePlayerHand = GameManager.player.GetPlayer().GetHand();
        GameManager.deck.seeDrawDeck = Deck.drawDeck;
        GameManager.deck.seePileDeck = Deck.pileDeck;

        timeSlider.maxValue = turnSeconds;
        currentTurnSeconds -= Time.deltaTime;
        timeSlider.value = currentTurnSeconds;

        comboSlider.maxValue = comboSeconds;
        currentComboSeconds -= Time.deltaTime;
        comboSlider.value = currentComboSeconds;

        if (currentTurnSeconds <= 0f && playerTurn)
            EndPlayerTurn();

        if (currentComboSeconds <= 0f)
        {
            ResetCombo();
            GameManager.deck.UpdateCardDescription(GameManager.player.GetPlayer().GetHand(), GameManager.deck.cardsGO);
            GameManager.combatFunctions.HideCombo();
        }

        if (Input.GetKeyDown(KeyCode.E) && playerTurn)
        {
            EndPlayerTurn();
        }

        //if (!playerTurn)
        //{
        //    enemy.ExecuteOption();
        //    StartPlayerTurn();
        //}

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
    }
}
