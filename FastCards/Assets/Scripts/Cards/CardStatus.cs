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
        StatusWHeal
    }

    public StatusType statusType;

    public void StatusCard(PlayerFunctions player)
    {
        player.ApplyStatus(statusDamage, statusDefense, statusHealing);
    }

    //Combined Status
    public void StatusWithAttack(PlayerFunctions player)
    {
        player.ApplyStatus(statusDamage, statusDefense, statusHealing);
        int fullDamage = damage + player.GetPlayer().GetStatusDamage();
        if (fullDamage >= 0)
            player.TakeDamage(fullDamage);
    }

    public void StatusWithDefense(PlayerFunctions player)
    {
        player.ApplyStatus(statusDamage, statusDefense, statusHealing);
        player.AddArmor(armor);
    }

    public void StatusWithHealing(PlayerFunctions player)
    {
        player.ApplyStatus(statusDamage, statusDefense, statusHealing);
        player.Heal(heal);
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
        }
    }

    public override void CardUse()
    {
        cardUse?.Invoke(GameManager.player);
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
    }
}//end inspectorclass

#endif