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

    public System.Action<bool, int> behaviorUse;

    public void Init()
    {
        switch (behaviourType)
        {
            case BehaviourType.Attack:
                break;
            case BehaviourType.Defense:
                break;
            case BehaviourType.Heal:
                break;
            case BehaviourType.TakeHp:
                break;
            case BehaviourType.BuffAttack:
                break;
            case BehaviourType.BuffDefense:
                break;
            case BehaviourType.BuffHealing:
                break;
            case BehaviourType.Draw:
                break;
        }
    }
    void Execute()
    {
        condition.CheckCondition();
        behaviorUse.Invoke(targetPlayer, value);
    }

    public static void AttackCard(bool self, int value)
    {
        int fullDamage = value + GameManager.player.GetPlayer().GetStatusDamage() + GameManager.combatManager.combo;
        if (self)
            GameManager.player.TakeDamage(value);
        else if (!self)
            CombatManager.enemy.TakeDamage(fullDamage);
    }

    public static void TakeHPCard(bool self, int value)
    {
        if (self)
            GameManager.player.LoseHP(value);
        else if (!self)
            CombatManager.enemy.LoseHP(value);

    }

    public static void DefenseCard(bool self, int value)
    {
        if (self)
            GameManager.player.AddArmor(value + GameManager.combatManager.combo);
        else if (!self)
            CombatManager.enemy.AddArmor(value);

    }

    public static void HealCard(bool self, int value)
    {
        if (self)
            GameManager.player.Heal(value + GameManager.combatManager.combo);
        else if (!self)
            CombatManager.enemy.Heal(value);
    }

    public static void StatusAttack(bool self, int value)
    {
        if (self)
            GameManager.player.ApplyStatus(value, 0, 0);
        else if (!self)
            CombatManager.enemy.ApplyStatus(value, 0, 0);
    }

    public static void StatusDefense(bool self, int value)
    {
        if (self)
            GameManager.player.ApplyStatus(0, value, 0);
        else if (!self)
            CombatManager.enemy.ApplyStatus(0, value, 0);
    }

    public static void StatusHeal(bool self, int value)
    {
        if (self)
            GameManager.player.ApplyStatus(0, 0, value);
        else if (!self)
            CombatManager.enemy.ApplyStatus(0, 0, value);
    }

    public static void DrawCard(bool self, int value)
    {
        for (int i = 0; i < value; i++)
        {
            Deck.DrawCard(GameManager.player.GetPlayer());
        }
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


