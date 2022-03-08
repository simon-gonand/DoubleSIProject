using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardPreset stats;
    public Transform self;
    public GameObject arrow;
    public MeshFilter symbol;    
    public List<Mesh> symbols;
    public TextMesh power;
    public MeshRenderer mesh;
    private float depthArrow = 0.3f;
    private float _tempPower;
    public float tempPower { get { return _tempPower; } }
    private bool _tempIsHeal;
    public bool tempIsHeal { get { return _tempIsHeal; } }
    private bool tempIsUnchangeable;

    private bool _isInStack = true;
    public bool isInStack { get { return _isInStack; }  set { _isInStack = value; } }

    private MaterialPropertyBlock propBlock;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        propBlock = new MaterialPropertyBlock();

        _tempPower = stats.power;
        tempIsUnchangeable = false;
        _tempIsHeal = false;
        InitArrows();
        InitSign();
        InitPower();
        InitGraph();
       
    }

    public void InitArrows()
    {
        for (int j = 0; j <= 7; ++j)
        {
            int mask = 1 << j;
            if ((mask & stats.direction) == mask)
            {
                switch (j)
                {
                    case 0:
                        GameObject localArrowUp = Instantiate(arrow, self, false);
                        localArrowUp.transform.localPosition = new Vector3(0, depthArrow, 4);
                        localArrowUp.transform.localEulerAngles = new Vector3(-90, 90, 0);
                        break;
                    case 1:
                        GameObject localArrowDown = Instantiate(arrow, self, false);
                        localArrowDown.transform.localPosition = new Vector3(0, depthArrow, -4);
                        localArrowDown.transform.localEulerAngles = new Vector3(-90, -90, 0);

                        break;
                    case 2:
                        GameObject localArrowLeft = Instantiate(arrow, self, false);
                        localArrowLeft.transform.localPosition = new Vector3(-4, depthArrow, 0);
                        localArrowLeft.transform.localEulerAngles = new Vector3(-90, 0, 0);
                        break;
                    case 3:
                        GameObject localArrowRight = Instantiate(arrow, self, false);
                        localArrowRight.transform.localPosition = new Vector3(4, depthArrow, 0);
                        localArrowRight.transform.localEulerAngles = new Vector3(-90, 0, 180);

                        break;
                    case 4:
                        GameObject localArrowUpRight = Instantiate(arrow, self, false);
                        localArrowUpRight.transform.localPosition = new Vector3(4, depthArrow, 4);
                        localArrowUpRight.transform.localEulerAngles = new Vector3(-90, 135, 0);
                        break;
                    case 5:
                        GameObject localArrowUpLeft = Instantiate(arrow, self, false);
                        localArrowUpLeft.transform.localPosition = new Vector3(-4, depthArrow, 4);
                        localArrowUpLeft.transform.localEulerAngles = new Vector3(-90, 45, 0);

                        break;
                    case 6:
                        GameObject localArrowDownRight = Instantiate(arrow, self, false);
                        localArrowDownRight.transform.localPosition = new Vector3(4, depthArrow, -4);
                        localArrowDownRight.transform.localEulerAngles = new Vector3(-90, -135, 0);

                        break;
                    case 7:
                        GameObject localArrowDownLeft = Instantiate(arrow, self, false);
                        localArrowDownLeft.transform.localPosition = new Vector3(-4, depthArrow, -4);
                        localArrowDownLeft.transform.localEulerAngles = new Vector3(-90, -45, 0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void InitSign()
    {
        switch (stats.effect)
        {
            case CardPreset.Effect.Unchangeable:
                symbol.mesh = symbols[0];
                break;
            case CardPreset.Effect.Equals:
                symbol.mesh = symbols[1];
                break;
            case CardPreset.Effect.Plus:
                symbol.mesh = symbols[2];
                break;
            case CardPreset.Effect.Minus:
                symbol.mesh = symbols[3];
                break;
            case CardPreset.Effect.Multiply:
                symbol.mesh = symbols[4];
                break;
            case CardPreset.Effect.Divide:
                symbol.mesh = symbols[5];
                break;
            case CardPreset.Effect.Nullify:
                symbol.mesh = symbols[6];
                break;
            case CardPreset.Effect.Heal:
                symbol.mesh = symbols[7];
                break;
            case CardPreset.Effect.None:
                symbol.mesh = symbols[8];
                return;
        }
    }
    public void InitPower()
    {
        power.text = stats.power.ToString();
    }    public void InitGraph()
    {
        mesh.GetPropertyBlock(propBlock);

        propBlock.SetTexture("MainTex", stats.texture);

        mesh.SetPropertyBlock(propBlock);
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
}