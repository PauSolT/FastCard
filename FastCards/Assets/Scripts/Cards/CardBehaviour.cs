using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    
    public struct Condition
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


        Dictionary<LookUpTable.DelegateType, LookUpTable.DelegateType> conditionPairs;
        List<ConditionTypes> conditionTypes;


        void Init()
        {
            conditionPairs = new Dictionary<LookUpTable.DelegateType, LookUpTable.DelegateType>();
            conditionTypes = new List<ConditionTypes>();
        }

        public bool CheckCondition()
        {
            int index = 0;
            bool result = true;

            foreach(KeyValuePair<LookUpTable.DelegateType, LookUpTable.DelegateType> pair in conditionPairs)
            {
                
                result &= CheckPairs(conditionTypes[index++], LookUpTable.lookUpTable[pair.Key](), LookUpTable.lookUpTable[pair.Value]());
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

    Condition condition;

    void Execute()
    {

    }

    void AttackCard(PlayerFunctions player, int value)
    {
        int fullDamage = value + player.GetPlayer().GetStatusDamage() + GameManager.combatManager.combo;
        if (fullDamage >= 0)
            CombatManager.enemy.TakeDamage(fullDamage);
    }

    void DefenseCard(PlayerFunctions player, int value)
    {
        player.AddArmor(value + GameManager.combatManager.combo);
    }

    void HealCard(PlayerFunctions player, int value)
    {
        player.Heal(value + GameManager.combatManager.combo);
    }

    void StatusCard(PlayerFunctions player, bool target, int valueDamage, int valueDefense, int valueHealing)
    {
        if (target)
            player.ApplyStatus(valueDamage, valueDefense, valueHealing);
        else if (!target)
            CombatManager.enemy.ApplyStatus(valueDamage, valueDefense, valueHealing);
    }

    void DrawCard(PlayerFunctions player, int value)
    {
        for (int i = 0; i < value; i++)
        {
            Deck.DrawCard(player.GetPlayer());
        }
    }
}
