using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    private Text cardNameText;
    private Text cardType;
    private Image cardImage;
    private Text cardGameplayText;
    private Text cardLoreText;


    private bool canDisplay;

    private void Update()
    {
        if (canDisplay)
        {
            
        }
        else
        {
            cardNameText.text = null;
            cardType.text = null;
            cardImage.sprite = null;
            cardGameplayText.text = null;
            cardLoreText.text = null;
        }
    }
}
