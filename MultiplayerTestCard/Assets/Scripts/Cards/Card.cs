using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public class CardInteractionEventArgs : EventArgs
    {
        public PointerEventData pointerEventData;
        public Card card;
    }
    
    public event EventHandler<CardInteractionEventArgs> OnPointerDownEvent;
    public event EventHandler<CardInteractionEventArgs> OnPointerUpEvent;
    public event EventHandler<CardInteractionEventArgs> OnDragEvent;
    public event EventHandler<CardInteractionEventArgs> OnBeginDragEvent;
    public event EventHandler<CardInteractionEventArgs> OnEndDragEvent;
    public event EventHandler<CardInteractionEventArgs> OnPointerEnterEvent;
    public event EventHandler<CardInteractionEventArgs> OnPointerExitEvent;

    public bool isDragging = false;

    public GameObject currentSlot = null;
    public int parentIndex = 0;
    public bool isSelected = false;
    public float selectionOffset = 1f;

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
        OnPointerDownEvent?.Invoke(this, new CardInteractionEventArgs { pointerEventData = eventData, card = this }) ;
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke(this, new CardInteractionEventArgs { pointerEventData = eventData, card = this });
        handleOnPointerUp();
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this, new CardInteractionEventArgs { pointerEventData = eventData , card = this });
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(this, new CardInteractionEventArgs { pointerEventData = eventData , card = this });
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(this, new CardInteractionEventArgs { pointerEventData = eventData , card = this });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke(this, new CardInteractionEventArgs { pointerEventData = eventData , card = this });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke(this, new CardInteractionEventArgs { pointerEventData = eventData , card = this });
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
