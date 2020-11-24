using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Draw Card", menuName = "Card/Draw Card")]
public class CardDraw : Card
{

    public override void CardInit()
    {
        cardType = CardType.Draw;
        cardUse += DrawCard;
    }

    public override void CardUse()
    {
        cardUse?.Invoke(GameManager.player);
    }
}


//Custom inspector starts here
#if UNITY_EDITOR

[CustomEditor(typeof(CardDraw))]
class DrawInspectorEditor : Editor
{

    //cast target
    CardDraw enumScript;

    public override void OnInspectorGUI()
    {
        enumScript = target as CardDraw;

        //Enum drop down
        enumScript.cardName = EditorGUILayout.TextField("Name", enumScript.cardName);
        enumScript.cardDescription = EditorGUILayout.TextField("Description", enumScript.cardDescription);
        enumScript.cost = EditorGUILayout.IntField("Cost", enumScript.cost);
        enumScript.discard = EditorGUILayout.Toggle("Discard", enumScript.discard);
        enumScript.draw = EditorGUILayout.IntField("Nº Cards Draw", enumScript.draw);
    }
}//end inspectorclass

#endif