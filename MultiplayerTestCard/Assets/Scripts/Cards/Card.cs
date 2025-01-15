using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : NetworkBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public class CardInteractionEventArgs : EventArgs
    {
        public PointerEventData pointerEventData;
        public Card card;
    }

    public delegate void CardInteractionEvent(Card card, PointerEventData pointerData);

    
    public static event CardInteractionEvent OnPointerDownEvent;
    public static event CardInteractionEvent OnPointerUpEvent;
    public static event CardInteractionEvent OnDragEvent;
    public static event CardInteractionEvent OnBeginDragEvent;
    public static event CardInteractionEvent OnEndDragEvent;
    public static event CardInteractionEvent OnPointerEnterEvent;
    public static event CardInteractionEvent OnPointerExitEvent;

    public bool isDragging = false;

    public GameObject currentSlot = null;
    public HorizontalCardHolder currentHolder = null;
    public int parentIndex = 0;
    public bool isSelected = false;
    public float selectionOffset = 1f;
    public CardNaipe naipe = CardNaipe.Clubs;
    public CardNumber number = CardNumber.AS;

    public GameObject cardVisual = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            handleDrag(Input.mousePosition);
        }
    }

    #region Selectable Events
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke(this, eventData) ;
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke(this, eventData);
        handleOnPointerUp();
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this, eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(this, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(this, eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke(this, eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke(this, eventData);
    }
    #endregion

    protected void handleDrag(Vector2 pointerPosition)
    {
        transform.position = Vector3.Lerp(transform.position, pointerPosition, Time.deltaTime * 16);
    }

    protected void handleOnPointerUp()
    {
        transform.localPosition = Vector3.zero;
    }

}
