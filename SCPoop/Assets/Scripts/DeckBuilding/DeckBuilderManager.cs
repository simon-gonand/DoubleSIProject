using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilderManager : MonoBehaviour
{
    #region Variable
    public static DeckBuilderManager instance;

    private int cardCount = 0;
    private List<CardPreset> deck = new List<CardPreset>();
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    public bool AddCard(CardPreset _card)
    {
        if (cardCount < 12)
        {
            deck.Add(_card);
            cardCount++;
            Debug.Log("Card added, Total: " + cardCount);
            return true;
        }
        else
        {
            Debug.Log("Deck is full!");
            return false;
        }
    }

    public bool RemoveCard(CardPreset _card)
    {
        deck.Remove(_card);
        cardCount--;
        Debug.Log("Card removed, Total: " + cardCount);
        return false;
    }
}
