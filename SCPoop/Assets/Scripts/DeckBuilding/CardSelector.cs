using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSelector : MonoBehaviour
{
    public Image image;
    public CardPreset cardPreset;
    public TextMeshProUGUI symbol;
    public TextMeshProUGUI power;
    public List<Image> arrows;

    bool selected = false;
    public bool IsSelected { get { return selected; } }
    [SerializeField] private GameObject selectTrinket;
    //public int deckCardCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        selectTrinket.SetActive(false);
        //assign image from CardPreset
        InitSymbol();
        power.text = cardPreset.power.ToString();
        InitArrows();
    }

    private void InitSymbol()
    {
        switch (cardPreset.effect)
        {
            case CardPreset.Effect.Unchangeable:
                symbol.text = "[]";
                break;
            case CardPreset.Effect.Equals:
                symbol.text = "=";
                break;
            case CardPreset.Effect.Plus:
                symbol.text = "+";
                break;
            case CardPreset.Effect.Minus:
                symbol.text = "-";
                break;
            case CardPreset.Effect.Multiply:
                symbol.text = "x";
                break;
            case CardPreset.Effect.Divide:
                symbol.text = "/";
                break;
            case CardPreset.Effect.Nullify:
                symbol.text = "∅";
                break;
            case CardPreset.Effect.Heal:
                symbol.text = "<3";
                break;
            case CardPreset.Effect.None:
                symbol.text = "";
                return;
        }
    }

    public void InitArrows()
    {
        for (int j = 0; j <= 7; ++j)
        {
            int mask = 1 << j;
            if ((mask & cardPreset.direction) == mask)
            {
                arrows[j].enabled = true;
            }
        }
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
