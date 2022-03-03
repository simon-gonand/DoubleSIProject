using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardPreset stats;
    public Transform self;

    private float _tempPower;
    public float tempPower { get { return _tempPower; } }
    private bool _tempIsHeal;
    public bool tempIsHeal { get { return _tempIsHeal; } }
    private bool tempIsUnchangeable;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        _tempPower = stats.power;
        tempIsUnchangeable = false;
        _tempIsHeal = false;
    }

    public void ApplyEffect(CardPreset.Effect effect, float value)
    {
        if (tempIsUnchangeable) return;
        switch (effect)
        {
            case CardPreset.Effect.Unchangeable:
                tempIsUnchangeable = true;
                break;
            case CardPreset.Effect.Equals:
                _tempPower = value;
                tempIsUnchangeable = true;
                break;
            case CardPreset.Effect.Plus:
                _tempPower += value;
                break;
            case CardPreset.Effect.Minus:
                _tempPower -= value;
                break;
            case CardPreset.Effect.Multiply:
                _tempPower *= value;
                break;
            case CardPreset.Effect.Divide:
                _tempPower /= value;
                break;
            case CardPreset.Effect.Nullify:
                _tempPower = 0.0f;
                break;
            case CardPreset.Effect.Heal:
                _tempIsHeal = true;
                break;
            case CardPreset.Effect.None:
                return;
        }

        if (_tempPower < 0.0f) _tempPower = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}