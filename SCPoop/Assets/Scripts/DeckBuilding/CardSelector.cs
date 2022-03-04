using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    public Sprite selectorImage;
    public CardPreset cardPreset;
    bool selected = false;
    [SerializeField] private GameObject selectTrinket;
    //public int deckCardCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        selectTrinket.SetActive(false);
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

        selectTrinket.SetActive(selected);
    }
}
