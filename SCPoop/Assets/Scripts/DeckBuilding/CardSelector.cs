using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    [HideInInspector]public Sprite selectorImage;
    public CardPreset cardPreset;
    bool selected = false;
    //public int deckCardCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //assign image from CardPreset
    }

    public void ToggleAddRemove()
    {
        if (!selected)
        {
            selected = DeckBuilderManager.instance.AddCard(cardPreset);
        }
        else
        {
            selected = DeckBuilderManager.instance.RemoveCard(cardPreset);
        }
    }
}
