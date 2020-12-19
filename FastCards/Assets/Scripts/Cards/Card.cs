using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class Card : MonoBehaviour
{
    //Enums
    public enum CardType
    {
        Attack,
        Defense,
        Status,
        Heal,
        Draw,
        Passive
    }

    //Essentials
    [SerializeField] public string cardName = null;
    [SerializeField] public string cardDescription = null;

    [SerializeField] public int cost;

    [SerializeField] public bool discard;
    [SerializeField] public bool self;

    [SerializeField] public CardType cardType;

    //Value types
    [SerializeField] public int damage;
    [SerializeField] public int armor;
    [SerializeField] public int heal;
    [SerializeField] public int draw;

    [SerializeField] public int statusDamage;
    [SerializeField] public int statusDefense;
    [SerializeField] public int statusHealing;
    [SerializeField] public List <CardPassive> passivesApplied = new List<CardPassive>();

    [SerializeField] List<CardBehaviour> cardBehaviours = new List<CardBehaviour>();
    //Events
    protected Action<PlayerFunctions> cardUse;

    //Basic Card Functions
    protected void AttackCard(PlayerFunctions player)
    {
        int fullDamage = damage + player.GetPlayer().GetStatusDamage() + GameManager.combatManager.combo;
        if (fullDamage >= 0)
            CombatManager.enemy.TakeDamage(fullDamage);
    }

    protected void DefenseCard(PlayerFunctions player)
    {
        player.AddArmor(armor + GameManager.combatManager.combo);
    }

    protected void HealCard(PlayerFunctions player)
    {
        player.Heal(heal + GameManager.combatManager.combo);
    }

    protected void StatusCard(PlayerFunctions player)
    {
        if (self)
            player.ApplyStatus(statusDamage, statusDamage, statusHealing);
        else if (!self)
            CombatManager.enemy.ApplyStatus(statusDamage, statusDefense, statusHealing);
    }

    protected void DrawCard(PlayerFunctions player)
    {
        for (int i = 0; i < draw; i++)
        {
            Deck.DrawCard(player.GetPlayer());
        }
    }

    //Virtual functions so childs use them
    public virtual void CardInit()
    {
        
    }

    public virtual void CardUse()
    {
        if (GameManager.combatManager.currentComboSeconds > 0f)
            GameManager.combatManager.BuildCombo();
    }

    protected void OneTimeUse()
    {

    }

    public string GetCardName() { return cardName; }
    public void SetCardName(string value) { cardName = value; }
}


//Custom inspector starts here
#if UNITY_EDITOR

[CustomEditor(typeof(CardBehaviour))]
class CardBehaviourInspectorEditor : Editor
{
    ////cast target
    //CardBehaviour behaviourScript;
    //Condition conditionScript;

    //enum BehaviourType
    //{
    //    Attack,
    //    Defense,
    //    Heal,
    //    LoseHp,
    //    StatusAttack,
    //    StatusDefense,
    //    StatusHeal,
    //    Draw
    //}
    //BehaviourType behaviourType;


    //bool condition = false;

    //bool literalValue = false;
    //int compValue;
    //LookUpTable.DelegateType firstDelegate;
    //LookUpTable.DelegateType secondDelegate;

    //Condition.ConditionTypes conditionTypes;
    public override void OnInspectorGUI()
    {

        //behaviourScript.value = EditorGUILayout.IntField("Value", behaviourScript.value);
        //behaviourScript.behaviourTarget = EditorGUILayout.Toggle("Self", behaviourScript.behaviourTarget);
        //condition = EditorGUILayout.Toggle("Condition", condition);
        //behaviourType = (BehaviourType)EditorGUILayout.EnumPopup("Choose Behaviour", behaviourType);



        //if (condition)
        //{
        //    firstDelegate = (LookUpTable.DelegateType)EditorGUILayout.EnumPopup(firstDelegate);
        //    literalValue = EditorGUILayout.Toggle("Literal value", literalValue);

        //    if (literalValue)
        //        compValue = EditorGUILayout.IntField("Value", compValue);
        //    else if (!literalValue)
        //        secondDelegate = (LookUpTable.DelegateType)EditorGUILayout.EnumPopup(secondDelegate);

        //    conditionTypes = (Condition.ConditionTypes)EditorGUILayout.EnumPopup(conditionTypes);

        //    conditionScript.conditionTypes.Add(conditionTypes);

        //    conditionScript.deleagtePairs.Add(firstDelegate, secondDelegate);
        //}


        //switch (behaviourType)
        //{
        //    case BehaviourType.Attack:
        //        behaviourScript.behaviorUse += behaviourScript.AttackCard;
        //        break;
        //    case BehaviourType.Defense:
        //        behaviourScript.behaviorUse += behaviourScript.DefenseCard;
        //        break;
        //    case BehaviourType.Heal:
        //        behaviourScript.behaviorUse += behaviourScript.HealCard;
        //        break;
        //    case BehaviourType.LoseHp:
        //        behaviourScript.behaviorUse += behaviourScript.TakeHPCard;
        //        break;
        //    case BehaviourType.StatusAttack:
        //        behaviourScript.behaviorUse += behaviourScript.StatusAttack;
        //        break;
        //    case BehaviourType.StatusDefense:
        //        behaviourScript.behaviorUse += behaviourScript.StatusDefense;
        //        break;
        //    case BehaviourType.StatusHeal:
        //        behaviourScript.behaviorUse += behaviourScript.StatusHeal;
        //        break;
        //    case BehaviourType.Draw:
        //        behaviourScript.behaviorUse += behaviourScript.DrawCard;
        //        break;
        //}


        //EditorUtility.SetDirty(target);
    }
}//end inspectorclass

#endif