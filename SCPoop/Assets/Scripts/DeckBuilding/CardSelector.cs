using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    [HideInInspector]public Sprite selectorImage;
    public CardPreset cardPreset;
    bool selected = false;
    public int deckCardCount = 0; //test
    
    // Start is called before the first frame update
    void Start()
    {
        //assign image from CardPreset
    }

    public void ToggleAddRemove()
    {
        if (!selected)
        {
            if (deckCardCount < 12)
            {
                deckCardCount++;
                Debug.Log("Card added, Total: " + deckCardCount);
                selected = true;
            }
            else
                Debug.Log("Deck is full!");
        }
        else
        {
            deckCardCount--;
            Debug.Log("Card removed, Total: " + deckCardCount);
            selected = false;
        }
    }
}
