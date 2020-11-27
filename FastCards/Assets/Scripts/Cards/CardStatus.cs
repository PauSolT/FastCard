using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Status Card", menuName = "Card/Status Card")]
public class CardStatus : Card
{
    public enum StatusType
    {
        StatusOnly,
        StatusWAttack,
        StatusWDefense,
        StatusWHeal,
        StatusWDraw
    }

    public StatusType statusType;

    //Combined Status
    void StatusWithAttack(PlayerFunctions player)
    {
        AttackCard(player);
        StatusCard(player);
    }

    void StatusWithDefense(PlayerFunctions player)
    {
        StatusCard(player);
        DefenseCard(player);
    }

    void StatusWithHealing(PlayerFunctions player)
    {
        StatusCard(player);
        HealCard(player);
    }

    void StatusWithDraw(PlayerFunctions player)
    {
        StatusCard(player);
        DrawCard(player);
    }

    public override void CardInit()
    {
        cardType = CardType.Status;
        switch (statusType)
        {
            case StatusType.StatusOnly:
                cardUse += StatusCard;
                break;
            case StatusType.StatusWAttack:
                cardUse += StatusWithAttack;
                break;
            case StatusType.StatusWDefense:
                cardUse += StatusWithDefense;
                break;
            case StatusType.StatusWHeal:
                cardUse += StatusWithHealing;
                break;
            case StatusType.StatusWDraw:
                cardUse += StatusWithDraw;
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

[CustomEditor(typeof(CardStatus))]
class StatusInspectorEditor : Editor
{
    //cast target
    CardStatus enumScript;

    public override void OnInspectorGUI()
    {
        enumScript = target as CardStatus;

        //Enum drop down
        enumScript.cardName = EditorGUILayout.TextField("Name", enumScript.cardName);
        enumScript.cardDescription = EditorGUILayout.TextField("Description", enumScript.cardDescription);
        enumScript.cost = EditorGUILayout.IntField("Cost", enumScript.cost);
        enumScript.discard = EditorGUILayout.Toggle("Discard", enumScript.discard);
        enumScript.self = EditorGUILayout.Toggle("Self", enumScript.self);
        enumScript.statusDamage = EditorGUILayout.IntField("Status Damage", enumScript.statusDamage);
        enumScript.statusDefense = EditorGUILayout.IntField("Status Defense", enumScript.statusDefense);
        enumScript.statusHealing = EditorGUILayout.IntField("Status Healing", enumScript.statusHealing);
        enumScript.statusType = (CardStatus.StatusType)EditorGUILayout.EnumPopup(enumScript.statusType);

        //switch statement for different variables

        switch (enumScript.statusType)
        {
            case CardStatus.StatusType.StatusOnly:
                break;
            case CardStatus.StatusType.StatusWAttack:
                enumScript.damage = EditorGUILayout.IntField("Damage", enumScript.damage);
                break;
            case CardStatus.StatusType.StatusWDefense:
                enumScript.armor = EditorGUILayout.IntField("Defense", enumScript.armor);
                break;
            case CardStatus.StatusType.StatusWHeal:
                enumScript.heal = EditorGUILayout.IntField("Heal", enumScript.heal);
                break;
        }
        EditorUtility.SetDirty(target);
    }
}//end inspectorclass

#endif