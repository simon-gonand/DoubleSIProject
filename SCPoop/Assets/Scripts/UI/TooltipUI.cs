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

    private void Start()
    {
        CleanInfo();
    }

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
            cardImage.enabled = true;
            cardImage.sprite = displayedCard.stats.cardTooltipImage;

            switch (displayedCard.stats.effect)
            {
                case CardPreset.Effect.Unchangeable:
                    cardEffect = "Targeted units power can't be altered";
                    break;
                case CardPreset.Effect.Equals:
                    cardEffect = "Change the power of the targeted units to " + displayedCard.stats.power;
                    break;
                case CardPreset.Effect.Plus:
                    cardEffect = "Add " + displayedCard.stats.power + " to the targeted units power";
                    break;
                case CardPreset.Effect.Minus:
                    cardEffect = "Remove " + displayedCard.stats.power + " to the targeted units power";
                    break;
                case CardPreset.Effect.Multiply:
                    cardEffect = "Multiply the targeted units power by " + displayedCard.stats.power;
                    break;
                case CardPreset.Effect.Divide:
                    cardEffect = "Divide the targeted units power by " + displayedCard.stats.power;
                    break;
                case CardPreset.Effect.Nullify:
                    cardEffect = "Reduce the targeted units power to 0";
                    break;
                case CardPreset.Effect.Heal:
                    cardEffect = "Targeted units heal you by their power INSTEAD of attacking";
                    break;
                case CardPreset.Effect.None:
                    cardEffect = "None";
                    break;
            }

            cardGameplayText.text = "BASE POWER : " + displayedCard.stats.power + "\nEFFECT : " + cardEffect + ".";
            //cardLoreText.text = displayedCard.stats.cardDescription;
        }
    }

    /// <summary>
    /// Remove the info displayed on the tooltip screen
    /// </summary>
    public void CleanInfo()
    {
        cardNameText.text = null;
        cardType.text = null;
        cardImage.enabled = false;
        cardGameplayText.text = null;
        //cardLoreText.text = null;
    }
}
