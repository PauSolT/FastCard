using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class LookUpTable
{
    public delegate int myDelegate();
    [System.Serializable] public enum DelegateType
    {
        //Zero
        none,

        //Player stats related
        playerHealth, playerMaxHealth, playerCurrentArmor, player12Health, player34Health, player13Health, player14Health,
        //0             1               2                   3               4               5               6

        //Enemy stats related
        enemyHealth, enemyMaxHealth, enemyCurrentArmor, enemy12Health, enemy34Health, enemy13Health, enemy14Health,
        //7           8               9                  10             11              12              13

        //Cards played this round
        attackCardsRound, defenseCardsRound, healingCardsRound, statusCardsRound, drawsCardsRound,
        //14                15                 16               17                  18

        //Combat stats this round
        damageRound, armorRound, healRound, statusRound, drawRound,
        //14            15       16         17              18

        //Combat stats this combat
        damageCombat, armorCombat, healingCombat, statusCombat, drawCombat,
        //19            20          21              22          23

        //Turn
        currentTurn,

        //Enemy Actions Related
        enemyHealingCombat
    }
    public static int GetZero() { return 0; }

    [SerializeField] public static Dictionary<DelegateType, myDelegate> lookUpTable = new Dictionary<DelegateType, myDelegate>();

    public static void LoadTable()
    {
        lookUpTable.Add(DelegateType.none, GetZero);
        lookUpTable.Add(DelegateType.playerHealth, GameManager.player.GetPlayer().GetCurrentHealth);
        lookUpTable.Add(DelegateType.enemyHealth, CombatManager.enemy.GetEnemy().GetCurrentHealth);
        lookUpTable.Add(DelegateType.playerMaxHealth, GameManager.player.GetPlayer().GetCurrentMaxHealth);
        lookUpTable.Add(DelegateType.enemyMaxHealth, CombatManager.enemy.GetEnemy().GetStartingMaxHealth);
        lookUpTable.Add(DelegateType.playerCurrentArmor, GameManager.player.GetPlayer().GetCurrentArmor);
        lookUpTable.Add(DelegateType.enemyCurrentArmor, CombatManager.enemy.GetEnemy().GetCurrentArmor);
        lookUpTable.Add(DelegateType.player12Health, GameManager.player.GetPlayer().GetHalfHealth);
        lookUpTable.Add(DelegateType.enemy12Health, CombatManager.enemy.GetEnemy().GetHalfHealth);
        lookUpTable.Add(DelegateType.player34Health, GameManager.player.GetPlayer().GetThreeQuartersHealth);
        lookUpTable.Add(DelegateType.enemy34Health, CombatManager.enemy.GetEnemy().GetThreeQuartersHealth);
        lookUpTable.Add(DelegateType.player13Health, GameManager.player.GetPlayer().GetOneThirdHealth);
        lookUpTable.Add(DelegateType.enemy13Health, CombatManager.enemy.GetEnemy().GetOneThirdHealth);
        lookUpTable.Add(DelegateType.player14Health, GameManager.player.GetPlayer().GetOneFourthHealth);
        lookUpTable.Add(DelegateType.enemy14Health, CombatManager.enemy.GetEnemy().GetOneFourthHealth);
        lookUpTable.Add(DelegateType.attackCardsRound, CombatManager.GetAttackCardsRound);
        lookUpTable.Add(DelegateType.defenseCardsRound, CombatManager.GetDefendCardsRound);
        lookUpTable.Add(DelegateType.healingCardsRound, CombatManager.GetHealingCardsRound);
        lookUpTable.Add(DelegateType.statusCardsRound, CombatManager.GetStatusCardsRound);
        lookUpTable.Add(DelegateType.drawsCardsRound, CombatManager.GetDrawCardsRound);
        lookUpTable.Add(DelegateType.damageRound, CombatManager.GetDamageDealtRound);
        lookUpTable.Add(DelegateType.armorRound, CombatManager.GetArmorDealtRound);
        lookUpTable.Add(DelegateType.healRound, CombatManager.GetHealingDealtRound);
        lookUpTable.Add(DelegateType.statusRound, CombatManager.GetStatusDealtRound);
        lookUpTable.Add(DelegateType.drawRound, CombatManager.GetDrawsDealtRound);
        lookUpTable.Add(DelegateType.damageCombat, CombatManager.GetDamageDealt);
        lookUpTable.Add(DelegateType.armorCombat, CombatManager.GetArmorGotten);
        lookUpTable.Add(DelegateType.healingCombat, CombatManager.GetHealingDone);
        lookUpTable.Add(DelegateType.statusCombat, CombatManager.GetStatusInflicted);
        lookUpTable.Add(DelegateType.drawCombat, CombatManager.GetCardsDrawn);
        lookUpTable.Add(DelegateType.currentTurn, CombatManager.GetCurrentTurn);
        lookUpTable.Add(DelegateType.enemyHealingCombat, CombatManager.enemy.GetHealingCombat);
    }
}
