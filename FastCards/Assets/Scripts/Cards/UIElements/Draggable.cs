﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Card card;
    private RectTransform rectTransform;
    private Vector2 startingPosition;

    private int unitsToShowCard = 180;

    private int siblingIndex;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta * 1.32f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        siblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        CombatManager.hand.enabled = false;
        startingPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + unitsToShowCard);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = startingPosition;
        transform.SetSiblingIndex(siblingIndex);
        CombatManager.hand.enabled = true;
    }

    bool cardUsed = false;
    public void OnEndDrag(PointerEventData eventData)
    {
        float playField = Screen.height * 0.2f;
        if ( playField <= rectTransform.anchoredPosition.y &&!cardUsed)
        {
            Debug.Log(gameObject.name + " used!");
            cardUsed = true;
            GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), card);
            Destroy(gameObject);
            GameManager.deck.cardsGO.Remove(gameObject);
            CombatManager.hand.enabled = true;
            CombatManager.hand.spacing -= 100;
        }
        else
        {
            rectTransform.anchoredPosition = startingPosition;
            transform.SetSiblingIndex(siblingIndex);
            CombatManager.hand.enabled = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition = eventData.pressPosition;
        //transform.SetAsLastSibling();
    }
}
