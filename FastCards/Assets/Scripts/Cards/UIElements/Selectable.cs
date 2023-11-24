using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//Component of cards as a reward of defeating an enemy
public class Selectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public bool selected = false;
    private RectTransform rectTransform;

    public Card card;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = new Vector2(1.05f, 1.05f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!selected)
        {
            rectTransform.localScale = new Vector2(1.0f, 1.0f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!selected && RewardSystem.rewardsSelected < RewardSystem.numMaxRewards)
        {
            selected = true;
            transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = card.colorBgCard;
            RewardSystem.IncreaseRewardsSelected();
        }
        else if (selected)
        {
            selected = false;
            transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f);
            RewardSystem.DecreaseRewardsSelected();
        }
    }
}
