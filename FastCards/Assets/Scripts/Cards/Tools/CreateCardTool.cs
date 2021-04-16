// Creates a simple wizard that lets you create a Light GameObject
// or if the user clicks in "Apply", it will set the color of the currently
// object selected to red

using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

#if UNITY_EDITOR
public class CreateCardTool : ScriptableWizard
{
    public int id;
    public string cardName;
    [TextArea]
    public string cardDescription;
    public int cost;
    public bool discard;

    public Card.CardType type;
    public Card.CardTier tier;

    Color colorCard;
    Color colorBackground;

    public CardBehaviour[] cardBehaviour;

    [MenuItem("Tools/Create Card")]
    static void CreateWizard()
    {
        DisplayWizard<CreateCardTool>("Create Card", "Create");
        //If you don't want to use the secondary button simply leave it out:
        //ScriptableWizard.DisplayWizard<WizardCreateLight>("Create Light", "Create");
    }

    void OnWizardCreate()
    {
        string filePath = "Assets/StreamingAssets/CardData.json";

        switch (type)
        {
            case Card.CardType.Attack:
                colorBackground = new Color(0.8078431f, 0.1254902f, 0.1607843f, 1.0f);
                break;
            case Card.CardType.Defense:
                colorBackground = new Color(0.1254902f, 0.678621f, 0.8078431f, 1.0f);
                break;
            case Card.CardType.Heal:
                colorBackground = new Color(0.1792898f, 0.745283f, 0.2996411f, 1.0f);
                break;
            case Card.CardType.Status:
                colorBackground = new Color(0.8867924f, 0.4497346f, 0.1882343f, 1.0f);
                break;
            case Card.CardType.Draw:
                colorBackground = new Color(0.5943396f, 0.5943396f, 0.5943396f, 1.0f);
                break;
            case Card.CardType.Passive:
                colorBackground = new Color(0.7710036f, 0.4123353f, 0.7735849f, 1.0f);
                break;
        }
        
        switch (tier)
        {
            case Card.CardTier.Common:
                colorCard = new Color(0.7710036f, 0.4123353f, 0.7735849f, 1.0f);
                break;
            case Card.CardTier.Rare:
                colorCard = new Color(0.05598078f, 0.4471672f, 0.6981132f, 1.0f);
                break;
            case Card.CardTier.Epic:
                colorCard = new Color(0.7138336f, 0.164204f, 0.7735849f, 1.0f);
                break;
            case Card.CardTier.Legendary:
                colorCard = new Color(0.8392157f, 0.1568318f, 0.1215686f, 1.0f);
                break;
        }
                                             


        Card card = new Card() {
            cardName = cardName,
            cardDescription = cardDescription,
            cardType = type,
            cardTier = tier,
            cost = cost,
            discard = discard,
            colorBgCard = colorBackground,
            colorCard = colorCard
        };

        for (int i = 0; i < cardBehaviour.Length; i++)
        {
            card.cardBehaviours.Add(cardBehaviour[i]);

            switch (card.cardBehaviours[i].behaviourType)
            {
                case CardBehaviour.BehaviourType.Attack:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].AttackCard;
                    break;
                case CardBehaviour.BehaviourType.Defense:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].DefenseCard;
                    break;
                case CardBehaviour.BehaviourType.Heal:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].HealCard;
                    break;
                case CardBehaviour.BehaviourType.TakeHp:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].TakeHPCard;
                    break;
                case CardBehaviour.BehaviourType.BuffAttack:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].StatusAttack;
                    break;
                case CardBehaviour.BehaviourType.BuffDefense:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].StatusDefense;
                    break;
                case CardBehaviour.BehaviourType.BuffHealing:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].StatusHeal;
                    break;
                case CardBehaviour.BehaviourType.Draw:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].DrawCard;
                    break;
                case CardBehaviour.BehaviourType.Passive:
                    card.cardBehaviours[i].passiveBehaviour += cardBehaviour[i].PassiveCard;
                    break;
            }
        }

        string jsonData = File.ReadAllText(filePath);
        string cardData = JsonUtility.ToJson(card, true);
        cardData = cardData.Substring(0, cardData.Length - 1);

        int ind = jsonData.LastIndexOf("]");
        jsonData = jsonData.Insert(ind, ",\n" + cardData + "}\n");
        File.WriteAllText(filePath, jsonData);
    }

    void OnWizardUpdate()
    {
       // helpString = "Please set the color of the light!";
    }

    // When the user presses the "Apply" button OnWizardOtherButton is called.
    void OnWizardOtherButton()
    {
        if (Selection.activeTransform != null)
        {
            Light lt = Selection.activeTransform.GetComponent<Light>();

            if (lt != null)
            {
                lt.color = Color.red;
            }
        }
    }
}

#endif