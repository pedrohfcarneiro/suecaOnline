using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DropableArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public delegate void DropableEvent(Card card, PointerEventData pointerData);

    public event DropableEvent onDropedCard;

    protected bool isPointerInsideArea = false;

    // Start is called before the first frame update
    void Start()
    {
        //Card.OnDragEvent += handleCardDrag;
        Card.OnPointerUpEvent += handleCardPointerUp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handleCardDrag(Card card, PointerEventData pointerData)
    {
        
    }

    public void handleCardPointerUp(Card card, PointerEventData pointerData)
    {
        if (!isPointerInsideArea) return;

        if(card != null)
        {
            Debug.Log(card);
            onDropedCard?.Invoke(card, pointerData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInsideArea = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerInsideArea = false;
    }
}
