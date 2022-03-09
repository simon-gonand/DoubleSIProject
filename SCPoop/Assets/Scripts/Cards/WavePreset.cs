using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "New Enemy Wave", order = 1)]
public class WavePreset : ScriptableObject
{
    public CardPreset[] cards = new CardPreset[3];

    [Range(10f, 150f)]
    public float healthPoints;
}
