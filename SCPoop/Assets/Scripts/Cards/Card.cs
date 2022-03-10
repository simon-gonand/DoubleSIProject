using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public MeshSpawner meshSpawner;

    public CardPreset stats;
    public Transform self;
    public GameObject arrow;
    public MeshFilter symbol;
    public MeshRenderer materialSymbol;
    public List<Mesh> symbols;
    public List<Material> symbolMaterials;
    public TextMesh power;
    public MeshRenderer mesh;
    private float depthArrow = 0.3f;
    [HideInInspector] public float _tempPower;
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
        if (stats != null)
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
                        localArrowUp.layer = gameObject.layer;
                        localArrowUp.GetComponentsInChildren<Transform>()[1].gameObject.layer = gameObject.layer;
                        localArrowUp.transform.localPosition = new Vector3(0, depthArrow, 4);
                        localArrowUp.transform.localEulerAngles = new Vector3(-90, 90, 0);
                        break;
                    case 1:
                        GameObject localArrowDown = Instantiate(arrow, self, false);
                        localArrowDown.layer = gameObject.layer;
                        localArrowDown.GetComponentsInChildren<Transform>()[1].gameObject.layer = gameObject.layer;
                        localArrowDown.transform.localPosition = new Vector3(0, depthArrow, -4);
                        localArrowDown.transform.localEulerAngles = new Vector3(-90, -90, 0);

                        break;
                    case 2:
                        GameObject localArrowLeft = Instantiate(arrow, self, false);
                        localArrowLeft.layer = gameObject.layer;
                        localArrowLeft.GetComponentsInChildren<Transform>()[1].gameObject.layer = gameObject.layer;
                        localArrowLeft.transform.localPosition = new Vector3(-4, depthArrow, 0);
                        localArrowLeft.transform.localEulerAngles = new Vector3(-90, 0, 0);
                        break;
                    case 3:
                        GameObject localArrowRight = Instantiate(arrow, self, false);
                        localArrowRight.layer = gameObject.layer;
                        localArrowRight.GetComponentsInChildren<Transform>()[1].gameObject.layer = gameObject.layer;
                        localArrowRight.transform.localPosition = new Vector3(4, depthArrow, 0);
                        localArrowRight.transform.localEulerAngles = new Vector3(-90, 0, 180);
                        break;
                    case 4:
                        GameObject localArrowUpRight = Instantiate(arrow, self, false);
                        localArrowUpRight.layer = gameObject.layer;
                        localArrowUpRight.GetComponentsInChildren<Transform>()[1].gameObject.layer = gameObject.layer;
                        localArrowUpRight.transform.localPosition = new Vector3(4, depthArrow, 4);
                        localArrowUpRight.transform.localEulerAngles = new Vector3(-90, 135, 0);
                        break;
                    case 5:
                        GameObject localArrowUpLeft = Instantiate(arrow, self, false);
                        localArrowUpLeft.layer = gameObject.layer;
                        localArrowUpLeft.GetComponentsInChildren<Transform>()[1].gameObject.layer = gameObject.layer;
                        localArrowUpLeft.transform.localPosition = new Vector3(-4, depthArrow, 4);
                        localArrowUpLeft.transform.localEulerAngles = new Vector3(-90, 45, 0);

                        break;
                    case 6:
                        GameObject localArrowDownRight = Instantiate(arrow, self, false);
                        localArrowDownRight.GetComponentsInChildren<Transform>()[1].gameObject.layer = gameObject.layer;
                        localArrowDownRight.layer = gameObject.layer;
                        localArrowDownRight.transform.localPosition = new Vector3(4, depthArrow, -4);
                        localArrowDownRight.transform.localEulerAngles = new Vector3(-90, -135, 0);

                        break;
                    case 7:
                        GameObject localArrowDownLeft = Instantiate(arrow, self, false);
                        localArrowDownLeft.layer = gameObject.layer;
                        localArrowDownLeft.GetComponentsInChildren<Transform>()[1].gameObject.layer = gameObject.layer;
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
                materialSymbol.material = symbolMaterials[0];
                break;
            case CardPreset.Effect.Equals:
                symbol.mesh = symbols[1];
                materialSymbol.material = symbolMaterials[1];
                break;
            case CardPreset.Effect.Plus:
                symbol.mesh = symbols[2];
                materialSymbol.material = symbolMaterials[2];
                break;
            case CardPreset.Effect.Minus:
                symbol.mesh = symbols[3];
                materialSymbol.material = symbolMaterials[3];
                break;
            case CardPreset.Effect.Multiply:
                symbol.mesh = symbols[4];
                materialSymbol.material = symbolMaterials[4];
                break;
            case CardPreset.Effect.Divide:
                symbol.mesh = symbols[5];
                materialSymbol.material = symbolMaterials[5];
                break;
            case CardPreset.Effect.Nullify:
                symbol.mesh = symbols[6];
                materialSymbol.material = symbolMaterials[6];
                break;
            case CardPreset.Effect.Heal:
                symbol.mesh = symbols[7];
                materialSymbol.material = symbolMaterials[7];
                break;
            case CardPreset.Effect.None:
                symbol.mesh = symbols[8];
                materialSymbol.material = symbolMaterials[8];
                return;
        }
    }
    public void InitPower()
    {
        power.text = stats.power.ToString();
    }    
    
    public void InitGraph()
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

    public IEnumerator PlayCard(int x, int y)
    {
        float t = 0.0f;
        int index = y * 3 + x;
        Vector3 startPos = self.position;
        Vector3 endPos = Grid.instance.slots[index].position;
        Quaternion startRot = self.rotation;
        Quaternion endRot = Quaternion.Euler(Vector3.zero);
        while (t < 1.0f)
        {
            t += Time.deltaTime * 10.0f;
            self.position = Vector3.Lerp(startPos, endPos, t);
            self.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }        
        Grid.instance.AddCard(this, x, y);
    }
}