using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "New Card", order = 0)]
public class CardPreset : ScriptableObject
{
    public enum Effect
    {
        Unchangeable = 0,
        Equals,
        Plus,
        Minus,
        Multiply,
        Divide,
        Nullify,
        Heal,
        None
    }

    public int power;
    public int direction;
    public Effect effect;
    public Texture texture;
    public Mesh scpMesh;
    public Material scpMaterial;
    public Sprite cardSprite;
    public string cardName;
    public string cardType;
    public string cardDescription;
    public Sprite cardImage;
}
