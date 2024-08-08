using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private HorizontalCardHolder cardHolder = null;

    public HorizontalCardHolder GetCardHolder() { return cardHolder; }

    public List<Card> getCards
    {
        get
        {
            return cardHolder.currentCards;
        }
        private set { }
    }

    private void Awake()
    {
        cardHolder = GetComponent<HorizontalCardHolder>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
