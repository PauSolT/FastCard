using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CombatFunctions : MonoBehaviour
{
    //HUD
    public void ShowEndTurnInfo()
    {
        CombatManager.endTurnInfo.gameObject.SetActive(true);
    }

    public void HideEndTurnInfo()
    {
        CombatManager.endTurnInfo.gameObject.SetActive(false);
    }

    public void ShowCombo()
    {
        CombatManager.comboSlider.gameObject.SetActive(true);
        CombatManager.comboText.gameObject.SetActive(true);
    }

    public void HideCombo()
    {
        CombatManager.comboSlider.gameObject.SetActive(false);
        CombatManager.comboText.gameObject.SetActive(false);
    }

    void HideLevelUp()
    {
        CombatManager.levelUpHUD.SetActive(false);
    }

    //End player turn
    public void EndTurn()
    {
        GameManager.combatManager.EndPlayerTurn();
    }

    //Rewards when player levels up
    public void IncreaseMaxHealthButton()
    {
        GameManager.player.IncreaseCurrentMaxHealth();
        HideLevelUp();
        GameManager.rewardSystem.GetRewards();
    }

    public void IncreaseMaxManaButton()
    {
        GameManager.player.IncreaseCurrentMaxMana();
        HideLevelUp();
        GameManager.rewardSystem.GetRewards();
    }

    public void IncreaseMaxHandSizeButton()
    {
        GameManager.player.IncreaseCurrentHandSize();
        HideLevelUp();
        GameManager.rewardSystem.GetRewards();
    }

    public void IncreaseComboMultiplierButton()
    {
        GameManager.player.IncreaseComboMultiplier();
        HideLevelUp();
        GameManager.rewardSystem.GetRewards();
        
    }

    //Add selected reward cards to player's deck
    public void AddRewards()
    {
        GameManager.rewardSystem.AddRewardCardsToPlayer();
    }

    //public void SaveGame()
    //{
    //    string path = Application.dataPath + "/SaveFile.json";
    //    string startingText = "{\n \"player\": {\n ";

    //    if (!File.Exists(path))
    //    {
    //        File.WriteAllText(path, startingText);
    //    } else
    //    {
    //        File.Delete(path);
    //        File.WriteAllText(path, startingText);
    //    }
    //    string playerText = "\"currentMaxHealth\": " + GameManager.player.GetPlayer().GetCurrentMaxHealth() + ",\n" +
    //                        "\"currentHealth\": " + GameManager.player.GetPlayer().GetCurrentHealth() + ",\n" +
    //                         "\"currentMaxMana\": " + GameManager.player.GetPlayer().GetCurrentMaxMana() + ",\n" +
    //                         "\"currentMaxHandSize\": " + GameManager.player.GetPlayer().GetCurrentMaxHandSize() + "\n},\n";



    //    string playerDeck = "\"cards\": [\n";
    //    int i = 0;

    //    foreach (Card card in GameManager.deck.playerDeck)
    //    {
    //        playerDeck += JsonUtility.ToJson(card, true);

    //        if (i != GameManager.deck.playerDeck.Count - 1)
    //        {
    //            playerDeck += ",\n";
    //        }
    //        i++;
    //    }

    //    playerDeck += "\n]";


    //    File.AppendAllText(path, playerText);
    //    File.AppendAllText(path, playerDeck);

    //    File.AppendAllText(path, "\n}");
       
    //}

    //private void Start()
    //{
    //    SaveGame();
    //}

}
