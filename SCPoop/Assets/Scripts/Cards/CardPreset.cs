using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "New Card", order = 0)]
public class CardPreset : ScriptableObject
{
    public enum Effect
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Equals,
        Null,
        Unchangeable,
        None
    }

    public int power;
    public int direction;
    public Effect effect;
}
