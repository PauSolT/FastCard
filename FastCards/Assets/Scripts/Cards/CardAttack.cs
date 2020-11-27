﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Attack Card", menuName = "Card/Attack Card")]

public class CardAttack : Card
{
    public enum AttackType
    {
        AttackOnly,
        AttackWDefense,
        AttackWStatus,
        AttackWHeal,
        AttackWDraw
    }

    public AttackType attackType;

    //Combined Attacks
    void AttackWithDefense(PlayerFunctions player)
    {
        AttackCard(player);
        DefenseCard(player);
    }

    void AttackWithHealing(PlayerFunctions player)
    {
        AttackCard(player);
        HealCard(player);
    }

    void AttackWithStatus(PlayerFunctions player)
    {
        AttackCard(player);
        StatusCard(player);
    }

    void AttackWithDraw(PlayerFunctions player)
    {
        AttackCard(player);
        DrawCard(player);
    }

    public override void CardInit()
    {
        cardType = CardType.Attack;

        switch (attackType)
        {
            case AttackType.AttackOnly:
                cardUse += AttackCard;
                break;
            case AttackType.AttackWDefense:
                cardUse += AttackWithDefense;
                break;
            case AttackType.AttackWHeal:
                cardUse += AttackWithHealing;
                break;
            case AttackType.AttackWStatus:
                cardUse += AttackWithStatus;
                break;
            case AttackType.AttackWDraw:
                cardUse += AttackWithDraw;
                break;
        }
    }

    public override void CardUse()
    {
        cardUse?.Invoke(GameManager.player);
        base.CardUse();
    }

}

//Custom inspector starts here
#if UNITY_EDITOR

[CustomEditor(typeof(CardAttack))]
class AttackInspectorEditor : Editor
{
    //cast target
    CardAttack enumScript;

    public override void OnInspectorGUI()
    {
        enumScript = target as CardAttack;
        
        //Enum drop down
        enumScript.cardName = EditorGUILayout.TextField("Name", enumScript.cardName);
        enumScript.cardDescription = EditorGUILayout.TextField("Description", enumScript.cardDescription);
        enumScript.cost = EditorGUILayout.IntField("Cost", enumScript.cost);
        enumScript.discard = EditorGUILayout.Toggle("Discard", enumScript.discard);
        enumScript.self = EditorGUILayout.Toggle("Self", enumScript.self);
        enumScript.damage = EditorGUILayout.IntField("Damage", enumScript.damage);
        enumScript.attackType = (CardAttack.AttackType)EditorGUILayout.EnumPopup(enumScript.attackType);

        //switch statement for different variables

        switch (enumScript.attackType)
        {
            case CardAttack.AttackType.AttackOnly:
                break;
            case CardAttack.AttackType.AttackWDefense:
                enumScript.armor = EditorGUILayout.IntField("Defense", enumScript.armor);
                break;
            case CardAttack.AttackType.AttackWHeal:
                enumScript.heal = EditorGUILayout.IntField("Heal", enumScript.heal);
                break;
            case CardAttack.AttackType.AttackWStatus:
                enumScript.statusDamage = EditorGUILayout.IntField("Status Damage", enumScript.statusDamage);
                enumScript.statusDefense = EditorGUILayout.IntField("Status Defense", enumScript.statusDefense);
                enumScript.statusHealing = EditorGUILayout.IntField("Status Healing", enumScript.statusHealing);
                break;
        }
        EditorUtility.SetDirty(target);
    }
}//end inspectorclass

#endif