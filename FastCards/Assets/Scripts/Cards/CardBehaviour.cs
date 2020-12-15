using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardBehaviour : MonoBehaviour
{
    Condition condition = new Condition();

    public int value = 0;
    public bool behaviourTarget = true;

    public System.Action<bool, int> behaviorUse;
    void Execute()
    {
        condition.CheckCondition();
        behaviorUse.Invoke(behaviourTarget, value);
    }

    public void AttackCard(bool self, int value)
    {
        int fullDamage = value + GameManager.player.GetPlayer().GetStatusDamage() + GameManager.combatManager.combo;
        if (self)
            GameManager.player.TakeDamage(value);
        else if (!self)
            CombatManager.enemy.TakeDamage(fullDamage);
    }

    public void TakeHPCard(bool self, int value)
    {
        if (self)
            GameManager.player.LoseHP(value);
        else if (!self)
            CombatManager.enemy.LoseHP(value);

    }

    public void DefenseCard(bool self, int value)
    {
        if (self)
            GameManager.player.AddArmor(value + GameManager.combatManager.combo);
        else if (!self)
            CombatManager.enemy.AddArmor(value);

    }

    public void HealCard(bool self, int value)
    {
        if (self)
            GameManager.player.Heal(value + GameManager.combatManager.combo);
        else if (!self)
            CombatManager.enemy.Heal(value);
    }

    public void StatusAttack(bool self, int value)
    {
        if (self)
            GameManager.player.ApplyStatus(value, 0, 0);
        else if (!self)
            CombatManager.enemy.ApplyStatus(value, 0, 0);
    }

    public void StatusDefense(bool self, int value)
    {
        if (self)
            GameManager.player.ApplyStatus(0, value, 0);
        else if (!self)
            CombatManager.enemy.ApplyStatus(0, value, 0);
    }

    public void StatusHeal(bool self, int value)
    {
        if (self)
            GameManager.player.ApplyStatus(0, 0, value);
        else if (!self)
            CombatManager.enemy.ApplyStatus(0, 0, value);
    }

    public void DrawCard(bool self, int value)
    {
        for (int i = 0; i < value; i++)
        {
            Deck.DrawCard(GameManager.player.GetPlayer());
        }
    }
}

public class Condition : MonoBehaviour
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


    public Dictionary<LookUpTable.DelegateType, LookUpTable.DelegateType> conditionPairs;
    public List<ConditionTypes> conditionTypes;


    void Init()
    {
        conditionPairs = new Dictionary<LookUpTable.DelegateType, LookUpTable.DelegateType>();
        conditionTypes = new List<ConditionTypes>();
    }

    public bool CheckCondition()
    {
        int index = 0;
        bool result = true;

        foreach (KeyValuePair<LookUpTable.DelegateType, LookUpTable.DelegateType> pair in conditionPairs)
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


//Custom inspector starts here
#if UNITY_EDITOR

[CustomEditor(typeof(CardBehaviour))]
class CardBehaviourInspectorEditor : Editor
{
    //cast target
    CardBehaviour behaviourScript;
    Condition conditionScript; 

    enum BehaviourType
    {
        Attack,
        Defense,
        Heal,
        LoseHp,
        StatusAttack,
        StatusDefense,
        StatusHeal,
        Draw
    }
    BehaviourType behaviourType;


    bool condition = false;

    bool literalValue = false;
    int compValue;
    LookUpTable.DelegateType firstDelegate;
    LookUpTable.DelegateType secondDelegate;

    Condition.ConditionTypes conditionTypes;
    public override void OnInspectorGUI()
    {
        behaviourScript = target as CardBehaviour;
        conditionScript = target as Condition;

        behaviourScript.value = EditorGUILayout.IntField("Value", behaviourScript.value);
        behaviourScript.behaviourTarget = EditorGUILayout.Toggle("Self", behaviourScript.behaviourTarget);
        condition = EditorGUILayout.Toggle("Condition", condition);
        behaviourType = (BehaviourType)EditorGUILayout.EnumPopup("Choose Behaviour",behaviourType);



        if (condition)
        {
            firstDelegate = (LookUpTable.DelegateType)EditorGUILayout.EnumPopup(firstDelegate);
            literalValue = EditorGUILayout.Toggle("Literal value", literalValue);

            if (literalValue)
                compValue = EditorGUILayout.IntField("Value", compValue);
            else if(!literalValue)
                secondDelegate = (LookUpTable.DelegateType)EditorGUILayout.EnumPopup(secondDelegate);

            conditionTypes = (Condition.ConditionTypes)EditorGUILayout.EnumPopup(conditionTypes);

            conditionScript.conditionTypes.Add(conditionTypes);

            conditionScript.conditionPairs.Add(firstDelegate, secondDelegate);
        }


        switch (behaviourType)
        {
            case BehaviourType.Attack:
                behaviourScript.behaviorUse += behaviourScript.AttackCard;
                break;
            case BehaviourType.Defense:
                behaviourScript.behaviorUse += behaviourScript.DefenseCard;
                break;
            case BehaviourType.Heal:
                behaviourScript.behaviorUse += behaviourScript.HealCard;
                break;
            case BehaviourType.LoseHp:
                behaviourScript.behaviorUse += behaviourScript.TakeHPCard;
                break;
            case BehaviourType.StatusAttack:
                behaviourScript.behaviorUse += behaviourScript.StatusAttack;
                break;
            case BehaviourType.StatusDefense:
                behaviourScript.behaviorUse += behaviourScript.StatusDefense;
                break;
            case BehaviourType.StatusHeal:
                behaviourScript.behaviorUse += behaviourScript.StatusHeal;
                break;
            case BehaviourType.Draw:
                behaviourScript.behaviorUse += behaviourScript.DrawCard;
                break;
        }

        
        EditorUtility.SetDirty(target);
    }
}//end inspectorclass

#endif