using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFunctions : MonoBehaviour
{
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

    public void EndTurn()
    {
        GameManager.combatManager.EndPlayerTurn();
    }


    public void IncreaseMaxHealthButton()
    {
        GameManager.player.IncreaseCurrentMaxHealth();
        HideLevelUp();
    }

    public void IncreaseMaxManaButton()
    {
        GameManager.player.IncreaseCurrentMaxMana();
        HideLevelUp();
    }

    public void IncreaseMaxHandSizeButton()
    {
        GameManager.player.IncreaseCurrentHandSize();
        HideLevelUp();
    }

    public void IncreaseComboMultiplierButton()
    {
        GameManager.player.IncreaseComboMultiplier();
        HideLevelUp();
    }

    void HideLevelUp()
    {
        CombatManager.levelUpHUD.SetActive(false);
    }

}
