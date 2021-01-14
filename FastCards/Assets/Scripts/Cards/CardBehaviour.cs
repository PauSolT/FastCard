using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class CardBehaviour
{
    [SerializeField] public Condition condition = new Condition();

    public int value = 0;
    public bool targetPlayer = true;
    [SerializeField]public LookUpTable.DelegateType valueDelegate;

    [System.Serializable] public enum BehaviourType
    {
        Attack,
        Defense,
        Heal,
        TakeHp,
        BuffAttack,
        BuffDefense,
        BuffHealing,
        Draw
    }

    [SerializeField] public BehaviourType behaviourType;

    [SerializeField] public System.Action<bool, int, LookUpTable.DelegateType> behaviorUse;

    public void Init()
    {
        switch (behaviourType)
        {
            case BehaviourType.Attack:
                behaviorUse += AttackCard;
                break;
            case BehaviourType.Defense:
                behaviorUse += DefenseCard;
                break;
            case BehaviourType.Heal:
                behaviorUse += HealCard;
                break;
            case BehaviourType.TakeHp:
                behaviorUse += TakeHPCard;
                break;
            case BehaviourType.BuffAttack:
                behaviorUse += StatusAttack;
                break;
            case BehaviourType.BuffDefense:
                behaviorUse += StatusDefense;
                break;
            case BehaviourType.BuffHealing:
                behaviorUse += StatusHeal;
                break;
            case BehaviourType.Draw:
                behaviorUse += DrawCard;
                break;
        }
    }
    public void Execute()
    {
        Init();
        if (condition.CheckCondition())
            behaviorUse.Invoke(targetPlayer, value, valueDelegate);

        switch (behaviourType)
        {
            case BehaviourType.Attack:
                CombatManager.attackCardsRound++;
                break;
            case BehaviourType.Defense:
                CombatManager.defendCardsRound++;
                break;
            case BehaviourType.Heal:
                CombatManager.healingCardsRound++;
                break;
            case BehaviourType.TakeHp:
                break;
            case BehaviourType.BuffAttack:
                CombatManager.statusCardsRound++;
                break;
            case BehaviourType.BuffDefense:
                CombatManager.statusCardsRound++;
                break;
            case BehaviourType.BuffHealing:
                CombatManager.statusCardsRound++;
                break;
            case BehaviourType.Draw:
                CombatManager.cardsDrawn++;
                break;
        }
    }

    public static void AttackCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        int fullDamage = value + LookUpTable.lookUpTable[del]() + GameManager.player.GetPlayer().GetStatusDamage() + GameManager.combatManager.combo;
        if (self)
            GameManager.player.TakeDamage(value + LookUpTable.lookUpTable[del]());
        else if (!self)
            CombatManager.enemy.TakeDamage(fullDamage);

        CombatManager.damageDealtRound += value + LookUpTable.lookUpTable[del]();
        CombatManager.damageDealt += value + LookUpTable.lookUpTable[del]();
    }

    public static void TakeHPCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        if (self)
            GameManager.player.LoseHP(value + LookUpTable.lookUpTable[del]());
        else if (!self)
            CombatManager.enemy.LoseHP(value + LookUpTable.lookUpTable[del]());

    }

    public static void DefenseCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        if (self)
            GameManager.player.AddArmor(value + LookUpTable.lookUpTable[del]() + GameManager.combatManager.combo);
        else if (!self)
            CombatManager.enemy.AddArmor(value + LookUpTable.lookUpTable[del]());

        CombatManager.armorDealtRound += value + LookUpTable.lookUpTable[del]();
        CombatManager.armorGotten += value + LookUpTable.lookUpTable[del]();
    }

    public static void HealCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        if (self)
            GameManager.player.Heal(value + +LookUpTable.lookUpTable[del]() + GameManager.combatManager.combo);
        else if (!self)
            CombatManager.enemy.Heal(value + LookUpTable.lookUpTable[del]());


        CombatManager.healingDealtRound += value + LookUpTable.lookUpTable[del]();
        CombatManager.healingDone += value + LookUpTable.lookUpTable[del]();
    }

    public static void StatusAttack(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        if (self)
            GameManager.player.ApplyStatus(value +LookUpTable.lookUpTable[del](), 0, 0);
        else if (!self)
            CombatManager.enemy.ApplyStatus(value + LookUpTable.lookUpTable[del](), 0, 0);

        CombatManager.statusDealtRound += value + LookUpTable.lookUpTable[del]();
        CombatManager.statusInflicted += value + LookUpTable.lookUpTable[del]();
    }

    public static void StatusDefense(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        if (self)
            GameManager.player.ApplyStatus(0, value + LookUpTable.lookUpTable[del](), 0);
        else if (!self)
            CombatManager.enemy.ApplyStatus(0, value + LookUpTable.lookUpTable[del](), 0);

        CombatManager.statusDealtRound += value + LookUpTable.lookUpTable[del]();
        CombatManager.statusInflicted += value + LookUpTable.lookUpTable[del]();
    }

    public static void StatusHeal(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        if (self)
            GameManager.player.ApplyStatus(0, 0, value + LookUpTable.lookUpTable[del]());
        else if (!self)
            CombatManager.enemy.ApplyStatus(0, 0, value + LookUpTable.lookUpTable[del]());

        CombatManager.statusDealtRound += value + LookUpTable.lookUpTable[del]();
        CombatManager.statusInflicted += value + LookUpTable.lookUpTable[del]();
    }

    public static void DrawCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        for (int i = 0; i < value; i++)
        {
            Deck.DrawCard(GameManager.player.GetPlayer());
        }

        CombatManager.drawsDealtRound += value + LookUpTable.lookUpTable[del]();
        CombatManager.cardsDrawn += value + LookUpTable.lookUpTable[del]();
    }
}

[System.Serializable]
public class Condition
{
    public enum ConditionTypes
    {
        equal,
        lessThan,
        greaterThan,
        lessOrEqual,
        greaterOrEqual,
        different
    }

    [System.Serializable] public struct DeleagtePair
    {
        [SerializeField] public LookUpTable.DelegateType delegate1;
        [SerializeField] public LookUpTable.DelegateType delegate2;
    }

    [SerializeField] public DeleagtePair[] deleagtePairs;

    //[SerializeField] public Dictionary<LookUpTable.DelegateType, LookUpTable.DelegateType> conditionPairs;
    [SerializeField] public ConditionTypes[] conditionTypes;


    void Init()
    {
        
    }

    public bool CheckCondition()
    {
        int index = 0;
        bool result = true;

        foreach (var pair in deleagtePairs)
        {
            result &= CheckPairs(conditionTypes[index++], LookUpTable.lookUpTable[pair.delegate1](), LookUpTable.lookUpTable[pair.delegate2]());
        }
        return result;
    }

    public bool CheckPairs(ConditionTypes type, int a, int b)
    {
        switch (type)
        {
            case ConditionTypes.equal:
                return a == b;
            case ConditionTypes.lessThan:
                return a < b;
            case ConditionTypes.greaterThan:
                return a > b;
            case ConditionTypes.lessOrEqual:
                return a <= b;
            case ConditionTypes.greaterOrEqual:
                return a >= b;
            case ConditionTypes.different:
                return a != b;

        }
        return false;
    }
}


