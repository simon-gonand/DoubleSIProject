using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    [Header("Requirements")]
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text cardType;
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text cardGameplayText;
    [SerializeField] private TMP_Text cardLoreText;

    public bool canDisplay = true;
    private string cardEffect;

    /// <summary>
    /// display the card info on the tooltip screen
    /// </summary>
    /// <param name="displayedCard"></param>
    public void DisplayInfo(Card displayedCard)
    {
        if (canDisplay && displayedCard != null)
        {
            cardNameText.text = displayedCard.stats.cardName;
            cardType.text = displayedCard.stats.cardType;
            cardImage.sprite = displayedCard.stats.cardSprite;

            switch (displayedCard.stats.effect)
            {
                case CardPreset.Effect.Unchangeable:
                    cardEffect = "freeze the power of the targeted units";
                    break;
                case CardPreset.Effect.Equals:
                    cardEffect = "change the power of the targeted units to " + displayedCard.stats.power;
                    break;
                case CardPreset.Effect.Plus:
                    cardEffect = "add " + displayedCard.stats.power + " to the targeted units power";
                    break;
                case CardPreset.Effect.Minus:
                    cardEffect = "remove " + displayedCard.stats.power + " to the targeted units power";
                    break;
                case CardPreset.Effect.Multiply:
                    cardEffect = "multiply the targeted units power by " + displayedCard.stats.power;
                    break;
                case CardPreset.Effect.Divide:
                    cardEffect = "divide the targeted units power by " + displayedCard.stats.power;
                    break;
                case CardPreset.Effect.Nullify:
                    cardEffect = "make the targeted units power equal to 0";
                    break;
                case CardPreset.Effect.Heal:
                    cardEffect = "heal you instead by " + displayedCard.stats.power + " instead of attacking at the end of the turn";
                    break;
                case CardPreset.Effect.None:
                    cardEffect = "";
                    break;
            }

            cardGameplayText.text = "Base Power : " + displayedCard.power + " - Effect : This unit will " + cardEffect;
            cardLoreText.text = displayedCard.stats.cardDescription;
        }
    }

    /// <summary>
    /// Remove the info displayed on the tooltip screen
    /// </summary>
    public void CleanInfo()
    {
        cardNameText.text = null;
        cardType.text = null;
        cardImage.sprite = null;
        cardGameplayText.text = null;
        cardLoreText.text = null;
    }
}
