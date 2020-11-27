using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Heal Card", menuName = "Card/Heal Card")]
public class CardHeal : Card
{
    public enum HealType
    {
        HealOnly,
        HealWAttack,
        HealWDefense,
        HealWStatus,
        HealWDraw
    }

    public HealType healType;

    //Combined Healings
    void HealingWithAttack(PlayerFunctions player)
    {
        HealCard(player);
        AttackCard(player);
    }

    void HealingWithDefense(PlayerFunctions player)
    {
        HealCard(player);
        DefenseCard(player);
    }

    void HealingWithStatus(PlayerFunctions player)
    {
        HealCard(player);
        StatusCard(player);
    }

    void HealingWithDraw(PlayerFunctions player)
    {
        HealCard(player);
        DrawCard(player);
    }

    public override void CardInit()
    {
        cardType = CardType.Heal;

        switch (healType)
        {
            case HealType.HealOnly:
                cardUse += HealCard;
                break;
            case HealType.HealWAttack:
                cardUse += HealingWithAttack;
                break;
            case HealType.HealWDefense:
                cardUse += HealingWithDefense;
                break;
            case HealType.HealWStatus:
                cardUse += HealingWithStatus;
                break;
            case HealType.HealWDraw:
                cardUse += HealingWithDraw;
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

[CustomEditor(typeof(CardHeal))]
class HealInspectorEditor : Editor
{
    //cast target
    CardHeal enumScript;

    public override void OnInspectorGUI()
    {
        enumScript = target as CardHeal;

        //Enum drop down
        enumScript.cardName = EditorGUILayout.TextField("Name", enumScript.cardName);
        enumScript.cardDescription = EditorGUILayout.TextField("Description", enumScript.cardDescription);
        enumScript.cost = EditorGUILayout.IntField("Cost", enumScript.cost);
        enumScript.discard = EditorGUILayout.Toggle("Discard", enumScript.discard);
        enumScript.self = EditorGUILayout.Toggle("Self", enumScript.self);
        enumScript.heal = EditorGUILayout.IntField("Heal", enumScript.heal);
        enumScript.healType = (CardHeal.HealType)EditorGUILayout.EnumPopup(enumScript.healType);

        //switch statement for different variables

        switch (enumScript.healType)
        {
            case CardHeal.HealType.HealOnly:
                break;
            case CardHeal.HealType.HealWAttack:
                enumScript.damage = EditorGUILayout.IntField("Damage", enumScript.damage);
                break;
            case CardHeal.HealType.HealWDefense:
                enumScript.armor = EditorGUILayout.IntField("Defense", enumScript.armor);
                break;
            case CardHeal.HealType.HealWStatus:
                enumScript.statusDamage = EditorGUILayout.IntField("Status Damage", enumScript.statusDamage);
                enumScript.statusDefense = EditorGUILayout.IntField("Status Defense", enumScript.statusDefense);
                enumScript.statusHealing = EditorGUILayout.IntField("Status Healing", enumScript.statusHealing);
                break;
        }
        EditorUtility.SetDirty(target);
    }
}//end inspectorclass

#endif
