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

}
