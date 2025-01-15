using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPile : MonoBehaviour
{
    [SerializeField] protected List<Card> cards = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialize(List<Card> cards)
    {
        cards.ForEach(card =>
        {
            this.cards.Add(card);
        });
    }

    public Card popCard()
    {
        Card poppedCard = cards[cards.Count-1];
        cards.RemoveAt(cards.Count - 1);
        return poppedCard;
    }

    public void ReceiveCard(Card receivedCard)
    {
        this.cards.Add(receivedCard);
    }

    public void sendCardTo(CardPile cardPile, Card cardToSend)
    {
        this.cards.Remove(cardToSend);
        cardPile.ReceiveCard(cardToSend);
    }

    public List<Card> getPileCards()
    {
        return this.cards;
    }
}
