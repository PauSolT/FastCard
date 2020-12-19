using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class LookUpTable
{
    public delegate int myDelegate();

    [System.Serializable] public enum DelegateType
    {
        playerHealth,
        playerMaxHealth,
        enemyHealth,
        enemyMaxHealth,
    }

    
    [SerializeField] public static Dictionary<DelegateType, myDelegate> lookUpTable = new Dictionary<DelegateType, myDelegate>();

    public static void LoadTable()
    {
        lookUpTable.Add(DelegateType.playerHealth, GameManager.player.GetPlayer().GetCurrentHealth);
        lookUpTable.Add(DelegateType.playerMaxHealth, GameManager.player.GetPlayer().GetCurrentMaxHealth);
        lookUpTable.Add(DelegateType.enemyHealth, CombatManager.enemy.GetEnemy().GetCurrentHealth);
        lookUpTable.Add(DelegateType.enemyHealth, CombatManager.enemy.GetEnemy().GetStartingMaxHealth);
    }
    // Start is called before the first frame update

    
}
