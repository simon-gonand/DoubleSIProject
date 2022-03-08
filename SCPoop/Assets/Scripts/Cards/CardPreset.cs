using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Material texture;
    public Mesh scpMesh;
    public Material scpMaterial;
}
