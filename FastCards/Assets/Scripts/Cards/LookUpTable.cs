using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LookUpTable
{
    public delegate int myDelegate();

    public enum DelegateType
    {
        playerLife,
        playerDamage,
        playerCards,
    }
    
    public static Dictionary<DelegateType, myDelegate> lookUpTable = new Dictionary<DelegateType, myDelegate>();

    public static void LoadTable()
    {
        lookUpTable.Add(DelegateType.playerLife, GameManager.player.GetPlayer().GetCurrentHealth);
    }
    // Start is called before the first frame update

    
}
