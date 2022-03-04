using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class DeckBuilderManager : MonoBehaviour
{
    #region Variable
    public static DeckBuilderManager instance;

    private int cardCount = 0;
    private List<CardPreset> deck = new List<CardPreset>();

    [SerializeField] private Text cardCountText;
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
            //Debug.Log("Card added, Total: " + cardCount);
            cardCountText.text = cardCount.ToString();
            return true;
        }
        else
        {
            //Debug.Log("Deck is full!");
            return false;
        }
    }

    public bool RemoveCard(CardPreset _card)
    {
        deck.Remove(_card);
        cardCount--;
        //Debug.Log("Card removed, Total: " + cardCount);
        cardCountText.text = cardCount.ToString();
        return false;
    }

    //public void ClearDeck()
    //{
    //    deck = new List<CardPreset>();
    //    cardCount = 0;
    //    //Reload scene
    //}
}
