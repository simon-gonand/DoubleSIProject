using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSelector : MonoBehaviour
{
    public Image image;
    public CardPreset cardPreset;
    public Image symbol;
    public List<Sprite> symbols;
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
        image.sprite = cardPreset.cardSprite;
        InitArrows();
    }

    private void InitSymbol()
    {
        switch (cardPreset.effect)
        {
            case CardPreset.Effect.Unchangeable:
                symbol.sprite = symbols[0];
                break;
            case CardPreset.Effect.Equals:
                symbol.sprite = symbols[1];
                break;
            case CardPreset.Effect.Plus:
                symbol.sprite = symbols[2];
                break;
            case CardPreset.Effect.Minus:
                symbol.sprite = symbols[3];
                break;
            case CardPreset.Effect.Multiply:
                symbol.sprite = symbols[4];
                break;
            case CardPreset.Effect.Divide:
                symbol.sprite = symbols[5];
                break;
            case CardPreset.Effect.Nullify:
                symbol.sprite = symbols[6];
                break;
            case CardPreset.Effect.Heal:
                symbol.sprite = symbols[7];
                break;
            case CardPreset.Effect.None:
                symbol.enabled = false;
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
